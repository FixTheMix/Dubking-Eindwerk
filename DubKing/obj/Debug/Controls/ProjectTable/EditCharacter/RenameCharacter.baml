   M S B A M L   `   `   `  ����B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:DubKing.Controls.ProjectTable.EditCharacter+DubKing.Controls.ProjectTable.EditCharacter  5      C ?WPFSpark, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:WPFSpark;assembly=WPFSparkWPFSpark 5      A    ;DubKing.Controls.ProjectTable.EditCharacter.RenameCharacter   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Klocal9clr-namespace:DubKing.Controls.ProjectTable.EditCharacter     5      :spark(clr-namespace:WPFSpark;assembly=WPFSpark     5      .��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      �� 5      �� 5      .+�5   
   �� $��	New Name:��6   $��16(�5      $��4q�6   �� 5   
   $��4q�6   $��32��5       �� 
IsEditable . $��1��5      $��Center=�5       �� StaysOpenOnEdit . &   System.Windows.Media.TextOptions   TextHintingMode$ Animated=�5       �� Text 5      �� *CharacterCopy.Name+ �� ValidatesOnDataErrors . ��5      �� *CharacterNames+ =� ErrorTemplate 5!      �� 5"      .R�5#      �� .+�5$      �� .+�5%      �� 
���  ��$��Right=�5'   (   ��5%   (   �� *[0].ErrorContent+5'   (   �� 5(      
���  ��$��2q�5)   %   ._�5*   "   �� 5+      5,      5-      5.      51      �� 52   
   $��Mark As Recorded��6   $��2��53      $��4q�54       d� 	IsChecked 55      �� *MarkAsRecorded+� 56   
   $��3��6     57      �� 58      �� 59       � ColumnDefinitions 5;      �� 5<      �� 5=      �� 5>      .+�5@      �� $��Between Range:��6   �� 5A      $��From:��6   $��1��5B      y�5C      �� 	 �� ElementName$	 BetweenRange��
 �� Path$
 	IsChecked�   
BoolHidden �� 	Converter# [  �� 5D      $	��To:��6   $��2��5E      y�5F      �� $	 BetweenRange��$
 	IsChecked�# [    WPFSpark.ToggleSwitch  5G      -   5          Name$ BetweenRange��5G   9   $��1��6!   $��4q�6.    6O   �� *BetweenRange+6t   �� 5H      $��Center=�6    .  $��4q�5M      $��32��5N      $��1��5O      $��1��5P      y�5I      �� $	 BetweenRange��$
 	IsChecked�# [  ��5J      �� *
Episodes+ �� SelectedItem 5K      �� *From+��5Q      �� 5R      .R�5S      ��   EpisodeSelector#���  6$   6P   5T      5V      �� 5W      $��Center=�6    .  $��4q�5\      $��32��5]      $��2��5^      $��1��5_      y�5X      �� $	 BetweenRange��$
 	IsChecked�# [  ��5Y      �� *
Episodes+ 5Z      �� *To+��5`      �� 5a      .R�5b      �� #���  6$   6P   5c      5e      5g      5i      5j      