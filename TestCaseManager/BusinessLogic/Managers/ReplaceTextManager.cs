using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCaseManagerApp.BusinessLogic.Entities;

namespace TestCaseManagerApp
{
    public static class ReplaceTextManager
    {
        public static string ReplaceAll(this string textToReplace, List<TextReplacePair> textReplacePairs)
        {
            string newText = textToReplace ?? String.Empty;
            foreach (TextReplacePair currentPair in textReplacePairs)
            {
                newText = newText.Replace(currentPair.OldText, currentPair.NewText);
            }

            return newText;
        }
    }
}
