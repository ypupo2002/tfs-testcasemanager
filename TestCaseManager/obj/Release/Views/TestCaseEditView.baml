   M S B A M L   `   `   `  ���� N  JTestCaseManagerCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullfFclr-namespace:TestCaseManagerCore.Helpers;assembly=TestCaseManagerCoreTestCaseManagerCore.Helpers  J FTestCaseManager, Version=1.0.9.1, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TestCaseManagerApp.ViewsTestCaseManagerApp.Views �Wclr-namespace:TestCaseManagerCore.BusinessLogic.Converters;assembly=TestCaseManagerCore,TestCaseManagerCore.BusinessLogic.Converters  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ~Rclr-namespace:TestCaseManagerCore.BusinessLogic.Enums;assembly=TestCaseManagerCore'TestCaseManagerCore.BusinessLogic.Enums  /   )TestCaseManagerApp.Views.TestCaseEditView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  ThelpersFclr-namespace:TestCaseManagerCore.Helpers;assembly=TestCaseManagerCore  2local&clr-namespace:TestCaseManagerApp.Views  econvertWclr-namespace:TestCaseManagerCore.BusinessLogic.Converters;assembly=TestCaseManagerCore  3System&clr-namespace:System;assembly=mscorlib  ^enumsRclr-namespace:TestCaseManagerCore.BusinessLogic.Enums;assembly=TestCaseManagerCore  -   .���   � 	Resources  N   HTestCaseManagerCore.BusinessLogic.Converters.TestStepPropertiesConverter   TestStepPropertiesConverter��%   &	           J� CommandBindings )  #System.Windows.Input.CommandBinding  -   /  )TestCaseManagerApp.Views.TestCaseEditView   SaveCommand   Command# Z   -      SaveAndCloseCommand# Z   -      EditStepCommand# Z   -      ChangeStepCommand# Z   -      InsertStepCommand# Z   -      AssociateTestCommand# Z   -   	   DeleteTestStepCommand# Z	   -	   
   MoveUpTestStepsCommand# Z
   -
      MoveDownTestStepsCommand# Z   -      AddSharedStepCommand# Z   -      ShareStepCommand# Z   -      CopyTestStepsCommand# Z   -      CutTestStepsCommand# Z   -      PasteTestStepsCommand# Z   -      UndoCommand# Z   -      RedoCommand# Z   -      EditSharedStepCommand# Z   -   !   DuplicateSharedStepCommand# Z   -      EditTestStepCommand# Z   -   3  -TestCaseManagerApp.Views.TestCasesInitialView   PreviewCommand# Z .+�� -   $��progressBar�� � Minimum$ 0Q� � Maximum$ 1Q�$��16�� � IsIndeterminate . $��0,0,0,16q�� -   $��mainGrid��$y�Hidden=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+�� $��0�� � ColumnDefinitions �� $��* ��� $
��Auto ��� $
��Auto �.+��� $��
10,0,10,20q�$
��Auto��$
��Auto��$��26(�$
��Bold"�$
��Left=�$��0������ *	PageTitle+�� -   $��btnUndo��{�.  $
��Undo��$��0,0,0,20q�$	��100��$��25��$��Center=� �� 	IsDefault . $��1���� -   $��btnRedo��{�.  $
��Redo��$��
20,0,10,20q�$	��100��$��25��$��Center=� . $��2��� $��1��    dataFromEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters  M� TypeName��%B   &	       S� $ 	GetValues��"   M� $  enums:Priority�� �� $
��Auto ��� $
��Auto � �� $
��Auto ��� $	��10* ��� $��5* ��� $��5* ��� $
��1.8* ��� $
��2.2* �.+��� $��ID��$��	10,0,10,0q�$��25��$
��Bold"�$��Stretch=�$��0��$��0���� $��	10,0,10,0q�$��25��$
��Left=�$��1��$��0��! �� 
IsReadOnly! . ���� *TestCaseIdLabel+�� $��Title��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��1���� -   $��tbTitle��$��Stretch=�$��	10,0,10,0q�$��25��$��1��$��1��" �� 	MaxLength$	" 255������ *TestBase.Title+# � ToolTip# �� *TestBase.Title+�� $��Suite��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��2��{��� *ShowTestCaseSpecificFields+�� -   $��tbSuite��$��	10,0,10,0q�$��25��$��Stretch=�$��1��$��2��! . {��� *ShowTestCaseSpecificFields+���� *TestCase.ITestSuiteBase.Title+$ �� Mode$$ OneWay=��� $
��Area��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��3���� -   $��cbArea��$��25��$��	10,0,10,0q�% �� IsTextSearchEnabled% . & �� 
IsEditable& . <   6TestCaseManagerCore.Helpers.ComboBoxDropdownExtensions '   OpenDropDownAutomatically' . $��1��$��3������ *Areas+( �� SelectedValue( �� ) �� Path$) TestBase.Area�$$ TwoWay=��� $��Priority��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��4���� -   $��
cbPriority��$��25��$��	10,0,10,0q�$��1��$��4��' . ���� * �� Source#* [ ( �� $) TestBase.Priority�$$ TwoWay=��� $��	Automated��$��	10,0,10,0q�$��25��$
��Bold"�$
��Left=�$��0��$��5��{��� *ShowTestCaseSpecificFields+�� $��1��$��5��! . {��� *ShowTestCaseSpecificFields+���� * TestCase.ITestCase.IsAutomated+$$ OneWay=�� $��2�� �� $
��0.7* ��� $
��Auto ��� $��* � �� $
��1.2* ��� $
��1.2* ��� $��* �.+�� $��0��$��3�� �� $
��Auto ��� $��* ��� $
��Auto � �� $
��1.2* ��� $
��1.2* ��� -   + ��Name$+ Column3��$��* �.+��� $��Work on Step��$��
10,20,10,0q�$��25��$
��Bold"�$
��Left=�$��0���� -   $��	rtbAction��$��Stretch=�$��Stretch=�$��	10,0,10,0q�$��1��$��1��$��0���� -    $��rtbExpectedResult��$��Stretch=�$��Stretch=�$��	10,0,10,0q�$��2��$��1��$��1���� $��Disabled=�$
��Auto=�$��2��$��1��{��� *ShowTestCaseSpecificFields+.���� , �� Orientation$, Vertical=�{��� *ShowTestCaseSpecificFields+.+��� -!   $��btnAssociateToAutomation��$��
Ass_ociate��$
��Auto��$��30��$��
10,0,10,10q�$
��Left=�$��Center=� . {��� *IsAutomated+�� $��Automated Test Type��$��
10,0,10,10q�$��25��$
��Bold"�$
��Left=��� $��
10,0,10,10q�$��25��$��Stretch=�$��1��! . ���� *AssociatedAutomation.Type+$$ OneWay=��� $��Automated test storage��$��
10,0,10,10q�$��25��$
��Bold"�$
��Left=��� $��
10,0,10,10q�$��25��$��Stretch=�$��1��$��1��! . ���� *AssociatedAutomation.Assembly+$$ OneWay=��� $��Automated test name��$��
10,0,10,10q�$��25��$
��Bold"�$
��Left=��� $��
10,0,10,10q�$��25��$��Stretch=�! . ���� *AssociatedAutomation.TestName+$$ OneWay=��� $, 
Horizontal=�$��2��$��0��$��2��$��	0,10,0,10q�.+��� -"   $��btnInsertStep��$��_Insert��$	��100��$��30��$��	10,0,10,0q�$��Center=� . $��3���� -#   $��btnEdit��$��_Edit��$	��100��$��30��$��	10,0,10,0q�$��Center=�$��4���� -$   $��btnSaveTestStep��$
��Save��$	��100��$��30��$��	10,0,10,0q�$��Center=�{�.  $��5���� -%   $��btnCancelEdit��$��Cancel��$	��100��$��30��$��	10,0,10,0q�$��Center=�$
��Left=�{�.  $��6���� $��5��- �� ResizeDirection$
- Auto=�$��Stretch=�$��Stretch=�$��1��$��0��$��3��$��0,5,0,5q�� $��2��$��2�� �� $��* ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  -&   $��dgTestSteps��.   AutoGenerateColumns. .  /   
IsReadOnly/ . $0 �� IsSynchronizedWithCurrentItem$
0 TrueY�1   SelectionMode$1 Extended=�2   SelectionUnit$2 FullRow=�$
��Left=�$��0��3 �� SelectedIndex$3 0��4 %� 
ScrollUnit$4 Pixel=����� *ObservableTestSteps+5 J� InputBindings5 �� 
6 �� Key$6 C��7 �� 	Modifiers$
7 Ctrlf�8 �� Command$
8 Copy���� $6 V��$
7 Ctrlf�$8 Paste���� $6 X��$
7 Ctrlf�$	8 Cut��   -'   $
 Copy��  -(   $ Paste��  -)   $	 Cut������ 9 �� 
TargetType)  #System.Windows.Controls.DataGridRow"9  : �� BasedOn#: [B .�-*   ; � ContextMenu; �� .@�m� -+   $��dgTestStepsCutMenuItem��< m� Command#< Z $	��Cut��m� -,   $��dgTestStepsCopyMenuItem��#< Z $
��Copy��m� --   $��dgTestStepsPasteMenuItem��#< Z $��Paste���� m� -.   $��dgTestStepsShareMenuItem��#< Z $��Share��{��� *ShowTestCaseSpecificFields+m� -/   $��dgTestStepsEditMenuItem��#< Z $
��Edit��m� -0   $��dgTestStepsDeleteMenuItem��#< Z	 $��Delete��=   Columns= 4	  .System.Windows.Controls.DataGridTemplateColumn	  ,
  &System.Windows.Controls.DataGridColumn> 
  Header$> Shared Step��? 
  Width5  /System.Windows.Controls.DataGridLengthConverter$	? 150 @ 
  MaxWidth$	@ 200Q�A 
  HeaderStyleA �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"9  #: [B .��� B �� PropertyB � ��C �� Value$
C Left=�D 	  CellTemplateD �� .R��� $
��Wrap=����� *Title+	  $> Steps��$? 5* A �� "9  #: [B .��� B � ��$
C Left=�D �� .R��� $
��Wrap=����� *ActionTitle+	  $> Expected Results��$? 5* A �� "9  #: [B .��� B � ��$
C Left=�D �� .R��� $
��Wrap=����� *ActionExpectedResult+�� $, 
Horizontal=�$��1��$��3��.+��� $��Disabled=�$
��Auto=�.���� $, Vertical=�$��Center=�.+��� -1   $��	btnMoveUp��$	��_Up��$��80��$��30��$��5q�$��Center=��� -2   $��btnMoveDown��$��_Down��$��80��$��30��$��5q�$��Center=��� -3   $��btnAdd��$
��_Add��$��80��$��30��$��5q�$��Center=�{��� *ShowTestCaseSpecificFields+�� -4   $��btnShare��$��_Share��$��80��$��30��$��5q�$��Center=�$��6��{��� *ShowTestCaseSpecificFields+�� -5   $��btnDeleteStep��$��_Delete��$��80��$��30��$��5q�$��Center=�$��7���� -6   $��	btnChange��$��_Change��$��80��$��30��$��5q�$��Center=�$��8��� $��2��$��3�� �� $
��Auto ��� $��* �.+��� -7   $��tbSharedStepFilter��$��25��$��10,30,10,10q�$��Stretch=�$��0������ $) SharedStepsFilter�$$ TwoWay=�{��� *ShowTestCaseSpecificFields+  -8   $��dgSharedSteps��. .  $E   HorizontalScrollBarVisibility$E Disabled=�/ . $��1������ *ObservableSharedSteps+{��� *ShowTestCaseSpecificFields+���� "9  #: [B .�-9   �� B �@�ContextMenuC �� .@�m� #< Z $
��Edit��{��� $) IsAddEnabled�m� #< Z $��	Duplicate��{��� $) IsAddEnabled�= 0  *System.Windows.Controls.DataGridTextColumn  $> Title��$	? 400 1  +System.Windows.Controls.DataGridBoundColumnF   BindingF �� *ISharedStep.Title+A �� "9  #: [B .��� B � ��$
C Left=�G 
  	CellStyleG �� *  $System.Windows.Controls.DataGridCell"9  .��� B �@�ToolTipC �� *StepsToolTip+�� $, 
Horizontal=�$��3��.+��� -:   $��btnSaveAndCloseTestCase��$��SaveAndClose��$��30��$	��150��$��	0,20,0,20q�$��Center=� . �� -;   $��btnSaveTestCase��$
��Save��$��30��$��90��$��
20,20,0,20q�$��Center=� . �� -<   $��	btnCancel��$��Cancel��$��30��$��90��$��
20,20,0,20q�$��Center=� . H �� IsCancelH . �� -=   $��btnDuplicate��$��
_Duplicate��$��30��$��90��$��
20,20,0,20q�$��Center=� . {��� *ShowTestCaseSpecificFields+�� ->   $��
btnPreview��$��_Preview��$��	20,0,10,0q�$��30��$��90��$��Center=�{��� *ShowTestCaseSpecificFields+6  0FirstFloor.ModernUI.Windows.Controls.BBCodeBlock  -?   $��lbSaved��I   BBCode$"I [b][size=20]SAVED[/size][/b]��$��
20,20,0,20q�$��Right=�$y�Hidden=�