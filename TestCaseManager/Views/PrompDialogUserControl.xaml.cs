using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    public partial class PrompDialogUserControl : UserControl
    {
        public PrompDialogUserControl()
        {
            InitializeComponent();            
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            ExecutionContext.SharedStepTitle = tbSharedStepTitle.Text;
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ExecutionContext.SharedStepTitle = string.Empty;
            ExecutionContext.SharedStepTitleDialogCancelled = true;
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void tbSharedStepTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSharedStepTitle.Text))
            {
                btnOk.IsEnabled = true;
            }
            else
            {
                btnOk.IsEnabled = false;
            }
        }
    }
}
