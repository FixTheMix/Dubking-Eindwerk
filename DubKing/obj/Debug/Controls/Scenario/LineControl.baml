   M S B A M L   `   `   `  ����B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DubKing.Controls.ScenarioDubKing.Controls.Scenario  5      +    %DubKing.Controls.Scenario.LineControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:DubKing.Controls.Scenario     5      X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45xceed)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          >�Name$  lineItem��5      $��4q�61    � 	Resources 5
       �� 
TargetType 
  ShowEp �� Property �� Value �� Triggers �� Binding �� RelativeSource �� AncestorType	 �� Path
 �� Value��%  &	        �� 5   
   " ��6   .�5      ��  � y�$ 	Collapsed=�5       5      �� 5       6   ��  �� *FindAncestor+" ��$	 DataContext.SearchProject�
 True5      .a�5      ��  � y�$ Visible=�5      5      5      5      .��5      �� ��6   �� *
IsRecorded+  
IsRecorded �� 	Converter# [  J� InputBindings 5   
   d� 5       d� Gesture$ 	LeftClicka�6    �� Command 5      ��  �� *FindAncestor+" ��$	 DataContext.SelectLine�._�5   
   � $��2q�6    � ColumnDefinitions 5   
   �� 5       $
��Auto �6#   �� 5!      $��1* �6#   $	��120��6.   �� 5"      $��80 �6   �� 5#      $
��Auto �6   .+�5%      �� $	��Top=�6   $
��Bold"�5'      $��0 0 10 0q�5(      #���   5)      ��5&      �� *Episode.CustomCode+5*      �� 5+      $	��Top=�5,      $��32��5-      $��1��5.      $��1q�5/      ��5+      ��  �� ElementName$ lineItem��$	 CharacterName���5/      ��  �� $ BorderL�$	 
Background�54      �� 55      $��2��56      $��32��57      $��1q�58      $	��Top=�59      ��55      �� $ lineItem��$	 TimeCode�5?      #  Xceed.Wpf.Toolkit.RichTextBox  5@   
   -   5          Name$	 rtb��5A      $��3��5@      $	��800��5C      $��32��5D      $��Stretch=�5E      $��1q�5F      $	��Top=�5H       �� 
IsReadOnly .    Text 5B      �� $ lineItem��$	 LineText�5N      5P      5Q      5R      