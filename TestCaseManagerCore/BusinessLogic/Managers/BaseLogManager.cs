// <copyright file="BaseLogManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Managers
{
	using System;
	using System.IO;
	using TestCaseManagerCore.BusinessLogic.Entities;
	using TestCaseManagerCore.BusinessLogic.Enums;

	/// <summary>
	/// Provides base logic for all log managers
	/// </summary>
	public class BaseLogManager
	{
		/// <summary>
		/// The full result file path
		/// </summary>
		public string FullResultFilePath { get; set; }

		/// <summary>
		/// Initializes the log files.
		/// </summary>
		public void Initialize(FileType fileType, string resultsFilePrefix, string resultsFolder)
		{
			string fileExtension = BaseFileTypeManager.GetExtensionByFileType(fileType);
			FullResultFilePath = String.Concat(resultsFolder, resultsFilePrefix, DateTime.Now.ToString(DateTimeFormats.DateTimeShortFileFormat), fileExtension);
		}

		/// <summary>
		/// Opens the config in notepad.
		/// </summary>
		/// <param name="configPath">The config path.</param>
		public void OpenConfigInNotepad(string configPath)
		{
			string notepadPlusPath = @"C:\Program Files (x86)\Notepad++\notepad++.exe";
			string notepadPath = File.Exists(notepadPlusPath) ? notepadPlusPath : "notepad";
			string configArg = File.Exists(notepadPlusPath) ? String.Format("{0} -lxml", configPath) : configPath;
			System.Diagnostics.ProcessStartInfo procStartInfo =
				new System.Diagnostics.ProcessStartInfo(notepadPath, configArg);
			procStartInfo.RedirectStandardOutput = true;
			procStartInfo.UseShellExecute = false;
			procStartInfo.CreateNoWindow = true;
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.StartInfo = procStartInfo;
			proc.Start();
			string result = proc.StandardOutput.ReadToEnd();
		}
	}
}
