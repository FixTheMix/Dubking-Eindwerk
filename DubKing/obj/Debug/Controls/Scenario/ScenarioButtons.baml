   M S B A M L   `   `   `  ����B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DubKing.Controls.ScenarioDubKing.Controls.Scenario  5      /    )DubKing.Controls.Scenario.ScenarioButtons   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:DubKing.Controls.Scenario     5      .��5	      �   � Orientation$  
Horizontal=�6    � 	Resources 5
   
    �� 
TargetType �� Property �� Value��%?  '��       '�� �     �� 5      " ��6   .�5      ��  � ��$ 48��6+   �� 5       � ��$ 64��6*   �� 5       � ��$ 4q�6+   5      �� 5      " ��6   .�5      ��  � ��$ 2q�5      �� 5       � ��
 �����5      �� 5       � ��$ 1q�5      5      5      .+�5   
   �� 
�������$��1q�6)   ._�5      ��  �� Orientation$ 
Horizontal=�6   $��Bottom=�62   .+�5      �� $
��Prev��6   ��6(   �� *FindPrevious+�� 5      ��6   �� *SelectedLine.IsRecorded+ 
  recBtn �� 	Converter# [  ��5      �� *SetLineStatus+�� 5      $
��Next��6   ��6(   �� *
FindNext+5      5       �� 5#   
   $��1q�6   
�������._�5%      �  � RowDefinitions 5&      �� 5'      �� 5(      .+�5*      �� $��Offset��6   $��Center=�5+      $��Center=�5,      �� 5-      $ 
Horizontal=�6   $��1��66   .+�5.      �� -   5         ��Name$ OffsetTexBox��51   )   $��80��5.      $��32��5/      $��Center=�50      $��4q�51      ��52      �� *OffsetTimeCode+	 �� UpdateSourceTrigger$	 PropertyChanged=�.��6c   
 J� InputBindings
 53      �� 54      
 �� Key$ Subtract��6)    �� Command 55   )   �� *	OffSetMin+ �� CommandParameter 6G   ��  �� ElementName$ OffsetTexBox���� 56      $	 Add��6)    57   )   �� *
OffSetPlus+ 6H   �� $ OffsetTexBox��.��58   1   59      �� 5:      $	��Set��6   ��6+   �� *	SetOffset+�� 5;   
   $��	Calculate��6   ��6%   �� *CalculateOffset+5<      5=      5>      �� 5?   
   ._�5@      �� $ 
Horizontal=�6   $��Bottom=�5A      .+�5B      �� $
��Bold��6   .  (System.Windows.Documents.EditingCommands   
ToggleBold#��Z 5C      ��5D      �� $ SelectedTextField�� �� Path$
 Text��� 5E      $��Italic��6      ToggleItalic#��Z 5F      ��5G      �� $ SelectedTextField��$
 Text��� 5H      $��	Underline��6      ToggleUnderline#��Z 5I      ��5J      �� $ ScenarioListBox��$ SelectedItem.Content�5K      5L      �� 5M   
   ._�5N      �� $ 
Horizontal=�6   $��Bottom=�5O      .+�5P      �� $��Original��6   ��6,   �� *SetOriginal+�� 5Q      $��Add Line��6   ��6,   �� *	AddLine+�� 5R      $��Delete Line��6   ��6/   �� *
DeleteLine+5S      5T      5U      5V      