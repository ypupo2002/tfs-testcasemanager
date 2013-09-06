using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestCaseManagerApp
{
    public static class TextBoxExtensions
    {
        public static void RestoreDefaultSearchBoxText(this TextBox tb, string defaultText, ref bool flag)
        {
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = defaultText;
                flag = false;
            }
        }

        public static void ClearDefaultSearchBoxContent(this TextBox tb, ref bool flag)
        {
            if (!flag)
            {
                tb.Text = String.Empty;
                flag = true;
            }
        }
    }
}
