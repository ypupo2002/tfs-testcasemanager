   M S B A M L   `   `   `  ���� N  JTestCaseManagerCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullfFclr-namespace:TestCaseManagerCore.Helpers;assembly=TestCaseManagerCoreTestCaseManagerCore.Helpers   TestCaseManager0   *TestCaseManagerApp.Views.AssociateTestView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  ThelpersFclr-namespace:TestCaseManagerCore.Helpers;assembly=TestCaseManagerCore  .��� .+�� -   $��mainGrid��  � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+���  �� Orientation$ Vertical=�.+��� $��Associate Automation��$��
10,0,10,30q�$
��Auto��$
��Bold"�$��26(�$
��Left=�$��0��$��2���� $��
10,0,10,30q�$��25��$
��Bold"�$��18(�$
��Left=�$��0��$��2������ *TestCase.ITestCase.Title+� $��1�� � ColumnDefinitions �� $��* ��� $��* ��� $��* �.+��� -   $��
tbFullName��$��
10,0,10,10q�$��25��$��0��$��Stretch=����� *)'AssociateTestViewFilters.FullNameFilter+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� -   $��tbClassName��$��
10,0,10,10q�$��25��$��1��$��Stretch=����� **(AssociateTestViewFilters.ClassNameFilter+$ TwoWay=�$ PropertyChanged=��� -   $��
cbTestType��$��25��$��
10,0,10,10q�<   6TestCaseManagerCore.Helpers.ComboBoxDropdownExtensions    OpenDropDownAutomatically . $��2��$��Stretch=����� *	TestTypes+&   System.Windows.Controls.DataGrid  -   $��dgTests��   AutoGenerateColumns .  $��2��   
IsReadOnly .    SelectionMode$ Single=����� *ObservableTests+���� 	 �� 
TargetType)  #System.Windows.Controls.DataGridRow"	  
 �� BasedOn#
 [B .�-      Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ FullName��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
FullName+  $ 	ClassName�� �� *	ClassName+�� $ Vertical=�$��3��$��	0,30,0,30q�.+��� $ 
Horizontal=�$
��Left=�.+��� -   $��btnAssociate��$��0,0,10,0q�$��30��$��90��$��Center=����� B� .�A
ssociate