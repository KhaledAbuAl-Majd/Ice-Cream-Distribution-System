namespace IceCreamPro.Presentation
{
    public static class clsPL_Utils
    {
        public static string FormatCurrency(decimal value) => value.ToString("N2");
        public static string FormatDate(DateTime date) => date.ToString("yyyy/MM/dd");
    }
}