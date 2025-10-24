using GOIVF.Class;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
namespace GOIVF.Class
{
    static class GOIVFVariables
    {
        //UserID is accessible from anywhere
        public static string UserID;
        //UserName is accessible from anywhere
        public static string UserName;
        //UserType is accessible from anywhere
        public static string UserType;
        // 
        public static string TEMP_INVOICE_NO;
        /// <summary>
        /// Changes the language of a form by reloading it with the selected culture.
        /// </summary>
        /// <param name="form">The form to apply the language change to.</param>
        /// <param name="langCode">The language code, e.g., "en" or "ar".</param>
        public static void SetFormLanguage(Form form, string langCode)
        {
            if (string.IsNullOrWhiteSpace(langCode)) return;

            // Set culture
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCode);

            // Handle RightToLeft layout for Arabic or RTL languages
            bool isRTL = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
            form.RightToLeft = isRTL ? RightToLeft.Yes : RightToLeft.No;
            form.RightToLeftLayout = isRTL;

            // Re-load all UI components with new resources
            form.Controls.Clear();
            
        }

    }


}