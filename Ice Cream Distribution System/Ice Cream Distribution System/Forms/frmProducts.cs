using Business;
using Guna.UI2.WinForms;
using Ice_Cream_Distribution_System.Models;
using static IceCreamPro.Presentation.FormHelpers;

namespace IceCreamPro.Presentation.Forms
{
    public class frmProducts : Form
    {
        private Guna2TextBox txtName = new();
        private Guna2TextBox txtPrice = new();
        private Guna2ComboBox cmbType = new();
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new();
        private DataGridView dgv = new();
        private Product? _selected;

        public frmProducts() { BuildUI(); WireEvents(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            Controls.Add(new Label { Text = "إدارة المنتجات", ForeColor = AppColors.TextPrimary, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });

            var card = new Guna2Panel { Size = new Size(420, 290), Location = new Point(20, 60), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };

            StyleLabel(card, "اسم المنتج", new Point(20, 20));
            txtName = StyleTextBox(card, "أدخل اسم المنتج", new Point(20, 42));

            StyleLabel(card, "النوع", new Point(20, 100));
            cmbType.Size = new Size(340, 40); cmbType.Location = new Point(20, 122);
            cmbType.FillColor = AppColors.PrimaryDark; cmbType.ForeColor = AppColors.TextPrimary;
            cmbType.BorderColor = AppColors.BorderColor; cmbType.BorderRadius = 10;
            cmbType.Font = new Font("Segoe UI", 10f); cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            card.Controls.Add(cmbType);

            StyleLabel(card, "السعر", new Point(20, 175));
            txtPrice = StyleTextBox(card, "0.00", new Point(20, 197));

            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 248));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(148, 248));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(276, 248));
            card.Controls.AddRange(new Control[] { btnSave, btnDelete, btnClear });
            Controls.Add(card);

            StyleGrid(dgv, new Point(460, 60), new Size(680, 560));
            Controls.Add(dgv);
        }

        private void WireEvents()
        {
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearForm();
            dgv.SelectionChanged += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is not Product p) return;
                _selected = p; txtName.Text = p.Name; txtPrice.Text = p.Price.ToString("N2");
                foreach (var item in cmbType.Items) { if ((item as dynamic)?.Id == p.ProductTypeId) { cmbType.SelectedItem = item; break; } }
            };
        }

        private async Task LoadAsync()
        {
            try
            {
                var types = await ProductTypeService.GetAll();
                cmbType.DataSource = types; cmbType.DisplayMember = "Name"; cmbType.ValueMember = "Id";
                dgv.DataSource = await ProductService.GetAll() ?? new();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnSave_Click(object? s, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { clsPL_MessageBoxs.ShowErrorMessage("أدخل اسم المنتج"); return; }
            if (!decimal.TryParse(txtPrice.Text, out decimal price)) { clsPL_MessageBoxs.ShowErrorMessage("السعر غير صحيح"); return; }
            if (cmbType.SelectedValue == null) { clsPL_MessageBoxs.ShowErrorMessage("اختر النوع"); return; }
            short typeId = Convert.ToInt16(cmbType.SelectedValue);
            try
            {
                if (_selected is null) await ProductService.Add(new Product { Name = txtName.Text.Trim(), Price = price, ProductTypeId = typeId });
                else { _selected.Name = txtName.Text.Trim(); _selected.Price = price; _selected.ProductTypeId = typeId; await ProductService.Update(_selected); }
                clsPL_MessageBoxs.ShowSuccessMessage("تم الحفظ"); ClearForm(); await LoadAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (_selected is null) { clsPL_MessageBoxs.ShowErrorMessage("اختر منتجاً أولاً"); return; }
            if (clsPL_MessageBoxs.ShowConfirmMessage($"حذف '{_selected.Name}'؟") != DialogResult.Yes) return;
            try { await ProductService.Delete(_selected.Id); clsPL_MessageBoxs.ShowSuccessMessage("تم الحذف"); ClearForm(); await LoadAsync(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private void ClearForm() { _selected = null; txtName.Text = ""; txtPrice.Text = ""; cmbType.SelectedIndex = -1; }
    }
}