using Guna.UI2.WinForms;

namespace IceCreamPro.Presentation
{
    public static class FormHelpers
    {
        public static void StyleLabel(Control parent, string text, Point loc)
        {
            parent.Controls.Add(new Label
            {
                Text = text,
                ForeColor = AppColors.TextSecondary,
                Font = new Font("Segoe UI", 9f),
                AutoSize = true,
                Location = loc
            });
        }

        public static Guna2TextBox StyleTextBox(Control parent, string placeholder, Point loc)
        {
            var tb = new Guna2TextBox
            {
                Size = new Size(340, 46),
                Location = loc,
                PlaceholderText = placeholder,
                Font = new Font("Segoe UI", 10f),
                ForeColor = AppColors.TextPrimary,
                FillColor = AppColors.PrimaryDark,
                BorderColor = AppColors.BorderColor,
                BorderRadius = 10,
                BorderThickness = 1,
                TextAlign = HorizontalAlignment.Right
            };
            parent.Controls.Add(tb);
            return tb;
        }

        public static void StyleButton(Guna2Button btn, string text, Color color, Point loc)
        {
            btn.Text = text;
            btn.Size = new Size(116, 38);
            btn.Location = loc;
            btn.FillColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.BorderRadius = 10;
            btn.Cursor = Cursors.Hand;
        }

        public static void StyleGrid(DataGridView dgv, Point loc, Size size)
        {
            dgv.Location = loc;
            dgv.Size = size;
            dgv.BackgroundColor = AppColors.PrimaryCard;
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.DefaultCellStyle.BackColor = AppColors.PrimaryCard;
            dgv.DefaultCellStyle.ForeColor = AppColors.TextPrimary;
            dgv.DefaultCellStyle.SelectionBackColor = AppColors.AccentBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = AppColors.PrimarySidebar;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = AppColors.TextSecondary;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = AppColors.BorderColor;
            dgv.RowTemplate.Height = 40;
            dgv.ScrollBars = ScrollBars.Both;
            dgv.Dock = DockStyle.Fill;
        }

        public static void StyleGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = AppColors.PrimaryCard;
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // ... باقي ألوان الـ DefaultCellStyle والـ HeaderCellStyle اللي إنت عاملها ...
            dgv.ScrollBars = ScrollBars.Both;
        }
    }
}