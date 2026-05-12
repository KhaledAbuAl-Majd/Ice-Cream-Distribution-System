using Business;
using Guna.UI2.WinForms;
using Ice_Cream_Distribution_System.Models;
using static IceCreamPro.Presentation.FormHelpers;

namespace IceCreamPro.Presentation.Forms
{
    public class frmInvoices : Form
    {
        private Guna2ComboBox cmbCar = new(), cmbStore = new();
        private Guna2TextBox txtNotes = new();
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new();
        private DataGridView dgv = new();
        private Invoice? _selected;

        public frmInvoices() { BuildUI(); WireEvents(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            Controls.Add(new Label { Text = "إدارة الفواتير", ForeColor = AppColors.TextPrimary, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });

            var card = new Guna2Panel { Size = new Size(420, 270), Location = new Point(20, 60), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };
            StyleLabel(card, "السيارة", new Point(20, 20));
            cmbCar.Size = new Size(340, 40); cmbCar.Location = new Point(20, 42);
            cmbCar.FillColor = AppColors.PrimaryDark; cmbCar.ForeColor = AppColors.TextPrimary;
            cmbCar.BorderColor = AppColors.BorderColor; cmbCar.BorderRadius = 10;
            cmbCar.Font = new Font("Segoe UI", 10f); cmbCar.DropDownStyle = ComboBoxStyle.DropDownList;
            card.Controls.Add(cmbCar);

            StyleLabel(card, "المحل", new Point(20, 98));
            cmbStore.Size = new Size(340, 40); cmbStore.Location = new Point(20, 118);
            cmbStore.FillColor = AppColors.PrimaryDark; cmbStore.ForeColor = AppColors.TextPrimary;
            cmbStore.BorderColor = AppColors.BorderColor; cmbStore.BorderRadius = 10;
            cmbStore.Font = new Font("Segoe UI", 10f); cmbStore.DropDownStyle = ComboBoxStyle.DropDownList;
            card.Controls.Add(cmbStore);

            StyleLabel(card, "ملاحظات", new Point(20, 175));
            txtNotes = StyleTextBox(card, "ملاحظات اختيارية", new Point(20, 197));

            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 228));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(148, 228));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(276, 228));
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
                if (dgv.CurrentRow?.DataBoundItem is not Invoice inv) return;
                _selected = inv; txtNotes.Text = inv.Notes ?? "";
                foreach (var item in cmbCar.Items) { if ((item as dynamic)?.Id == inv.CarId) { cmbCar.SelectedItem = item; break; } }
                foreach (var item in cmbStore.Items) { if ((item as dynamic)?.Id == inv.StoreId) { cmbStore.SelectedItem = item; break; } }
            };
        }

        private async Task LoadAsync()
        {
            try
            {
                var cars = await CarService.GetAll();
                cmbCar.DataSource = cars; cmbCar.DisplayMember = "CarDetails"; cmbCar.ValueMember = "Id";
                var stores = await StoreService.GetAll();
                cmbStore.DataSource = stores; cmbStore.DisplayMember = "Id"; cmbStore.ValueMember = "Id";
                dgv.DataSource = await InvoiceService.GetAll() ?? new();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnSave_Click(object? s, EventArgs e)
        {
            if (cmbCar.SelectedValue == null) { clsPL_MessageBoxs.ShowErrorMessage("اختر السيارة"); return; }
            if (cmbStore.SelectedValue == null) { clsPL_MessageBoxs.ShowErrorMessage("اختر المحل"); return; }
            short carId = Convert.ToInt16(cmbCar.SelectedValue);
            int storeId = Convert.ToInt32(cmbStore.SelectedValue);
            try
            {
                if (_selected is null) await InvoiceService.Add(new Invoice { CarId = carId, StoreId = storeId, Date = DateTime.Now, Notes = txtNotes.Text });
                else { _selected.CarId = carId; _selected.StoreId = storeId; _selected.Notes = txtNotes.Text; await InvoiceService.Update(_selected); }
                clsPL_MessageBoxs.ShowSuccessMessage("تم الحفظ"); ClearForm(); await LoadAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (_selected is null) { clsPL_MessageBoxs.ShowErrorMessage("اختر فاتورة أولاً"); return; }
            if (clsPL_MessageBoxs.ShowConfirmMessage("حذف الفاتورة؟") != DialogResult.Yes) return;
            try { await InvoiceService.Delete(_selected.Id); clsPL_MessageBoxs.ShowSuccessMessage("تم الحذف"); ClearForm(); await LoadAsync(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private void ClearForm() { _selected = null; txtNotes.Text = ""; cmbCar.SelectedIndex = cmbStore.SelectedIndex = -1; }
    }
}