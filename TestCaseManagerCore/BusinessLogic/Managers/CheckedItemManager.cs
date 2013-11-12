// <copyright file="CheckedItemManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TestCaseManagerCore.BusinessLogic.Entities;

    /// <summary>
    /// Used to transform observable checked items to list of strings
    /// </summary>
    public static class CheckedItemManager
    {
        /// <summary>
        /// Automatics the list.
        /// </summary>
        /// <param name="checkedItems">The checked items.</param>
        /// <returns>list of string representation of all checked items</returns>
        public static List<string> ToList(this ObservableCollection<CheckedItem> checkedItems)
        {
            List<string> selectedDescriptions = new List<string>();
            foreach (var item in checkedItems)
            {
                if (item.Selected)
                {
                    selectedDescriptions.Add(item.Description);
                }
            }

            return selectedDescriptions;
        }
    }
}
