using Ice_Cream_Distribution_System.Models;
using MoneyMindManagerGlobal;

namespace IceCreamPro.Presentation
{
    public static class clsPL_Global
    {
        public static User? CurrentUser { get; set; }
        public static Form? ActiveForm { get; set; }

        public static void SubscribeToErrorOccurredEvent()
        {
            clsGlobalEvents.OnErrorOccured += clsPL_MessageBoxs.ShowErrorMessage;
        }
    }
}