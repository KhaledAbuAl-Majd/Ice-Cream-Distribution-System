using Business;
using Guna.UI2.WinForms;
using Ice_Cream_Distribution_System.Models;
using static IceCreamPro.Presentation.FormHelpers;

namespace IceCreamPro.Presentation.Forms
{
    public class frmCars : Form
    {
        private Guna2TextBox txtDetails = new();
        private Guna2ComboBox cmbArea = new();
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new();
        private DataGridView dgv = new();
        private Car? _selected;

        public frmCars() { BuildUI(); WireEvents(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            Controls.Add(new Label { Text = "إدارة السيارات", ForeColor = AppColors.TextPrimary, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });

            var card = new Guna2Panel { Size = new Size(420, 230), Location = new Point(20, 60), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };
            StyleLabel(card, "تفاصيل السيارة", new Point(20, 20));
            txtDetails = StyleTextBox(card, "رقم اللوحة أو الوصف", new Point(20, 42));
            StyleLabel(card, "المنطقة", new Point(20, 100));
            cmbArea.Size = new Size(340, 40); cmbArea.Location = new Point(20, 122);
            cmbArea.FillColor = AppColors.PrimaryDark; cmbArea.ForeColor = AppColors.TextPrimary;
            cmbArea.BorderColor = AppColors.BorderColor; cmbArea.BorderRadius = 10;
            cmbArea.Font = new Font("Segoe UI", 10f); cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
            card.Controls.Add(cmbArea);
            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 180));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(148, 180));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(276, 180));
            card.Controls.AddRange(new Control[] { btnSave, btnDelete, btnClear });
            Controls.Add(card);
            StyleGrid(dgv, new Point(460, 60), new Size(680, 500));
            Controls.Add(dgv);
        }

        private void WireEvents()
        {
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearForm();
            dgv.SelectionChanged += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is not Car c) return;
                _selected = c; txtDetails.Text = c.CarDetails ?? "";
                foreach (var item in cmbArea.Items) { if ((item as dynamic)?.Id == c.AreaId) { cmbArea.SelectedItem = item; break; } }
            };
        }

        private async Task LoadAsync()
        {
            try
            {
                var areas = await AreaService.GetAll();
                cmbArea.DataSource = areas; cmbArea.DisplayMember = "Name"; cmbArea.ValueMember = "Id";
                dgv.DataSource = await CarService.GetAll() ?? new();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnSave_Click(object? s, EventArgs e)
        {
            if (cmbArea.SelectedValue == null) { clsPL_MessageBoxs.ShowErrorMessage("اختر المنطقة"); return; }
            int areaId = Convert.ToInt32(cmbArea.SelectedValue);
            try
            {
                if (_selected is null) await CarService.Add(new Car { AreaId = areaId, CarDetails = txtDetails.Text.Trim() });
                else { _selected.AreaId = areaId; _selected.CarDetails = txtDetails.Text.Trim(); await CarService.Update(_selected); }
                clsPL_MessageBoxs.ShowSuccessMessage("تم الحفظ"); ClearForm(); await LoadAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (_selected is null) { clsPL_MessageBoxs.ShowErrorMessage("اختر سيارة أولاً"); return; }
            if (clsPL_MessageBoxs.ShowConfirmMessage("حذف السيارة؟") != DialogResult.Yes) return;
            try { await CarService.Delete(_selected.Id); clsPL_MessageBoxs.ShowSuccessMessage("تم الحذف"); ClearForm(); await LoadAsync(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private void ClearForm() { _selected = null; txtDetails.Text = ""; cmbArea.SelectedIndex = -1; }
    }
}