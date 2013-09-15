// <copyright file="TextReplacePair.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains old/new text pair used to change specific text
    /// </summary>
    public class TextReplacePair
    {
        /// <summary>
        /// Gets or sets the old text.
        /// </summary>
        /// <value>
        /// The old text.
        /// </value>
        public string OldText { get; set; }

        /// <summary>
        /// Gets or sets the new text.
        /// </summary>
        /// <value>
        /// The new text.
        /// </value>
        public string NewText { get; set; }
    }
}
