// <copyright file="ClipBoardCommand.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.BusinessLogic.Enums
{
    /// <summary>
    /// Contains the two types of clipboard operations: Copy and Paste
    /// </summary>
    public enum ClipBoardCommand
    {
        /// <summary>
        /// The copy command
        /// </summary>
        Copy,

        /// <summary>
        /// The cut command
        /// </summary>
        Cut,

        /// <summary>
        /// The none
        /// </summary>
        None
    }
}
