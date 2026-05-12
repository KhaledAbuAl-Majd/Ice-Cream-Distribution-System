using Business;
using Guna.UI2.WinForms;
using Ice_Cream_Distribution_System.Models;
using static IceCreamPro.Presentation.FormHelpers;

namespace IceCreamPro.Presentation.Forms
{
    public class frmUsers : Form
    {
        private Guna2TextBox txtUsername = new(), txtPassword = new(),
                             txtName = new(), txtPhone = new();
        private Guna2Button btnSave = new(), btnDelete = new(), btnClear = new();
        private DataGridView dgv = new();
        private User? _selected;

        public frmUsers() { BuildUI(); WireEvents(); _ = LoadAsync(); }

        private void BuildUI()
        {
            BackColor = AppColors.PrimaryDark; RightToLeft = RightToLeft.Yes; RightToLeftLayout = true;
            Controls.Add(new Label { Text = "إدارة المستخدمين", ForeColor = AppColors.TextPrimary, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) });

            var card = new Guna2Panel { Size = new Size(420, 370), Location = new Point(20, 60), FillColor = AppColors.PrimaryCard, BorderRadius = 14 };
            StyleLabel(card, "اسم المستخدم", new Point(20, 20));
            txtUsername = StyleTextBox(card, "username", new Point(20, 42));
            StyleLabel(card, "كلمة المرور (للإضافة فقط)", new Point(20, 100));
            txtPassword = StyleTextBox(card, "••••••••", new Point(20, 122));
            txtPassword.PasswordChar = '●';
            StyleLabel(card, "الاسم الكامل", new Point(20, 180));
            txtName = StyleTextBox(card, "الاسم الكامل", new Point(20, 202));
            StyleLabel(card, "الهاتف", new Point(20, 260));
            txtPhone = StyleTextBox(card, "01XXXXXXXXX", new Point(20, 282));

            StyleButton(btnSave, "💾 حفظ", AppColors.AccentBlue, new Point(20, 330));
            StyleButton(btnDelete, "🗑 حذف", AppColors.Danger, new Point(148, 330));
            StyleButton(btnClear, "✖ مسح", AppColors.BorderColor, new Point(276, 330));
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
                if (dgv.CurrentRow?.DataBoundItem is not User u) return;
                _selected = u; txtUsername.Text = u.UserName; txtPassword.Text = "";
                txtName.Text = u.Person?.PersonName ?? "";
                txtPhone.Text = u.Person?.Phone ?? "";
            };
        }

        private async Task LoadAsync()
        {
            try { dgv.DataSource = await UserService.GetAll() ?? new(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnSave_Click(object? s, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) { clsPL_MessageBoxs.ShowErrorMessage("أدخل اسم المستخدم"); return; }
            try
            {
                if (_selected is null)
                {
                    if (string.IsNullOrWhiteSpace(txtPassword.Text)) { clsPL_MessageBoxs.ShowErrorMessage("أدخل كلمة المرور"); return; }
                    var user = new User { UserName = txtUsername.Text.Trim(), IsActive = true, Person = new Person { PersonName = txtName.Text.Trim(), Phone = txtPhone.Text.Trim() } };
                    await UserService.Add(user, txtPassword.Text);
                }
                else
                {
                    _selected.UserName = txtUsername.Text.Trim();
                    if (_selected.Person != null) { _selected.Person.PersonName = txtName.Text.Trim(); _selected.Person.Phone = txtPhone.Text.Trim(); }
                    await UserService.Update(_selected);
                }
                clsPL_MessageBoxs.ShowSuccessMessage("تم الحفظ"); ClearForm(); await LoadAsync();
            }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (_selected is null) { clsPL_MessageBoxs.ShowErrorMessage("اختر مستخدماً أولاً"); return; }
            if (clsPL_MessageBoxs.ShowConfirmMessage($"حذف '{_selected.UserName}'؟") != DialogResult.Yes) return;
            try { await UserService.Delete(_selected); clsPL_MessageBoxs.ShowSuccessMessage("تم الحذف"); ClearForm(); await LoadAsync(); }
            catch (Exception ex) { clsPL_MessageBoxs.ShowErrorMessage(ex.Message); }
        }

        private void ClearForm() { _selected = null; txtUsername.Text = txtPassword.Text = txtName.Text = txtPhone.Text = ""; }
    }
}