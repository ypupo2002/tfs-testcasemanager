using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TestCaseManagerApp.Helpers
{
    public static class ComboBox_DropdownBehavior
    {
        /// <summary>
        /// Gets/sets whether or not the ComboBox this behavior is applied to opens its items-popup
        /// when the mouse hovers over it and closes again when the mouse leaves.
        /// </summary>
        public static readonly DependencyProperty OpenDropDownAutomaticallyProperty =
                 DependencyProperty.RegisterAttached(
                 "OpenDropDownAutomatically",
                 typeof(bool),
                 typeof(ComboBox_DropdownBehavior),
                 new UIPropertyMetadata(false, OnOpenDropDownAutomatically_Changed)
             );

        //DP-getter and -setter
        public static bool GetOpenDropDownAutomatically(ComboBox cbo)
        {
            return (bool)cbo.GetValue(OpenDropDownAutomaticallyProperty);
        }
        public static void SetOpenDropDownAutomatically(ComboBox cbo, bool value)
        {
            cbo.SetValue(OpenDropDownAutomaticallyProperty, value);
        }

        /// <summary>
        /// Fired when the assignment of the behavior changes (IOW, is being turned on or off).
        /// </summary>
        static void OnOpenDropDownAutomatically_Changed(
                DependencyObject doSource,
                DependencyPropertyChangedEventArgs e
            )
        {
            //The ComboBox that is the target of the assignment
            ComboBox cbo = doSource as ComboBox;
            if (cbo == null)
                return;

            //Just to be safe ...
            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
            {
                //Attach
                cbo.MouseMove += cbo_MouseMove;
                cbo.MouseEnter += cbo_MouseEnter;
            }
            else
            {
                //Detach
                cbo.MouseMove -= cbo_MouseMove;
                cbo.MouseEnter -= cbo_MouseEnter;
            }
        }

        public static void cbo_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Get a ref to the ComboBox
            ComboBox cbo = (ComboBox)sender;
            //Get a ref to the ComboBox'es popup (which is what displays the available items)
            Popup p = (Popup)cbo.Template.FindName("PART_Popup", cbo);

            //The DropDown/popup is to close when 
            // - it is still open
            // - the mouse is no longer over the popup
            // - the cbo's IsMouseDirectlyOver returns true (which, albeit strange, is true
            //   when the mouse is neither over the popup NOR the cbo itself
            if (cbo.IsDropDownOpen && !p.IsMouseOver && cbo.IsMouseDirectlyOver)
                cbo.IsDropDownOpen = false;
        }

        static void cbo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Open the DropDown/popup as soon as the mouse hovers over the control
            ((ComboBox)sender).IsDropDownOpen = true;
        }
    }
}
