using Business;
using Guna.UI2.WinForms;

namespace IceCreamPro.Presentation.Forms
{
    public class frmDashboard : Form
    {
        private Label lblStores = new(), lblProducts = new(),
                      lblInvoices = new(), lblPayments = new();

        public frmDashboard() { BuildUI(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            Controls.Add(new Label
            {
                Text = "لوحة التحكم",
                ForeColor = AppColors.TextPrimary,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            });

            var cards = new[]
            {
                (lblStores,   "المحلات",   AppColors.AccentBlue, new Point(20,  70)),
                (lblProducts, "المنتجات",  AppColors.Success,    new Point(220, 70)),
                (lblInvoices, "الفواتير",  AppColors.Warning,    new Point(420, 70)),
                (lblPayments, "المدفوعات", AppColors.Danger,     new Point(620, 70)),
            };

            foreach (var (lbl, title, color, loc) in cards)
            {
                var card = new Guna2Panel { Size = new Size(180, 110), Location = loc, FillColor = AppColors.PrimaryCard, BorderRadius = 16 };
                card.ShadowDecoration.Enabled = true;
                card.ShadowDecoration.Color = color;
                card.ShadowDecoration.Depth = 10;

                card.Controls.Add(new Panel { Size = new Size(5, 110), Location = new Point(175, 0), BackColor = color });
                card.Controls.Add(new Label { Text = title, ForeColor = AppColors.TextSecondary, Font = new Font("Segoe UI", 9f), AutoSize = true, Location = new Point(10, 15) });

                lbl.Text = "..."; lbl.ForeColor = Color.White;
                lbl.Font = new Font("Segoe UI", 24f, FontStyle.Bold);
                lbl.AutoSize = true; lbl.Location = new Point(10, 45);
                card.Controls.Add(lbl);
                Controls.Add(card);
            }
        }

        private async Task LoadAsync()
        {
            try
            {
                lblStores.Text = ((await StoreService.GetAll())?.Count ?? 0).ToString();
                lblProducts.Text = ((await ProductService.GetAll())?.Count ?? 0).ToString();
                lblInvoices.Text = ((await InvoiceService.GetAll())?.Count ?? 0).ToString();
                lblPayments.Text = ((await PaymentService.GetAll())?.Count ?? 0).ToString();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }
    }
}