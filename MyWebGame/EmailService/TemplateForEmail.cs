using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam
{
    public static class TemplateForEmail
    {
        public static string Registration(string url)
        {
           return string.Format(@Resources.Web.ForConfirmedEmail +
                            "<a href=\"{0}\" title=\" " + @Resources.Web.ConfirmRegistrationTitle + "\">{0}</a>",
                url);
        }
        public static string ForgotPassword(string url)
        {
            return string.Format(@Resources.Web.ForForgotPassword +
                            "<a href=\"{0}\" title=\" " + @Resources.Web.ChangePasswordTheme + "\">{0}</a>",
                url);
        }
    }
}