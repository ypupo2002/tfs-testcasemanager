// <copyright file="FileDialogManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System;
	using System.Windows.Forms;
	using Microsoft.Win32;
	using TestCaseManagerCore.BusinessLogic.Enums;

    /// <summary>
    /// Helps to get the path to specific file
    /// </summary>
	public class FileDialogManager : BasdeDialogManager
    {
		/// <summary>
		/// The instance
		/// </summary>
		private static FileDialogManager instance;

		/// <summary>
		/// Gets the intance.
		/// </summary>
		/// <value>
		/// The intance.
		/// </value>
		public static FileDialogManager Intance
		{ 
			get
			{
			   if(instance == null)
			   {
			       instance = new FileDialogManager();
			   }
			   return instance;
			}
		}

		/// <summary>
		/// Gets the name of the file.
		/// </summary>
		/// <returns></returns>
        public string GetFileName(FileType fileType)
        {
			Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
			base.GetFileFiltersByFileType(dialog, fileType);

			bool? result = dialog.ShowDialog();
			string resultFileName = String.Empty;
			if (result == true)
			{
				resultFileName = dialog.FileName;				
			}

			return resultFileName;
        }		
    }
}
