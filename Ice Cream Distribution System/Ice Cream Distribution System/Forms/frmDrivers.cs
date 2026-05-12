using Business;
using Guna.UI2.WinForms;
using Ice_Cream_Distribution_System.Models;
using static IceCreamPro.Presentation.FormHelpers;

namespace IceCreamPro.Presentation.Forms
{
    public class frmDrivers : Form
    {
        private Guna2ComboBox cmbUser = new(), cmbCar = new();
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new();
        private DataGridView dgv = new();
        private Driver? _selected;

        public frmDrivers() { BuildUI(); WireEvents(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            Controls.Add(new Label { Text = "إدارة السائقين", ForeColor = AppColors.TextPrimary, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });

            var card = new Guna2Panel { Size = new Size(420, 210), Location = new Point(20, 60), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };
            StyleLabel(card, "المستخدم", new Point(20, 20));
            cmbUser.Size = new Size(340, 40); cmbUser.Location = new Point(20, 42);
            cmbUser.FillColor = AppColors.PrimaryDark; cmbUser.ForeColor = AppColors.TextPrimary;
            cmbUser.BorderColor = AppColors.BorderColor; cmbUser.BorderRadius = 10;
            cmbUser.Font = new Font("Segoe UI", 10f); cmbUser.DropDownStyle = ComboBoxStyle.DropDownList;
            card.Controls.Add(cmbUser);

            StyleLabel(card, "السيارة", new Point(20, 98));
            cmbCar.Size = new Size(340, 40); cmbCar.Location = new Point(20, 118);
            cmbCar.FillColor = AppColors.PrimaryDark; cmbCar.ForeColor = AppColors.TextPrimary;
            cmbCar.BorderColor = AppColors.BorderColor; cmbCar.BorderRadius = 10;
            cmbCar.Font = new Font("Segoe UI", 10f); cmbCar.DropDownStyle = ComboBoxStyle.DropDownList;
            card.Controls.Add(cmbCar);

            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 168));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(148, 168));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(276, 168));
            card.Controls.AddRange(new Control[] { btnSave, btnDelete, btnClear });
            card.Dock = DockStyle.Left;
            Controls.Add(card);

            Panel pnlGridHolder = new Panel();
            pnlGridHolder.Location = new Point(460, 60);
            pnlGridHolder.Size = new Size(this.ClientSize.Width - 480, this.ClientSize.Height - 120);
            pnlGridHolder.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlGridHolder.BackColor = Color.Transparent;
            this.Controls.Add(pnlGridHolder);

            pnlGridHolder.Controls.Add(dgv);
            dgv.Dock = DockStyle.Fill;

            StyleGrid(dgv);
        }

        private void WireEvents()
        {
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearForm();
            dgv.SelectionChanged += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is not Driver d) return;
                _selected = d;
                foreach (var item in cmbUser.Items) { if ((item as dynamic)?.UserId == d.UserId) { cmbUser.SelectedItem = item; break; } }
                foreach (var item in cmbCar.Items) { if ((item as dynamic)?.Id == d.CarId) { cmbCar.SelectedItem = item; break; } }
            };
        }

        private async Task LoadAsync()
        {
            try
            {
                var users = await UserService.GetAll();
                cmbUser.DataSource = users; cmbUser.DisplayMember = "UserName"; cmbUser.ValueMember = "UserId";
                var cars = await CarService.GetAll();
                cmbCar.DataSource = cars; cmbCar.DisplayMember = "Id"; cmbCar.ValueMember = "Id";
                dgv.DataSource = await DriverService.GetAll() ?? new();

                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    item.Visible = false;
                }

                dgv.Columns[nameof(Driver.Id)].Visible = true;
                dgv.Columns[nameof(Driver.CarId)].Visible = true;
                dgv.Columns[nameof(Driver.UserId)].Visible = true;
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnSave_Click(object? s, EventArgs e)
        {
            if (cmbUser.SelectedValue == null) { clsPL_MessageBoxs.ShowErrorMessage("اختر المستخدم"); return; }
            if (cmbCar.SelectedValue == null) { clsPL_MessageBoxs.ShowErrorMessage("اختر السيارة"); return; }
            int userId = Convert.ToInt32(cmbUser.SelectedValue);
            short carId = Convert.ToInt16(cmbCar.SelectedValue);
            try
            {
                if (_selected is null) await DriverService.Add(new Driver { UserId = userId, CarId = carId });
                else { _selected.UserId = userId; _selected.CarId = carId; await DriverService.Update(_selected); }
                clsPL_MessageBoxs.ShowSuccessMessage("تم الحفظ"); ClearForm(); await LoadAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (_selected is null) { clsPL_MessageBoxs.ShowErrorMessage("اختر سائقاً أولاً"); return; }
            if (clsPL_MessageBoxs.ShowConfirmMessage("حذف السائق؟") != DialogResult.Yes) return;
            try { await DriverService.Delete(_selected.Id); clsPL_MessageBoxs.ShowSuccessMessage("تم الحذف"); ClearForm(); await LoadAsync(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private void ClearForm() { _selected = null; cmbUser.SelectedIndex = cmbCar.SelectedIndex = -1; }
    }
}