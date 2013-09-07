using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCaseManagerApp.BusinessLogic.Entities;

namespace TestCaseManagerApp
{
    /// <summary>
    /// Contains helper methods for replacing multiple text pairs in specific text
    /// </summary>
    public static class ReplaceTextManager
    {
        /// <summary>
        /// Replaces all specified text pairs.
        /// </summary>
        /// <param name="textToReplace">The text to be replaced.</param>
        /// <param name="textReplacePairs">The text replace pairs.</param>
        /// <returns>replaced text</returns>
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
