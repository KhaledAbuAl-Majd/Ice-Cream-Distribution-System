using Business;
using Guna.UI2.WinForms;
using Ice_Cream_Distribution_System.Models;
using static IceCreamPro.Presentation.FormHelpers;

namespace IceCreamPro.Presentation.Forms
{
    public class frmStores : Form
    {
        private Guna2TextBox txtOwnerName = new(), txtPhone = new(), txtAddress = new();
        private Guna2ComboBox cmbArea = new();
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new();
        private DataGridView dgv = new();
        private Store? _selected;

        public frmStores() { BuildUI(); WireEvents(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            Controls.Add(new Label { Text = "إدارة المحلات", ForeColor = AppColors.TextPrimary, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });

            var card = new Guna2Panel { Size = new Size(420, 320), Location = new Point(20, 60), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };
            StyleLabel(card, "اسم صاحب المحل", new Point(20, 20));
            txtOwnerName = StyleTextBox(card, "الاسم الكامل", new Point(20, 42));
            StyleLabel(card, "رقم الهاتف", new Point(20, 100));
            txtPhone = StyleTextBox(card, "01XXXXXXXXX", new Point(20, 122));
            StyleLabel(card, "العنوان", new Point(20, 178));
            txtAddress = StyleTextBox(card, "العنوان", new Point(20, 200));

            StyleLabel(card, "المنطقة", new Point(20, 258));
            cmbArea.Size = new Size(340, 40); cmbArea.Location = new Point(20, 278);
            cmbArea.FillColor = AppColors.PrimaryDark; cmbArea.ForeColor = AppColors.TextPrimary;
            cmbArea.BorderColor = AppColors.BorderColor; cmbArea.BorderRadius = 10;
            cmbArea.Font = new Font("Segoe UI", 10f); cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
            card.Controls.Add(cmbArea);

            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 328));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(148, 328));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(276, 328));
            card.Size = new Size(420, 376);
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
                if (dgv.CurrentRow?.DataBoundItem is not Store st) return;
                _selected = st;
                txtOwnerName.Text = st.Owner?.PersonName ?? "";
                txtPhone.Text = st.Owner?.Phone ?? "";
                txtAddress.Text = st.Owner?.Address ?? "";
                foreach (var item in cmbArea.Items) { if ((item as dynamic)?.Id == st.AreaId) { cmbArea.SelectedItem = item; break; } }
            };
        }

        private async Task LoadAsync()
        {
            try
            {
                var areas = await AreaService.GetAll();
                cmbArea.DataSource = areas; cmbArea.DisplayMember = "Name"; cmbArea.ValueMember = "Id";
                dgv.DataSource = await StoreService.GetAll() ?? new();

                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    item.Visible = false;
                }

                dgv.Columns[nameof(Store.Id)].Visible = true;
                dgv.Columns[nameof(Store.Balance)].Visible = true;
                dgv.Columns[nameof(Store.AreaId)].Visible = true;
                dgv.Columns[nameof(Store.OwnerId)].Visible = true;
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnSave_Click(object? s, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOwnerName.Text)) { clsPL_MessageBoxs.ShowErrorMessage("أدخل اسم الصاحب"); return; }
            if (cmbArea.SelectedValue == null) { clsPL_MessageBoxs.ShowErrorMessage("اختر المنطقة"); return; }
            int areaId = Convert.ToInt32(cmbArea.SelectedValue);
            try
            {
                if (_selected is null)
                {
                    var store = new Store { AreaId = areaId, Owner = new Person { PersonName = txtOwnerName.Text.Trim(), Phone = txtPhone.Text.Trim(), Address = txtAddress.Text.Trim() } };
                    await StoreService.Add(store);
                }
                else
                {
                    _selected.AreaId = areaId;
                    if (_selected.Owner != null) { _selected.Owner.PersonName = txtOwnerName.Text.Trim(); _selected.Owner.Phone = txtPhone.Text.Trim(); _selected.Owner.Address = txtAddress.Text.Trim(); }
                    await StoreService.Update(_selected);
                }
                clsPL_MessageBoxs.ShowSuccessMessage("تم الحفظ"); ClearForm(); await LoadAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (_selected is null) { clsPL_MessageBoxs.ShowErrorMessage("اختر محلاً أولاً"); return; }
            if (clsPL_MessageBoxs.ShowConfirmMessage("حذف المحل؟") != DialogResult.Yes) return;
            try { await StoreService.Delete(_selected.Id); clsPL_MessageBoxs.ShowSuccessMessage("تم الحذف"); ClearForm(); await LoadAsync(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private void ClearForm() { _selected = null; txtOwnerName.Text = txtPhone.Text = txtAddress.Text = ""; cmbArea.SelectedIndex = -1; }
    }
}