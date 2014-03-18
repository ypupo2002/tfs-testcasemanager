// <copyright file="ReportingViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.TeamFoundation.TestManagement.Client;
	using TestCaseManagerCore.BusinessLogic.Entities;
	using TestCaseManagerCore.BusinessLogic.Managers;

	/// <summary>
	/// Contains methods and properties related to the Reporting View
	/// </summary>
	public class ReportingViewModel
	{
		/// <summary>
		/// The selected item
		/// </summary>
		private object selectedItem = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportingViewModel"/> class.
		/// </summary>
		public ReportingViewModel()
		{
			this.TestCasesCounts = new ObservableCollection<PieEntity>();
			this.TestCasesCounts.Add(new PieEntity("Automated Test Cases", 100));
			this.TestCasesCounts.Add(new PieEntity("Not Automated Test Cases", 20));
		}

		/// <summary>
		/// Gets or sets the test cases counts.
		/// </summary>
		/// <value>
		/// The test cases counts.
		/// </value>
		public ObservableCollection<PieEntity> TestCasesCounts { get; set; }

		/// <summary>
		/// Gets or sets the selected item.
		/// </summary>
		/// <value>
		/// The selected item.
		/// </value>
		public object SelectedItem
		{
			get
			{
				return this.selectedItem;
			}

			set
			{
				this.selectedItem = value;
			}
		}
	}
}