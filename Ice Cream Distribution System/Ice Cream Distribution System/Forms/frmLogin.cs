using Business;
using Guna.UI2.WinForms;

namespace IceCreamPro.Presentation.Forms
{
    public class frmLogin : Form
    {
        // ─────────────────────────────────────────────────────────────
        //  بيانات الأدمن الافتراضي — بيتضاف مرة واحدة عند أول تشغيل
        // ─────────────────────────────────────────────────────────────
        private const string DefaultAdminUser = "admin";
        private const string DefaultAdminPass = "Admin@1234";

        private Guna2Panel pnlCard = new();
        private Label lblTitle = new();
        private Label lblSub = new();
        private Guna2TextBox txtUsername = new();
        private Guna2TextBox txtPassword = new();
        private Guna2Button btnLogin = new();
        private Guna2ToggleSwitch tglShow = new();
        private Label lblError = new();
        private Panel pnlClose = new();

        public frmLogin()
        {
            BuildUI();
            WireEvents();
            // Seed الأدمن في الخلفية بدون ما يعطّل الـ UI
            _ = SeedDefaultAdminAsync();
        }

        // ═════════════════════════════════════════════════════════════
        //  Seed الأدمن الافتراضي — يشتغل مرة واحدة فقط لو مش موجود
        // ═════════════════════════════════════════════════════════════
        private static async Task SeedDefaultAdminAsync()
        {
            try
            {
                var existing = await UserService.Get(DefaultAdminUser);
                if (existing is not null) return;   // موجود بالفعل

                var adminUser = new Ice_Cream_Distribution_System.Models.User
                {
                    UserName = DefaultAdminUser,
                    IsActive = true,
                    Person = new Ice_Cream_Distribution_System.Models.Person
                    {
                        PersonName = "المدير العام",
                        Phone = "01000000000",
                        Address = "المقر الرئيسي",
                        Email = "admin@icecreampro.com"
                    }
                };

                // UserService.Add بيعمل BCrypt للباسورد تلقائياً
                await UserService.Add(adminUser, DefaultAdminPass);
            }
            catch
            {
                // لو في مشكلة اتصال بالـ DB نتجاهلها — ما تأثرش على الـ Login
            }
        }

        // ═════════════════════════════════════════════════════════════
        //  بناء الـ UI
        // ═════════════════════════════════════════════════════════════
        private void BuildUI()
        {
            Text = "IceCream Pro";
            Size = new Size(460, 580);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = AppColors.PrimaryDark;
            FormBorderStyle = FormBorderStyle.None;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            // زر الإغلاق
            pnlClose.Size = new Size(16, 16);
            pnlClose.Location = new Point(16, 16);
            pnlClose.BackColor = AppColors.Danger;
            pnlClose.Cursor = Cursors.Hand;

            // الكارد الرئيسي
            pnlCard.Size = new Size(380, 480);
            pnlCard.Location = new Point(40, 55);
            pnlCard.FillColor = AppColors.PrimaryCard;
            pnlCard.BorderRadius = 20;
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.ShadowDecoration.Color = Color.FromArgb(0, 80, 200);
            pnlCard.ShadowDecoration.Depth = 20;

            lblTitle.Text = "IceCream Pro";
            lblTitle.ForeColor = AppColors.TextPrimary;
            lblTitle.Font = new Font("Segoe UI", 18f, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(100, 30);

            lblSub.Text = "نظام توزيع الآيس كريم";
            lblSub.ForeColor = AppColors.TextSecondary;
            lblSub.Font = new Font("Segoe UI", 10f);
            lblSub.AutoSize = true;
            lblSub.Location = new Point(95, 68);

            var lblUser = new Label
            {
                Text = "اسم المستخدم",
                ForeColor = AppColors.TextSecondary,
                Font = new Font("Segoe UI", 9f),
                AutoSize = true,
                Location = new Point(20, 110)
            };

            txtUsername.Size = new Size(340, 46);
            txtUsername.Location = new Point(20, 130);
            txtUsername.PlaceholderText = "أدخل اسم المستخدم";
            txtUsername.Font = new Font("Segoe UI", 11f);
            txtUsername.ForeColor = AppColors.TextPrimary;
            txtUsername.FillColor = AppColors.PrimaryDark;
            txtUsername.BorderColor = AppColors.BorderColor;
            txtUsername.BorderRadius = 10;
            txtUsername.BorderThickness = 1;
            txtUsername.TextAlign = HorizontalAlignment.Right;

            var lblPass = new Label
            {
                Text = "كلمة المرور",
                ForeColor = AppColors.TextSecondary,
                Font = new Font("Segoe UI", 9f),
                AutoSize = true,
                Location = new Point(20, 192)
            };

            txtPassword.Size = new Size(340, 46);
            txtPassword.Location = new Point(20, 212);
            txtPassword.PlaceholderText = "أدخل كلمة المرور";
            txtPassword.Font = new Font("Segoe UI", 11f);
            txtPassword.ForeColor = AppColors.TextPrimary;
            txtPassword.FillColor = AppColors.PrimaryDark;
            txtPassword.BorderColor = AppColors.BorderColor;
            txtPassword.BorderRadius = 10;
            txtPassword.BorderThickness = 1;
            txtPassword.PasswordChar = '●';
            txtPassword.TextAlign = HorizontalAlignment.Right;

            tglShow.Size = new Size(36, 18);
            tglShow.Location = new Point(20, 272);
            tglShow.CheckedState.FillColor = AppColors.AccentBlue;
            tglShow.UncheckedState.FillColor = AppColors.BorderColor;

            var lblShowPass = new Label
            {
                Text = "إظهار كلمة المرور",
                ForeColor = AppColors.TextSecondary,
                Font = new Font("Segoe UI", 9f),
                AutoSize = true,
                Location = new Point(62, 274)
            };

            lblError.Text = "";
            lblError.ForeColor = AppColors.Danger;
            lblError.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblError.AutoSize = true;
            lblError.Location = new Point(20, 302);

            btnLogin.Text = "تسجيل الدخول";
            btnLogin.Size = new Size(340, 48);
            btnLogin.Location = new Point(20, 328);
            btnLogin.FillColor = AppColors.AccentBlue;
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            btnLogin.BorderRadius = 12;
            btnLogin.Cursor = Cursors.Hand;

            // ─── بوكس تلميح الأدمن الافتراضي ─────────────────────────
            var pnlHint = new Panel
            {
                Size = new Size(340, 56),
                Location = new Point(20, 390),
                BackColor = Color.FromArgb(20, 0, 122, 255)  // أزرق شفاف
            };

            var lblHintTitle = new Label
            {
                Text = "🔑 بيانات الدخول الأولى (أمسح بعد التشغيل)",
                ForeColor = AppColors.AccentBlue,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(8, 6)
            };

            var lblHintCreds = new Label
            {
                Text = $"المستخدم: {DefaultAdminUser}     الباسورد: {DefaultAdminPass}",
                ForeColor = AppColors.TextSecondary,
                Font = new Font("Segoe UI", 8.5f),
                AutoSize = true,
                Location = new Point(8, 28)
            };

            pnlHint.Controls.AddRange(new Control[] { lblHintTitle, lblHintCreds });

            pnlCard.Controls.AddRange(new Control[]
            {
                lblTitle, lblSub,
                lblUser,  txtUsername,
                lblPass,  txtPassword,
                tglShow,  lblShowPass,
                lblError, btnLogin,
                pnlHint
            });

            Controls.AddRange(new Control[] { pnlCard, pnlClose });
        }

        // ═════════════════════════════════════════════════════════════
        //  ربط الأحداث
        // ═════════════════════════════════════════════════════════════
        private void WireEvents()
        {
            pnlClose.Click += (s, e) => Application.Exit();

            tglShow.CheckedChanged += (s, e)
                => txtPassword.PasswordChar = tglShow.Checked ? '\0' : '●';

            btnLogin.Click += BtnLogin_Click;

            txtPassword.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) BtnLogin_Click(s, e);
            };

            // تحريك الفورم بالسحب من أي مكان
            pnlCard.MouseDown += StartDrag;
            pnlCard.MouseMove += DoDrag;
            MouseDown += StartDrag;
            MouseMove += DoDrag;
        }

        private Point _drag;
        private void StartDrag(object? s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) _drag = new Point(e.X, e.Y);
        }
        private void DoDrag(object? s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Location = new Point(Location.X + e.X - _drag.X, Location.Y + e.Y - _drag.Y);
        }

        // ═════════════════════════════════════════════════════════════
        //  منطق تسجيل الدخول
        // ═════════════════════════════════════════════════════════════
        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "⚠ أدخل اسم المستخدم وكلمة المرور";
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "جاري التحقق...";

            try
            {
                var user = await UserService.Login(
                    txtUsername.Text.Trim(),
                    txtPassword.Text);

                if (user is null)
                {
                    lblError.Text = "✖ اسم المستخدم أو كلمة المرور غير صحيحة";
                    btnLogin.Enabled = true;
                    btnLogin.Text = "تسجيل الدخول";
                    return;
                }

                clsPL_Global.CurrentUser = user;
                new frmMain().Show();
                Hide();
            }
            catch (Exception ex)
            {
                lblError.Text = $"✖ خطأ في الاتصال بقاعدة البيانات:{Environment.NewLine}{ex.Message}";
                btnLogin.Enabled = true;
                btnLogin.Text = "تسجيل الدخول";
            }
        }
    }
}
