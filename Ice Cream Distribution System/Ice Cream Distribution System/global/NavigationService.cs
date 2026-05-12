namespace IceCreamPro.Presentation
{
    public static class NavigationService
    {
        private static Panel? _container;
        private static Form? _current;

        public static void Initialize(Panel container)
            => _container = container;

        public static void NavigateTo(Form form)
        {
            if (_container == null) return;
            _current?.Close();
            _current?.Dispose();
            _current = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            _container.Controls.Clear();
            _container.Controls.Add(form);
            form.Show();
            clsPL_Global.ActiveForm = form;
        }
    }
}