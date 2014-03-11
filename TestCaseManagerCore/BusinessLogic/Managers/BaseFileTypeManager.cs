// <copyright file="BaseFileTypeManager.cs" company="CodePlex">
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
	public static class BaseFileTypeManager
    {
		/// <summary>
		/// Gets the type of the file filters by file.
		/// </summary>
		/// <param name="dlg">The DLG.</param>
		/// <param name="fileType">Type of the file.</param>
		public static string GetFileFiltersByFileType(FileType fileType)
		{
			string currentFileFilter = String.Empty;
			switch (fileType)
			{
				case FileType.DLL:
					currentFileFilter = "Assembly Files (*.dll)|*.dll";
					break;
				case FileType.JSON:
					currentFileFilter = "JSON Files (*.json)|*.json";
					break;
			}

			return currentFileFilter;
		}

		/// <summary>
		/// Gets the type of the extension by file.
		/// </summary>
		/// <param name="fileType">Type of the file.</param>
		public static string GetExtensionByFileType(FileType fileType)
		{
			string currentExtension = String.Empty;
			switch (fileType)
			{
				case FileType.DLL:
					currentExtension = ".dll";
					break;
				case FileType.JSON:
					currentExtension = ".json";
					break;
			}
			return currentExtension;
		}
    }
}
