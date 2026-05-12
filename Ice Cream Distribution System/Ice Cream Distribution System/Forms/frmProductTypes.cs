using Business;
using Guna.UI2.WinForms;
using Ice_Cream_Distribution_System.Models;
using static IceCreamPro.Presentation.FormHelpers;

namespace IceCreamPro.Presentation.Forms
{
    public class frmProductTypes : Form
    {
        private Guna2TextBox txtName = new();
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new();
        private DataGridView dgv = new();
        private ProductType? _selected;

        public frmProductTypes() { BuildUI(); WireEvents(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            Controls.Add(new Label { Text = "أنواع المنتجات", ForeColor = AppColors.TextPrimary, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });

            var card = new Guna2Panel { Size = new Size(420, 160), Location = new Point(20, 60), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };
            StyleLabel(card, "اسم النوع", new Point(20, 20));
            txtName = StyleTextBox(card, "مثال: مثلجات كريمي", new Point(20, 42));
            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 108));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(148, 108));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(276, 108));
            card.Controls.AddRange(new Control[] { btnSave, btnDelete, btnClear });
            Controls.Add(card);

            StyleGrid(dgv, new Point(460, 60), new Size(680, 400));
            Controls.Add(dgv);
        }

        private void WireEvents()
        {
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearForm();
            dgv.SelectionChanged += (s, e) => { if (dgv.CurrentRow?.DataBoundItem is not ProductType pt) return; _selected = pt; txtName.Text = pt.Name; };
        }

        private async Task LoadAsync()
        {
            try { dgv.DataSource = await ProductTypeService.GetAll() ?? new(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnSave_Click(object? s, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { clsPL_MessageBoxs.ShowErrorMessage("أدخل اسم النوع"); return; }
            try
            {
                if (_selected is null) await ProductTypeService.Add(new ProductType { Name = txtName.Text.Trim() });
                else { _selected.Name = txtName.Text.Trim(); await ProductTypeService.Update(_selected); }
                clsPL_MessageBoxs.ShowSuccessMessage("تم الحفظ"); ClearForm(); await LoadAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (_selected is null) { clsPL_MessageBoxs.ShowErrorMessage("اختر نوعاً أولاً"); return; }
            if (clsPL_MessageBoxs.ShowConfirmMessage($"حذف '{_selected.Name}'؟") != DialogResult.Yes) return;
            try { await ProductTypeService.Delete(_selected.Id); clsPL_MessageBoxs.ShowSuccessMessage("تم الحذف"); ClearForm(); await LoadAsync(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private void ClearForm() { _selected = null; txtName.Text = ""; }
    }
}