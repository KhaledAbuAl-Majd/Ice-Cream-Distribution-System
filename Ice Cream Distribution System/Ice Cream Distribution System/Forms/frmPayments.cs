using Business;
using Guna.UI2.WinForms;
using Ice_Cream_Distribution_System.Models;
using static IceCreamPro.Presentation.FormHelpers;

namespace IceCreamPro.Presentation.Forms
{
    public class frmPayments : Form
    {
        private Guna2ComboBox cmbRep = new(), cmbStore = new();
        private Guna2TextBox txtValue = new(), txtNotes = new();
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new();
        private DataGridView dgv = new();
        private Payment? _selected;

        public frmPayments() { BuildUI(); WireEvents(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            Controls.Add(new Label { Text = "إدارة المدفوعات", ForeColor = AppColors.TextPrimary, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });

            var card = new Guna2Panel { Size = new Size(420, 330), Location = new Point(20, 60), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };
            StyleLabel(card, "المندوب", new Point(20, 20));
            cmbRep.Size = new Size(340, 40); cmbRep.Location = new Point(20, 42);
            cmbRep.FillColor = AppColors.PrimaryDark; cmbRep.ForeColor = AppColors.TextPrimary;
            cmbRep.BorderColor = AppColors.BorderColor; cmbRep.BorderRadius = 10;
            cmbRep.Font = new Font("Segoe UI", 10f); cmbRep.DropDownStyle = ComboBoxStyle.DropDownList;
            card.Controls.Add(cmbRep);

            StyleLabel(card, "المحل", new Point(20, 98));
            cmbStore.Size = new Size(340, 40); cmbStore.Location = new Point(20, 118);
            cmbStore.FillColor = AppColors.PrimaryDark; cmbStore.ForeColor = AppColors.TextPrimary;
            cmbStore.BorderColor = AppColors.BorderColor; cmbStore.BorderRadius = 10;
            cmbStore.Font = new Font("Segoe UI", 10f); cmbStore.DropDownStyle = ComboBoxStyle.DropDownList;
            card.Controls.Add(cmbStore);

            StyleLabel(card, "المبلغ", new Point(20, 175));
            txtValue = StyleTextBox(card, "0.00", new Point(20, 197));
            StyleLabel(card, "ملاحظات", new Point(20, 252));
            txtNotes = StyleTextBox(card, "اختياري", new Point(20, 274));

            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 284));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(148, 284));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(276, 284));
            card.Size = new Size(420, 332);
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
                if (dgv.CurrentRow?.DataBoundItem is not Payment p) return;
                _selected = p; txtValue.Text = p.PayedValue.ToString("N2"); txtNotes.Text = p.Notes ?? "";
                foreach (var item in cmbRep.Items) { if ((item as dynamic)?.Id == p.RepresentativeId) { cmbRep.SelectedItem = item; break; } }
                foreach (var item in cmbStore.Items) { if ((item as dynamic)?.Id == p.StoreId) { cmbStore.SelectedItem = item; break; } }
            };
        }

        private async Task LoadAsync()
        {
            try
            {
                var reps = await RepresentativeService.GetAll();
                cmbRep.DataSource = reps; cmbRep.DisplayMember = "Id"; cmbRep.ValueMember = "Id";
                var stores = await StoreService.GetAll();
                cmbStore.DataSource = stores; cmbStore.DisplayMember = "Id"; cmbStore.ValueMember = "Id";
                dgv.DataSource = await PaymentService.GetAll() ?? new();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnSave_Click(object? s, EventArgs e)
        {
            if (cmbRep.SelectedValue == null) { clsPL_MessageBoxs.ShowErrorMessage("اختر المندوب"); return; }
            if (cmbStore.SelectedValue == null) { clsPL_MessageBoxs.ShowErrorMessage("اختر المحل"); return; }
            if (!decimal.TryParse(txtValue.Text, out decimal val)) { clsPL_MessageBoxs.ShowErrorMessage("المبلغ غير صحيح"); return; }
            int repId = Convert.ToInt32(cmbRep.SelectedValue);
            int storeId = Convert.ToInt32(cmbStore.SelectedValue);
            try
            {
                if (_selected is null) await PaymentService.Add(new Payment { RepresentativeId = repId, StoreId = storeId, PayedValue = val, Date = DateTime.Now, Notes = txtNotes.Text });
                else { _selected.RepresentativeId = repId; _selected.StoreId = storeId; _selected.PayedValue = val; _selected.Notes = txtNotes.Text; await PaymentService.Update(_selected); }
                clsPL_MessageBoxs.ShowSuccessMessage("تم الحفظ"); ClearForm(); await LoadAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (_selected is null) { clsPL_MessageBoxs.ShowErrorMessage("اختر مدفوعة أولاً"); return; }
            if (clsPL_MessageBoxs.ShowConfirmMessage("حذف المدفوعة؟") != DialogResult.Yes) return;
            try { await PaymentService.Delete(_selected.Id); clsPL_MessageBoxs.ShowSuccessMessage("تم الحذف"); ClearForm(); await LoadAsync(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private void ClearForm() { _selected = null; txtValue.Text = txtNotes.Text = ""; cmbRep.SelectedIndex = cmbStore.SelectedIndex = -1; }
    }
}