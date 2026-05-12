using Guna.UI2.WinForms;

namespace IceCreamPro.Presentation.Forms
{
    public class frmMain : Form
    {
        private Panel pnlSidebar = new();
        private Panel pnlContent = new();
        private Panel pnlTopBar = new();
        private Label lblAppName = new();
        private Label lblPageTitle = new();
        private Label lblUserName = new();
        private Panel pnlClose = new();
        private Panel pnlMinimize = new();

        private Guna2Button btnDashboard = new();
        private Guna2Button btnProducts = new();
        private Guna2Button btnProductTypes = new();
        private Guna2Button btnStores = new();
        private Guna2Button btnCars = new();
        private Guna2Button btnDrivers = new();
        private Guna2Button btnReps = new();
        private Guna2Button btnInvoices = new();
        private Guna2Button btnPayments = new();
        private Guna2Button btnUsers = new();
        private Guna2Button btnLogout = new();
        private Guna2Button? _activeBtn;

        public frmMain()
        {
            BuildUI();
            WireEvents();
            NavigationService.Initialize(pnlContent);
            Navigate(btnDashboard, new frmDashboard());
            clsPL_Global.ActiveForm = this;
        }

        private void BuildUI()
        {
            Text = "IceCream Pro";
            Size = new Size(1180, 720);
            //Dock = DockStyle.Fill;
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = AppColors.PrimaryDark;
            //FormBorderStyle = FormBorderStyle.None;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            // TopBar
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Height = 52;
            pnlTopBar.BackColor = AppColors.PrimaryCard;

            pnlClose.Size = new Size(14, 14);
            pnlClose.Location = new Point(16, 19);
            pnlClose.BackColor = AppColors.Danger;
            pnlClose.Cursor = Cursors.Hand;

            pnlMinimize.Size = new Size(14, 14);
            pnlMinimize.Location = new Point(36, 19);
            pnlMinimize.BackColor = AppColors.Warning;
            pnlMinimize.Cursor = Cursors.Hand;

            lblAppName.Text = "🍦 IceCream Pro";
            lblAppName.ForeColor = AppColors.AccentBlue;
            lblAppName.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
            lblAppName.AutoSize = true;
            lblAppName.Location = new Point(200, 14);

            lblPageTitle.Text = "لوحة التحكم";
            lblPageTitle.ForeColor = AppColors.TextSecondary;
            lblPageTitle.Font = new Font("Segoe UI", 10f);
            lblPageTitle.AutoSize = true;
            lblPageTitle.Location = new Point(500, 17);

            lblUserName.Text = $"👤 {clsPL_Global.CurrentUser?.UserName ?? "مستخدم"}";
            lblUserName.ForeColor = AppColors.TextSecondary;
            lblUserName.Font = new Font("Segoe UI", 9f);
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(10, 18);

            pnlTopBar.Controls.AddRange(new Control[] { pnlClose, pnlMinimize, lblAppName, lblPageTitle, lblUserName });

            // Sidebar
            pnlSidebar.Dock = DockStyle.Right;
            pnlSidebar.Width = 200;
            pnlSidebar.BackColor = AppColors.PrimarySidebar;

            var items = new (Guna2Button btn, string text)[]
            {
                (btnDashboard,    "📊  لوحة التحكم"),
                (btnProducts,     "🍦  المنتجات"),
                (btnProductTypes, "📦  أنواع المنتجات"),
                (btnStores,       "🏪  المحلات"),
                (btnCars,         "🚗  السيارات"),
                (btnDrivers,      "👤  السائقون"),
                (btnReps,         "👔  المناديب"),
                (btnInvoices,     "🧾  الفواتير"),
                (btnPayments,     "💰  المدفوعات"),
                (btnUsers,        "👥  المستخدمون"),
            };

            int y = 16;
            foreach (var (btn, text) in items)
            {
                btn.Text = text;
                btn.Size = new Size(192, 42);
                btn.Location = new Point(4, y);
                btn.FillColor = Color.Transparent;
                btn.ForeColor = AppColors.TextSecondary;
                btn.Font = new Font("Segoe UI", 10f);
                btn.BorderRadius = 10;
                btn.Cursor = Cursors.Hand;
                pnlSidebar.Controls.Add(btn);
                y += 48;
            }

            btnLogout.Text = "🚪  تسجيل خروج";
            btnLogout.Size = new Size(192, 42);
            btnLogout.Location = new Point(4, 590);
            btnLogout.FillColor = Color.FromArgb(60, 231, 76, 60);
            btnLogout.ForeColor = AppColors.Danger;
            btnLogout.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            btnLogout.BorderRadius = 10;
            btnLogout.Cursor = Cursors.Hand;
            pnlSidebar.Controls.Add(btnLogout);

            // Content
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.BackColor = AppColors.PrimaryDark;

            Controls.AddRange(new Control[] { pnlContent, pnlSidebar, pnlTopBar });
        }

        private void SetActive(Guna2Button btn)
        {
            if (_activeBtn != null)
            {
                _activeBtn.FillColor = Color.Transparent;
                _activeBtn.ForeColor = AppColors.TextSecondary;
                _activeBtn.Font = new Font("Segoe UI", 10f);
            }
            btn.FillColor = AppColors.SidebarActive;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            _activeBtn = btn;
        }

        private void Navigate(Guna2Button btn, Form form)
        {
            SetActive(btn);
            lblPageTitle.Text = btn.Text.Trim();
            NavigationService.NavigateTo(form);
        }

        private void WireEvents()
        {
            pnlClose.Click += (s, e) => Application.Exit();
            pnlMinimize.Click += (s, e) => WindowState = FormWindowState.Minimized;

            this.FormClosed += (s,e)=> Application.Exit();

            btnDashboard.Click += (s, e) => Navigate(btnDashboard, new frmDashboard());
            btnProducts.Click += (s, e) => Navigate(btnProducts, new frmProducts());
            btnProductTypes.Click += (s, e) => Navigate(btnProductTypes, new frmProductTypes());
            btnStores.Click += (s, e) => Navigate(btnStores, new frmStores());
            btnCars.Click += (s, e) => Navigate(btnCars, new frmCars());
            btnDrivers.Click += (s, e) => Navigate(btnDrivers, new frmDrivers());
            btnReps.Click += (s, e) => Navigate(btnReps, new frmRepresentatives());
            btnInvoices.Click += (s, e) => Navigate(btnInvoices, new frmInvoices());
            btnPayments.Click += (s, e) => Navigate(btnPayments, new frmPayments());
            btnUsers.Click += (s, e) => Navigate(btnUsers, new frmUsers());

            btnLogout.Click += (s, e) =>
            {
                if (clsPL_MessageBoxs.ShowConfirmMessage("هل تريد تسجيل الخروج؟") == DialogResult.Yes)
                {
                    clsPL_Global.CurrentUser = null;
                    new frmLogin().Show();
                    Close();
                }
            };

            pnlTopBar.MouseDown += StartDrag;
            pnlTopBar.MouseMove += DoDrag;
        }

        private Point _drag;
        private void StartDrag(object? s, MouseEventArgs e) { if (e.Button == MouseButtons.Left) _drag = new Point(e.X, e.Y); }
        private void DoDrag(object? s, MouseEventArgs e) { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _drag.X, Location.Y + e.Y - _drag.Y); }
    }
}