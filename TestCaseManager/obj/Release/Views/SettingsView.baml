   M S B A M L   `   `   `  ���� N  JTestCaseManagerCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullfFclr-namespace:TestCaseManagerCore.Helpers;assembly=TestCaseManagerCoreTestCaseManagerCore.Helpers   TestCaseManager%   TestCaseManagerApp.SettingsView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  RlocalFclr-namespace:TestCaseManagerCore.Helpers;assembly=TestCaseManagerCore  .��� .+��� .���� -     ��Name$  Panel��$	��150��.+��� $��Settings��$
��Bold"�$��0,0,0,8q�$
��Auto��$��26(��� $��0,0,0,16q����� *AccentColors+ �� SelectedItem �� *SelectedAccentColor+ �� Mode$ TwoWay=����� .R�� ���� *ActualWidth+ �� ElementName$ Panel������ .R��� $��40��$��40��$��2q����� 	Brush�� �� Color �� � $
��Left=� � ColumnDefinitions �� $
��Auto ��� $	��120�� � RowDefinitions �� �� .+��� $��Theme:��$��Center=�$��0,0,16,4q��� -   $��cbThemes��$��1�� �� DisplayMemberPath$ DisplayName��$��Center=�<   6TestCaseManagerCore.Helpers.ComboBoxDropdownExtensions    OpenDropDownAutomatically . $��0,0,0,4q����� *Themes+ �� *SelectedTheme+$ TwoWay=�� $��2�� �� $
��Auto ��� $
��Auto ��� $
��Auto � �� $��* ��� $��3* �.+��� $+��%Associate Automation Project Dll Path��$
��Bold"�$��12(�$��10q�$��0���� 	 �� Orientation$	 
Horizontal=�$��1��$��0��.+��� -   $ ��tbAssociatedProjectDllPath��$��10q�$	��380���� -   $��	btnBrowse��$��Browse��$��10q�$	��100��$��24���� $��Hover Behavior Drop Drown��$��10q�$��25��$
��Auto��$
��Left=�$��1���� -   $��10q�$
��Left=�$��1��$��1��
 d� 	IsChecked
 ��  �� Path$ HoverBehaviorDropDown�$ TwoWay=��� $��Show Comment Window��$��10q�$��25��$
��Auto��$
��Left=�$��2���� -   $��10q�$
��Left=�$��1��$��2��
 �� $ ShouldCommentWindowShow�$ TwoWay=�