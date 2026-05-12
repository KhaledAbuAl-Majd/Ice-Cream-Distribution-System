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
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new(), btnAddRecord = new();
        private DataGridView dgv = new();
        private Invoice? _selected;

        public frmInvoices() { BuildUI(); WireEvents(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            Controls.Add(new Label { Text = "إدارة الفواتير", ForeColor = AppColors.TextPrimary, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });

            var card = new Guna2Panel { Size = new Size(420, 300), Location = new Point(20, 60), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };
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

            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 250));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(148, 250));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(20, 325));
            StyleButton(btnAddRecord, "إضافة عنصر", AppColors.BorderColor, new Point(148, 325));
            card.Controls.AddRange(new Control[] { btnSave, btnDelete, btnClear, btnAddRecord });
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
            btnAddRecord.Click += BtnAddRecord_Click;
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
                cmbCar.DataSource = cars; cmbCar.DisplayMember = "Id"; cmbCar.ValueMember = "Id";
                var stores = await StoreService.GetAll();
                cmbStore.DataSource = stores; cmbStore.DisplayMember = "Id"; cmbStore.ValueMember = "Id";
                dgv.DataSource = await InvoiceService.GetAll() ?? new();

                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    item.Visible = false;
                }

                dgv.Columns[nameof(Invoice.Id)].Visible = true;
                dgv.Columns[nameof(Invoice.CarId)].Visible = true;
                dgv.Columns[nameof(Invoice.Date)].Visible = true;
                dgv.Columns[nameof(Invoice.Total)].Visible = true;
                dgv.Columns[nameof(Invoice.StoreId)].Visible = true;
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
            try
            {

                var result = await InvoiceService.Delete(_selected.Id);

                if (!result)
                    return;

                clsPL_MessageBoxs.ShowSuccessMessage("تم الحذف");
                ClearForm();
                await LoadAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private void ClearForm() { _selected = null; txtNotes.Text = ""; cmbCar.SelectedIndex = cmbStore.SelectedIndex = -1; }
        private async void BtnAddRecord_Click(object? sender, EventArgs e)
        {
            var currentInvoiceId = _selected.Id;
            using (var frm = new frmInvoiceRecords(currentInvoiceId))
            {
                frm.ShowDialog();
                await LoadAsync();
                clsPL_MessageBoxs.ShowSuccessMessage("تمت إضافة الصنف وحساب الإجمالي تلقائياً");
            }
        }
    }
}
