using Business;
using Guna.UI2.WinForms;
using Ice_Cream_Distribution_System.Models;
using static IceCreamPro.Presentation.FormHelpers;

namespace IceCreamPro.Presentation.Forms
{
    public class frmInvoiceRecords : Form
    {
        private Guna2ComboBox cmbProduct = new();
        private Guna2TextBox txtCount = new(), txtPrice = new();
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new();
        private DataGridView dgv = new();

        private readonly int _invoiceId;
        private InvoiceRecord? _selected;

        public frmInvoiceRecords(int invoiceId)
        {
            _invoiceId = invoiceId;
            BuildUI();
            WireEvents();
            _ = LoadDataAsync();
        }

        private void BuildUI()
        {
            this.Size = new Size(1000, 600);
            this.Text = $"تفاصيل الأصناف للفاتورة رقم: {_invoiceId}";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = AppColors.PrimaryDark;
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            // كارت الإدخال (يمين)
            var card = new Guna2Panel { Size = new Size(350, 400), Location = new Point(20, 20), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };

            StyleLabel(card, "المنتج", new Point(20, 20));
            cmbProduct.Size = new Size(310, 36); cmbProduct.Location = new Point(20, 42);
            SetupComboStyle(cmbProduct);
            card.Controls.Add(cmbProduct);

            StyleLabel(card, "الكمية", new Point(20, 90));
            txtCount = StyleTextBox(card, "0", new Point(20, 112));

            StyleLabel(card, "سعر الوحدة", new Point(20, 160));
            txtPrice = StyleTextBox(card, "0.00", new Point(20, 182));
            txtPrice.ReadOnly = true;

            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 250));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(130, 250));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(240, 250));

            card.Controls.AddRange(new Control[] { btnSave, btnDelete, btnClear });
            Controls.Add(card);

            // الجريد (يسار)
            StyleGrid(dgv, new Point(390, 20), new Size(570, 500));
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(dgv);
        }

        private void SetupComboStyle(Guna2ComboBox cmb)
        {
            cmb.FillColor = AppColors.PrimaryDark;
            cmb.ForeColor = AppColors.TextPrimary;
            cmb.BorderColor = AppColors.BorderColor;
            cmb.BorderRadius = 10;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void WireEvents()
        {
            cmbProduct.SelectedIndexChanged += (s, e) => {
                if (cmbProduct.SelectedItem is Product p && _selected == null)
                    txtPrice.Text = p.Price.ToString("N2");
            };

            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearForm();

            dgv.SelectionChanged += (s, e) => {
                if (dgv.CurrentRow?.DataBoundItem is not InvoiceRecord ir) return;
                _selected = ir;
                cmbProduct.SelectedValue = ir.ProductId;
                txtCount.Text = ir.Count.ToString();
                txtPrice.Text = ir.ProductPrice.ToString("N2");
            };
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // تحميل المنتجات
                cmbProduct.DataSource = await ProductService.GetAll();
                cmbProduct.DisplayMember = "Name";
                cmbProduct.ValueMember = "Id";
                cmbProduct.SelectedIndex = -1;

                // تحميل أصناف الفاتورة الحالية
                var records = await InvoiceRecordService.GetAllByInvoiceId(_invoiceId);
                dgv.DataSource = records;

                // تنسيق الأعمدة
                foreach (DataGridViewColumn col in dgv.Columns) col.Visible = false;
                dgv.Columns["Product"].Visible = true; dgv.Columns["Product"].HeaderText = "الصنف";
                dgv.Columns["Count"].Visible = true; dgv.Columns["Count"].HeaderText = "الكمية";
                dgv.Columns["ProductPrice"].Visible = true; dgv.Columns["ProductPrice"].HeaderText = "السعر";
                dgv.Columns["Total"].Visible = true; dgv.Columns["Total"].HeaderText = "الإجمالي";

                // استخدام CellFormatting لإظهار اسم المنتج
                dgv.CellFormatting += (s, e) => {
                    if (dgv.Columns[e.ColumnIndex].Name == "Product" && e.Value is Product p)
                    {
                        e.Value = p.Name;
                        e.FormattingApplied = true;
                    }
                };
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnSave_Click(object? s, EventArgs e)
        {
            if (cmbProduct.SelectedValue == null) return;
            if (!short.TryParse(txtCount.Text, out short count)) return;
            if (!decimal.TryParse(txtPrice.Text, out decimal price)) return;

            try
            {
                if (_selected == null)
                {
                    await InvoiceRecordService.Add(new InvoiceRecord
                    {
                        InvoiceId = _invoiceId,
                        ProductId = (int)cmbProduct.SelectedValue,
                        Count = count,
                        ProductPrice = price
                    });
                }
                else
                {
                    _selected.ProductId = (int)cmbProduct.SelectedValue;
                    _selected.Count = count; _selected.ProductPrice = price;
                    await InvoiceRecordService.Update(_selected);
                }
                ClearForm(); await LoadDataAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (_selected == null) return;
            if (clsPL_MessageBoxs.ShowConfirmMessage("حذف هذا الصنف؟") != DialogResult.Yes) return;
            await InvoiceRecordService.Delete(_selected.Id);
            ClearForm(); await LoadDataAsync();
        }

        private void ClearForm() { _selected = null; cmbProduct.SelectedIndex = -1; txtCount.Text = ""; txtPrice.Text = ""; }
    }
}