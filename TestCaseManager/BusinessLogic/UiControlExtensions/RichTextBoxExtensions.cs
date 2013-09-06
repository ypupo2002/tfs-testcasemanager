using System;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TestCaseManagerApp
{
    public static class RichTextBoxExtensions
    {
        public static void RestoreDefaultSearchBoxText(this RichTextBox rtb, string defaultText, ref bool flag)
        {
            if (String.IsNullOrEmpty(rtb.GetText()))
            {
                rtb.AppendText(defaultText);
                flag = false;
            }
        }

        public static void ClearDefaultSearchBoxContent(this RichTextBox rtb, ref bool flag)
        {
            if (!flag)
            {
                rtb.Document.Blocks.Clear();
                flag = true;
            }
        }

        public static string GetText(this RichTextBox rtb)
        {
            string expectedResult = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text.TrimEnd();
            return expectedResult;
        }

        public static void SetText(this RichTextBox rtb, string textToAdd)
        {
            rtb.Document.Blocks.Clear();
            rtb.AppendText(textToAdd);
        }
    }
}
