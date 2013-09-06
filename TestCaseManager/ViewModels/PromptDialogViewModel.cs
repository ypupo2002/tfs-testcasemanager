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
    public class PromptDialogViewModel
              : NotifyPropertyChanged
    {
        public string ResponseText { get; set; }
    }
}

