   M S B A M L   `   `   `  ���� J  FTestCaseManager, Version=1.0.9.1, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TestCaseManagerApp.ViewsTestCaseManagerApp.Views  N JTestCaseManagerCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Uclr-namespace:TestCaseManagerCore.BusinessLogic.Entities;assembly=TestCaseManagerCore*TestCaseManagerCore.BusinessLogic.Entities �Wclr-namespace:TestCaseManagerCore.BusinessLogic.Converters;assembly=TestCaseManagerCore,TestCaseManagerCore.BusinessLogic.Converters 3    -TestCaseManagerApp.Views.TestCasesInitialView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  2local&clr-namespace:TestCaseManagerApp.Views  dentitiesUclr-namespace:TestCaseManagerCore.BusinessLogic.Entities;assembly=TestCaseManagerCore  econvertWclr-namespace:TestCaseManagerCore.BusinessLogic.Converters;assembly=TestCaseManagerCore  -   $��TestCaseInitialView��.���   � 	Resources  N  HTestCaseManagerCore.BusinessLogic.Converters.EnumMatchToBooleanConverter   enumBooleanConverter��%   &	           J� CommandBindings )  #System.Windows.Input.CommandBinding  -   3   -TestCaseManagerApp.Views.TestCasesInitialView   EditCommand   Command# Z   -      DuplicateCommand# Z   -      PreviewCommand# Z   -      
NewCommand# Z   -      CopyTestCasesCommand# Z   -      CutTestCasesCommand# Z   -   	   PasteTestCasesCommand# Z	   -	   
   RemoveCommand# Z
   -
   %   RemoveTestCaseFromSuiteCommand# Z   -      RenameSuiteCommand# Z   -      AddSuiteCommand# Z   -      RemoveSuiteCommand# Z   -      CopySuiteCommand# Z   -      CutSuiteCommand# Z   -      PasteSuiteCommand# Z   -      ExportTestCasesCommand# Z   -   !   SetBlockedTestCasesCommand# Z   -       SetFailedTestCasesCommand# Z   -       SetActiveTestCasesCommand# Z   -       SetPassedTestCasesCommand# Z .+�� -   $��progressBar�� � Minimum$ 0Q� � Maximum$ 1Q�$��16�� � IsIndeterminate . $��0,0,0,16q�� -   $��mainGrid��$y�Hidden=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $��* �$��65���� $
��Auto ��� $��6* �$	��800��.+���  �� Orientation$ 
Horizontal=�$��2��.+��� $��Search Test Cases��$��	0,0,10,15q�$
��Auto��$
��Auto��$
��Bold"�$��26(�$
��Left=�$��0���� $ Vertical=�$��1��$��2��.+��� $ Vertical=�$��3��$��2��$��3��$��	10,0,0,15q�.+��� $ 
Horizontal=�$
��Left=�$��	10,0,0,10q�.+��� $��Selected Suite ID: ��$
��Bold"�$��	10,0,10,0q�$
��Auto��$
��Auto��$
��Left=��� -   $��	tbSuiteId��$��
10,0,10,10q�$
��Auto��$��50��$
��Left=� �� 
IsReadOnly . �� -   $��spExecutionStatuses��$ 
Horizontal=�$
��Left=�$��0,0,0,10q�.+�	� -   $	��All��$��	20,0,10,0q�$
��Bold"�$��12(� 	� 	GroupName$ Options1�� d� 	IsChecked ��   �� Path$"  CurrentExecutionStatusOption�! �� Mode$! TwoWay=�" �� 	Converter#" [  # �� ConverterParameter$	# All��	� -   $��Active��$��	15,0,10,0q�$
��Bold"�$��12(�$ Options2�� �� $"  CurrentExecutionStatusOption�$! TwoWay=�#" [  $# Active��	� -   $��Passed��$��	15,0,10,0q�$
��Bold"�$��12(�$ Options3�� �� $"  CurrentExecutionStatusOption�$! TwoWay=�#" [  $# Passed��	� -   $��Failed��$��	15,0,10,0q�$
��Bold"�$��12(�$ Options4�� �� $"  CurrentExecutionStatusOption�$! TwoWay=�#" [  $# Failed��	� -   $��Blocked��$��	15,0,10,0q�$
��Bold"�$��12(�$ Options5�� �� $"  CurrentExecutionStatusOption�$! TwoWay=�#" [  $# Blocked���� $ 
Horizontal=�$
��Left=�.+��� -   $��btnPreview1��$��_Preview��$��	20,0,10,0q�$��30��$��90��$��Center=��� -    $��btnEdit1��$��_Edit��$��	15,0,10,0q�$��30��$��90��$��Center=��� -!   $��btnDuplicate1��$��
_Duplicate��$��	15,0,10,0q�$��30��$��90��$��Center=��� -"   $��btnNew1��$
��_New��$��	15,0,10,0q�$��30��$��90��$��Center=��� -#   $��btnRemoveTestCase1��$��_Remove��$��	15,0,10,0q�$��30��$��90��$��Center=��� -$   $��	btnExport��$��_Export��$��	15,0,10,0q�$��30��$��90��$��Center=��� -%   $��
btnArrange��$��Arrange��$��	15,0,10,0q�$��30��$
��Auto��$��Center=��� -&   $��btnChangeTestCases1��$��Change Test Cases��$��	30,0,10,0q�$��30��$
��Auto��$��Center=��� -'   $!��btnShowTestCaseWithoutSuite��$#��Show Test Cases Without Suite��$��	15,0,10,0q�$��30��$
��Auto��$��Center=��� $ 
Horizontal=�$
��Left=�$��
20,10,0,10q�.+��� -(   $��cbShowSubsuiteTestCases��$��0,0,10,0q�$��Center=�$
��Left=� �� $  ShowSubSuitesTestCases�$! TwoWay=��� $"��Show Test Cases in Subsuites��$��	10,0,10,0q�$
��Auto��$
��Auto��$
��Left=��  �� $
��0.9* ��� $��6* ��� $��* ��� $
��1.2* ��� $
��1.4* �.+��� -)   $��
tbIdFilter��$��0��$��	30,0,0,10q�$��25��$��0������ *InitialViewFilters.IdFilter+$! TwoWay=�$ �� UpdateSourceTrigger$$ PropertyChanged=��� -*   $��tbTitleFilter��$��1��$��	10,0,0,10q�$��25��$��1������ * InitialViewFilters.TitleFilter+$! TwoWay=�$$ PropertyChanged=��� -+   $��tbPriorityFilter��$��	10,0,0,10q�$��25��$��3��$��2������ *#!InitialViewFilters.PriorityFilter+$! TwoWay=�$$ PropertyChanged=��� -,   $��tbAssignedToFilter��$��	10,0,0,10q�$��25��$��4��$��3������ *%#InitialViewFilters.AssignedToFilter+$! TwoWay=�$$ PropertyChanged=��� $ 
Horizontal=�$��4��.+��� --   $��cbHideAutomated��$��
10,0,10,10q�$��Center=�$
��Left=� �� $  HideAutomated�$! TwoWay=��� $��Hide Automated��$��
0,10,10,10q�$��25��$
��Auto��$
��Left=�$��Bottom=�$��12(�R� -.   $��tvSuites��$��1��$��0��$��2��% R� SelectedValuePath$% Id��  	SplitLeft#��[ ���� *Suites+���� & �� 
TargetType"& Q�' �� BasedOn#' [BQ�.��� ( �� Property( �@Q�
IsExpanded) �� Value) �� $  IsNodeExpanded�$! TwoWay=��� ( �@Q�
IsSelected) �� $  
IsSelected�$! TwoWay=�-/   �� ( �@�ContextMenu) �� .@�m� * m� Command#* Z $	��Add��{��� $  IsAddEnabled��� m� #* Z $	��Cut��{��� $  IsCutEnabled�m� #* Z $
��Copy��{��� $  IsCopyEnabled�m� #* Z $��Paste��{��� $  IsPasteEnabled�m� #* Z $��Remove��{��� $  IsRemoveEnabled��� m� #* Z $��Rename��{��� $  IsRenameEnabled�+ J� InputBindings+ �� 
, �� Key$, C��- �� 	Modifiers$
- Ctrlf�. �� Command$
. Copy���� $, V��$
- Ctrlf�$. Paste���� $, X��$
- Ctrlf�$	. Cut��   -0   $
 Copy��  -1   $ Paste��  -2   $	 Cut��  / �� DataType6  0TestCaseManagerCore.BusinessLogic.Entities.Suite0 �� ItemsSource��%W   (��       *, +)�� "/  0 �� *	SubSuites+.R��� $��14(����� $  Title��� $��5��1 �� ResizeDirection$
1 Auto=�$��Stretch=�$��Stretch=�$��1��$��2��$��1��� -3   $��progressBarTestCases��$ 0Q�$ 1Q�$��16�� . $��0,0,0,16q�$��2��$��2��$y�Hidden=�&   System.Windows.Controls.DataGrid  -4   $��dgTestCases��2   AutoGenerateColumns2 .  $��2��$��2��3   
IsReadOnly3 . 4   SelectionMode$4 Extended=�5   SelectionUnit$5 FullRow=�$��5��  
SplitRight#��[ ���� $  ObservableTestCases�6 � ContextMenu6 �� .@�m� -5   $"��dgTestCaseContextItemPreview��#* Z $��Preview��m� -6   $��dgTestCaseContextItemEdit��#* Z $
��Edit��m� -7   $$��dgTestCaseContextItemDuplicate��#* Z $��	Duplicate���� m� -8   $��dgTestCaseContextItemCut��#* Z $	��Cut��m� -9   $��dgTestCaseContextItemCopy��#* Z $
��Copy��m� -:   $ ��dgTestCaseContextItemPaste��#* Z	 $��Paste��m� -;   $!��dgTestCaseContextItemRemove��#* Z $��Remove���� m� -<   $!��dgTestCaseContextItemActive��#* Z $��Reset test to Active��m� -=   $��dgTestCaseContextItemPass��#* Z $��	Pass test��m� ->   $��dgTestCaseContextItemFail��#* Z $��	Fail test��m� -?   $ ��dgTestCaseContextItemBlock��#* Z $��
Block test��+ �� $, C��$
- Ctrlf�$
. Copy���� $, V��$
- Ctrlf�$. Paste���� $, X��$
- Ctrlf�$	. Cut���� $, D1��$	- Altf�#. Z �� $, D2��$	- Altf�#. Z �� $, D3��$	- Altf�#. Z �� $, D4��$	- Altf�#. Z    -@   $
 Copy��  -A   $ Paste��  -B   $	 Cut������ )  #System.Windows.Controls.DataGridRow"&  #' [B .�-C   7   Columns7 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn8   Header$8 ID��9   Width5	  /System.Windows.Controls.DataGridLengthConverter$
9 0.6*	 1
  +System.Windows.Controls.DataGridBoundColumn: 
  Binding: �� *ITestCase.Id+;   HeaderStyle; �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"&  #' [B .��� ( � ��$
) Left=�  $8 Title��$9 6*	 : �� *Title+; �� "&  #' [B .��� ( � ��$
) Left=�<   	CellStyle< �� *  $System.Windows.Controls.DataGridCell"&  .��� ( �@�ToolTip) �� *Title+  $8 Priority��$9 1*	 : �� *
Priority+; �� "&  #' [B .��� ( � ��$
) Left=�  $8 Assigned To��$
9 1.2*	 : �� *(&TeamFoundationIdentityName.DisplayName+; �� "&  #' [B .��� ( � ��$
) Left=�  $8 	Automated��$
9 1.4*	 : �� *ITestCase.IsAutomated+; �� "&  #' [B .��� ( � ��$
) Left=��� $ Vertical=�$��3��$��2��$��3��$��	10,10,0,0q�.+��� $ 
Horizontal=�$
��Left=�.+��� -D   $��
btnPreview��$��_Preview��$��	20,0,10,0q�$��30��$��90��$��Center=��� -E   $��btnEdit��$��_Edit��$��	15,0,10,0q�$��30��$��90��$��Center=��� -F   $��btnDuplicate��$��
_Duplicate��$��	15,0,10,0q�$��30��$��90��$��Center=��� -G   $��btnNew��$
��_New��$��	15,0,10,0q�$��30��$��90��$��Center=��� -H   $��btnRemoveTestCase��$��_Remove��$��	15,0,10,0q�$��30��$��90��$��Center=��� -I   $��
btnExport1��$��_Export��$��	15,0,10,0q�$��30��$��90��$��Center=��� -J   $��btnArrange1��$��Arrange��$��	15,0,10,0q�$��30��$
��Auto��$��Center=��� -K   $��btnChangeTestCases��$��Change Test Cases��$��	30,0,10,0q�$��30��$
��Auto��$��Center=��� -L   $"��btnShowTestCaseWithoutSuite1��$#��Show Test Cases Without Suite��$��	15,0,10,0q�$��30��$
��Auto��$��Center=��� $ 
Horizontal=�$
��Left=�$��4��$��2��$��3��.+��� $��Selected Test Case Count:��$��
30,30,10,0q�$��25��$
��Auto��$
��Bold"��� -M   $��tbSelectedTestCaseCount��$��	0,30,10,0q�$��25��$
��Auto������ *SelectedTestCasesCount+�� $��Test Case Count:��$��	0,30,10,0q�$��25��$
��Auto��$
��Bold"��� -N   $��tbTestCaseCount��$��	0,30,10,0q�$��25��$
��Auto������ *TestCasesCount+