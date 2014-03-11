// <copyright file="BasdeDialogManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System;
	using System.Windows.Forms;
	using TestCaseManagerCore.BusinessLogic.Enums;

	/// <summary>
    /// Common methods for all dialog class managers
    /// </summary>
	public class BasdeDialogManager
    {
		/// <summary>
		/// Gets the type of the file filters by file.
		/// </summary>
		/// <param name="dlg">The DLG.</param>
		/// <param name="fileType">Type of the file.</param>
		protected void GetFileFiltersByFileType(Microsoft.Win32.OpenFileDialog dlg, FileType fileType)
		{
			switch (fileType)
			{
				case FileType.DLL:
					dlg.DefaultExt = ".dll";
					dlg.Filter = "Assembly Files (*.dll)|*.dll";
					break;
				case FileType.JSON:
					dlg.DefaultExt = ".json";
					dlg.Filter = "JSON Files (*.json)|*.json";
					break;
				default:
					break;
			}
		}
    }
}
