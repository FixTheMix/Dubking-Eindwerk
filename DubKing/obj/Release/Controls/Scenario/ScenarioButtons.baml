   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DubKing.Controls.ScenarioDubKing.Controls.Scenario  /    )DubKing.Controls.Scenario.ScenarioButtons   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:DubKing.Controls.Scenario     .���   � Orientation$  
Horizontal=� � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       '�� N     �� " ��.���  � ��$ 48����  � ��$ 64����  � ��$ 4q��� " ��.���  � ��$ 2q���  � ��
 �������  � ��$ 1q�.+��� 
�������$��1q�._���  �� Orientation$ 
Horizontal=�$��Bottom=�.+��� $
��Prev������ *FindPrevious+�� ���� *SelectedLine.IsRecorded+ 
  recBtn �� 	Converter# [  ���� *SetLineStatus+�� $
��Next������ *
FindNext+�� $��1q�
�������._��  � RowDefinitions �� �� .+��� $��Offset��$��Center=�$��Center=��� $ 
Horizontal=�$��1��.+��� -    ��Name$ OffsetTexBox��$��80��$��32��$��Center=�$��4q����� *OffsetTimeCode+	 �� UpdateSourceTrigger$	 PropertyChanged=�.��
 J� InputBindings
 �� 
 �� Key$ Subtract�� �� Command �� *	OffSetMin+ �� CommandParameter ��  �� ElementName$ OffsetTexBox���� $	 Add�� �� *
OffSetPlus+ �� $ OffsetTexBox��.���� $	��Set������ *	SetOffset+�� $��	Calculate������ *CalculateOffset+�� ._��� $ 
Horizontal=�$��Bottom=�.+��� $
��Bold��.  (System.Windows.Documents.EditingCommands   
ToggleBold#��Z ���� $ SelectedTextField�� �� Path$
 Text��� $��Italic��   ToggleItalic#��Z ���� $ SelectedTextField��$
 Text��� $��	Underline��   ToggleUnderline#��Z ���� $ ScenarioListBox��$ SelectedItem.Content��� ._��� $ 
Horizontal=�$��Bottom=�.+��� $��Original������ *SetOriginal+�� $��Add Line������ *	AddLine+�� $��Delete Line������ *
DeleteLine+