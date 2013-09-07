using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp.ViewModels
{
    /// <summary>
    /// Holds PromptDialogView Properties
    /// </summary>
    public class PromptDialogViewModel: NotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the response text.
        /// </summary>
        /// <value>
        /// The response text.
        /// </value>
        public string ResponseText { get; set; }
    }
}

