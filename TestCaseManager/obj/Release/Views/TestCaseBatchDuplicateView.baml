   M S B A M L   `   `   `  ���� N  JTestCaseManagerCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullfFclr-namespace:TestCaseManagerCore.Helpers;assembly=TestCaseManagerCoreTestCaseManagerCore.Helpers  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ~Rclr-namespace:TestCaseManagerCore.BusinessLogic.Enums;assembly=TestCaseManagerCore'TestCaseManagerCore.BusinessLogic.Enums   TestCaseManager9   3TestCaseManagerApp.Views.TestCaseBatchDuplicateView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  RlocalFclr-namespace:TestCaseManagerCore.Helpers;assembly=TestCaseManagerCore  3System&clr-namespace:System;assembly=mscorlib  ^enumsRclr-namespace:TestCaseManagerCore.BusinessLogic.Enums;assembly=TestCaseManagerCore  -   .��� .+�� -   $��progressBar��  � Minimum$  0Q� � Maximum$ 1Q�$��16�� � IsIndeterminate . $��0,0,0,16q�� -   $��mainGrid��$y�Hidden=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $��3* ��� $��1* ��� $��8* �.+��� $��Change | Duplicate��$��
10,0,10,30q�$
��Auto��$
��Auto��$
��Bold"�$��26(�$
��Left=�$��0����  �� Orientation$ Vertical=�$��0��$��1��$��2��.+��  � 	Resources    dataFromEnum S� 
MethodName S� 
ObjectType  System.Enum	 S� MethodParameters
 M� TypeName��%B   &	        S� $ 	GetValues��"  	 M� $
 enums:Priority�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $��4* ��� $��2* �.+��� $��	New Suite��$��10,0,0,0q�$��25��$
��Auto��$
��Left=�$��0��$��0��{��� *ShowTestCaseSpecificFields+�� -   $��cbSuite��$��25��$��	10,0,0,20q� �� SelectedIndex$ 0��$��Stretch=� �� DisplayMemberPath$ Title�� �� IsTextSearchEnabled . <   6TestCaseManagerCore.Helpers.ComboBoxDropdownExtensions    OpenDropDownAutomatically .  �� 
IsEditable . $��1��$��0������ *ObservableTestSuites+ �� SelectedValue ��  �� Path$" ReplaceContext.SelectedSuite� �� Mode$ TwoWay=�{��� *ShowTestCaseSpecificFields+�� $��Priority��$��10,0,0,0q�$��25��$
��Auto��$
��Left=�$��0��$��1���� -   $��
cbPriority��$��25��$��	10,0,0,20q�$ 0��$��Stretch=� .  .  . $��1��$��1������  �� Source# [   �� $% ReplaceContext.SelectedPriority�$ TwoWay=��� $��Assigned To��$��10,0,0,0q�$��25��$
��Auto��$
��Left=�$��2��$��0���� -   $#��cbTeamFoundationIdentityNames��$��25��$��	10,0,0,20q�$ 0��$��Stretch=�$ DisplayName�� .  .  . $��3��$��0������ *'%ObservableTeamFoundationIdentityNames+ �� $7 1ReplaceContext.SelectedTeamFoundationIdentityName�$ TwoWay=��� $ Vertical=�$��0��$��2��$��2��.+��� $ 
Horizontal=�.+��  �� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Replace In Titles:��$��
10,0,10,10q�$��25��$
��Auto��$
��Left=�$��0��$��0���� $��Replace In Test Steps:��$��
10,0,10,10q�$��25��$
��Auto��$
��Left=�$��0��$��1���� $��Replace Shared Steps:��$��
10,0,10,30q�$��25��$
��Auto��$
��Left=�$��0��$��2��{��� *ShowTestCaseSpecificFields+�� $��
10,0,10,10q�$
��Left=�$��1��$��0�� d� 	IsChecked �� $$ ReplaceContext.ReplaceInTitles�$ TwoWay=��� $��
10,0,10,10q�$
��Left=�$��1��$��1�� �� $' !ReplaceContext.ReplaceInTestSteps�$ TwoWay=��� $��
10,0,10,30q�$
��Left=�$��1��$��2�� �� $' !ReplaceContext.ReplaceSharedSteps�$ TwoWay=�{��� *ShowTestCaseSpecificFields+�  �� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Change Priorities��$��
10,0,10,10q�$��25��$
��Auto��$
��Left=�$��0��$��0���� $��Change Owner��$��
10,0,10,10q�$��25��$
��Auto��$
��Left=�$��0��$��1���� $��
10,0,10,10q�$
��Left=�$��1��$��0�� �� $% ReplaceContext.ChangePriorities�$ TwoWay=��� $��
10,0,10,10q�$
��Left=�$��1��$��1�� �� $  ReplaceContext.ChangeOwner�$ TwoWay=�&   System.Windows.Controls.DataGrid  -   $��dgSharedStepIds��   CanUserAddRows .    AutoGenerateColumns .  $��3��$��0��$��2������ *31ReplaceContext.ObservableSharedStepIdReplacePairs+{��� *ShowTestCaseSpecificFields+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Old Shared Step ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *OldSharedStepId+   HeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"   �� BasedOn# [B .���  �� Property � �� �� Value$
 Left=�  $ New Shared Step ID��$ *  �� *NewSharedStepIds+ �� "  # [B .���  � ��$
 Left=��� $��5��  �� ResizeDirection$
  Auto=�$��Stretch=�$��Stretch=�$��4��$��0��$��2��  -   $��dgTextsToBeReplaced�� .  .  $��5��$��0��$��2������ *+)ReplaceContext.ObservableTextReplacePairs+   $ Old Text��$ *  �� *	OldText+ �� "  # [B .���  � ��$
 Left=�  $ New Text��$ *  �� *	NewText+ �� "  # [B .���  � ��$
 Left=�� $��0��$��2��$��	10,0,0,10q� �� $��5* ��� $��3* ��� $��2* ��� $��* �.+��� -	   $��tbTitleFilter��$��	10,0,0,10q�$��25��$��0������ * InitialViewFilters.TitleFilter+$ TwoWay=�! �� UpdateSourceTrigger$! PropertyChanged=��� -
   $��tbSuiteFilter��$��	10,0,0,10q�$��25��$��1������ * InitialViewFilters.SuiteFilter+$ TwoWay=�$! PropertyChanged=�{��� *ShowTestCaseSpecificFields+�� -   $��tbAssignedToFilter��$��	10,0,0,10q�$��25��$��2������ *%#InitialViewFilters.AssignedToFilter+$ TwoWay=�$! PropertyChanged=��� -   $��tbPriorityFilter��$��	10,0,0,10q�$��25��$��3������ *#!InitialViewFilters.PriorityFilter+$ TwoWay=�$! PropertyChanged=�  -   $��dgTestCases�� .  "   
IsReadOnly" . #   SelectionMode$# Extended=�$��1��$��5��$��2��$��
10,10,0,10q����� )	  #System.Windows.Controls.DataGridRow" 	 # [B	 .�-      $ Title��$ 5*  �� *Title+ �� "  # [B .���  � ��$
 Left=�  $ Suite��$ 3*  �� *ITestSuiteBase.Title+ �� "  # [B .���  � ��$
 Left=�  $ Assigned To��$ 2*  �� *(&TeamFoundationIdentityName.DisplayName+ �� "  # [B .���  � ��$
 Left=�  $ Priority��$ *  �� *
Priority+ �� "  # [B .���  � ��$
 Left=��� $ 
Horizontal=�$
��Left=�$��6��$��0��$��2��.+��� -   $��btnBatchDuplicate��$��	0,20,10,0q�$��30��$	��150��$��Batch Duplicate��$
��Left=�$��Center=�$��3��$��0���� -   $��btnFindAndReplace��$��
10,20,10,0q�$��30��$	��150��$��Find And Replace��$
��Left=�$��Center=�$��3��$��0���� $ 
Horizontal=�$
��Left=�$��6��$��2��.+��� $��Selected Count:��$��	0,20,10,0q�$��25��$
��Auto��$
��Bold"��� -   $��tbSelectedTestCaseCount��$��
10,20,30,0q�$��25��$
��Auto������ *SelectedEntitiesCount+�� $��Count:��$��	0,20,10,0q�$��25��$
��Auto��$
��Bold"��� -   $��tbTestCaseCount��$��
10,20,10,0q�$��25��$
��Auto������ *EntitiesCount+