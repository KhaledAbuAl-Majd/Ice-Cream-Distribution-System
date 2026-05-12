namespace IceCreamPro.Presentation
{
    public static class clsPL_MessageBoxs
    {
        public static void ShowErrorMessage(string message)
            => ShowMessage(message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void ShowSuccessMessage(string message)
            => ShowMessage(message, "تم بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static DialogResult ShowConfirmMessage(string message)
            => ShowMessage(message, "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        public static DialogResult ShowMessage(
            string message, string caption,
            MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            MessageBoxOptions options = 0;
            if (message.Any(c => c >= 0x0600 && c <= 0x06FF))
                options = MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;

            if (clsPL_Global.ActiveForm == null)
                return MessageBox.Show(message, caption, buttons, icon, defaultButton);

            DialogResult result = DialogResult.Cancel;
            clsPL_Global.ActiveForm.Invoke(() =>
            {
                result = MessageBox.Show(
                    clsPL_Global.ActiveForm, message, caption,
                    buttons, icon, defaultButton, options);
            });
            return result;
        }
    }
}