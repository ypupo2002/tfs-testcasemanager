// <copyright file="PieEntity.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.BusinessLogic.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Used in Pie Charts
	/// </summary>
	public class PieEntity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PieEntity"/> class.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <param name="number">The number.</param>
		public PieEntity(string category, int number)
		{
			this.Category = category;
			this.Number = number;
		}

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>
		/// The category.
		/// </value>
		public string Category { get; set; }

		/// <summary>
		/// Gets or sets the number.
		/// </summary>
		/// <value>
		/// The number.
		/// </value>
		public int Number { get; set; }		
	}
}