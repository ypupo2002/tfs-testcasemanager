   M S B A M L   `   `   `  ���� J  FTestCaseManager, Version=1.0.9.1, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TestCaseManagerApp.ViewsTestCaseManagerApp.Views  N JTestCaseManagerCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Wclr-namespace:TestCaseManagerCore.BusinessLogic.Converters;assembly=TestCaseManagerCore,TestCaseManagerCore.BusinessLogic.Converters 3    -TestCaseManagerApp.Views.TestCaseDetailedView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  2local&clr-namespace:TestCaseManagerApp.Views  econvertWclr-namespace:TestCaseManagerCore.BusinessLogic.Converters;assembly=TestCaseManagerCore  -   .���   � 	Resources  N  HTestCaseManagerCore.BusinessLogic.Converters.TestStepPropertiesConverter   TestStepPropertiesConverter��%   &	           J� CommandBindings )  #System.Windows.Input.CommandBinding  -   3   -TestCaseManagerApp.Views.TestCasesInitialView   EditCommand   Command# Z   -      DuplicateCommand# Z .+�� -   $��progressBar�� � Minimum$ 0Q� � Maximum$ 1Q�$��16�� � IsIndeterminate . $��0,0,0,16q�$y�Visible=�� -   $��mainGrid��$y�Hidden=� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� 	 �� Orientation$	 Vertical=�.+��� $��Preview Test Case��$��
10,0,10,30q�$
��Auto��$
��Auto��$��26(�$
��Bold"�$
��Left=�$��0���  �� $
��Auto ��� $
��Auto �
 � ColumnDefinitions
 �� $
��1.2* ��� $	��10* ��� $��5* ��� $��5* ��� $
��1.8* ��� $
��2.2* �.+��� $��ID��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��0���� $��	10,0,10,0q�$��25��$��Stretch=�$��1��$��0�� �� 
IsReadOnly . ���� *TestCase.ITestCase.Id+ �� Mode$ OneWay=��� $��Title��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��1���� $��	10,0,10,0q�$��25��$��Stretch=�$��1��$��1�� . ���� *TestCase.Title+$ OneWay=� � ToolTip �� *TestCase.Title+$ OneWay=��� $��Suite��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��2���� $��	10,0,10,0q�$��25��$��Stretch=�$��1��$��2�� . ���� *TestCase.ITestSuiteBase.Title+$ OneWay=��� $
��Area��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��3���� $��	10,0,10,0q�$��25��$��Stretch=�$��1��$��3�� . ���� *TestCase.Area+$ OneWay=��� $��Priority��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��4���� $��	10,0,10,0q�$��25��$��Stretch=�$��1��$��4�� . ���� *TestCase.Priority+$ OneWay=��� $��	Automated��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��5���� $��	10,0,10,0q�$��25��$��Stretch=�$��1��$��5�� . ���� * TestCase.ITestCase.IsAutomated+$ OneWay=�� -   $��gAssociatedAutomationInfo��$��	0,30,0,30q� �� $��* ��� $��* �
 �� $��* ��� $��* ��� $��* �.+��� $��Automated Test Type��$��	10,0,10,0q�$��25��$
��Bold"�$��Stretch=�$��0��$��0���� $��	10,0,10,0q�$��25��$
��Left=�$��1��$��0������ *AssociatedAutomation.Type+$ OneWay=��� $��Automated test storage��$��	10,0,10,0q�$��25��$
��Bold"�$��Stretch=�$��0��$��1���� $��	10,0,10,0q�$��25��$��Stretch=�$��1��$��1�� . ���� *AssociatedAutomation.Assembly+$ OneWay=��� $��Automated test name��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��2���� $��	10,0,10,0q�$��25��$��Stretch=�$��1��$��2�� . ���� *AssociatedAutomation.TestName+$ OneWay=�&   System.Windows.Controls.DataGrid  -   $��dgTestSteps��   AutoGenerateColumns .  $
��Auto��$��1��$��0,0,0,30q� %� 
ScrollUnit$ Pixel=�����  �� Path$ ObservableTestSteps�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Shared Step Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150    MaxWidth$	 200Q�   HeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"   �� BasedOn# [B .���  �� Property � �� �� Value$
 Left=�   CellTemplate �� .R���  �� TextWrapping$
 Wrap=����� *Title+  $ Steps��$ 5*  �� "  # [B .���  � ��$
 Left=� �� .R��� $
 Wrap=����� *ActionTitle+  $ Expected Results��$ 5*  �� "  # [B .���  � ��$
 Left=� �� .R��� $
 Wrap=����� *ActionExpectedResult+�� $	 
Horizontal=�$
��Left=�$��2��.+��� -   $��btnEdit��$��0,0,0,30q�$��30��$��90��$��_Edit��$��Center=��� -	   $��btnDuplicate��$��
40,0,10,30q�$��30��$��90��$��
_Duplicate��$��Center=��� $	 
Horizontal=�$��Right=�$��2��.+��� -
   $��btnPass��$��
40,0,10,30q�$��30��$��90��$��	Pass test��$��Center=��� -   $��btnFail��$��
40,0,10,30q�$��30��$��90��$��	Fail test��$��Center=��� -   $��btnBlock��$��
40,0,10,30q�$��30��$��90��$��
Block test��$��Center=�