using IceCreamPro.Presentation;
using IceCreamPro.Presentation.Forms;

namespace Ice_Cream_Distribution_System
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            clsPL_Global.SubscribeToErrorOccurredEvent();
            Application.Run(new frmLogin());
        }
    }
}