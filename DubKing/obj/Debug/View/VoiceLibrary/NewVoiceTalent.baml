   M S B A M L   `   `   `  ����B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DubKing.View.VoiceLibraryDubKing.View.VoiceLibrary  5   	   3clr-namespace:DubKing.ControlsDubKing.Controls  5   	   .    (DubKing.View.VoiceLibrary.NewVoiceTalent   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:DubKing.View.VoiceLibrary     5   	   3controlsclr-namespace:DubKing.Controls     5   	     � Title$  NewVoiceTalent��5	   	   $
��1000��6     � WindowStartupLocation$ CenterScreen=�5
   	    � SizeToContent$ WidthAndHeight=�5   	    � 
ResizeMode$ NoResize=�5   	   $��2q�5   	   
����  � � WindowStyle$
 None=�5   	    � DataContext 5   	   ��    Locator �� Source# [   �� Path$ NewVoiceTalent�.��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��New Voice-Talent:��6   $��24(�5      (   "DubKing.Controls.VoiceTalentFields  5   
   $��1��6%   $��10q�5   %   	   PosibleLanguages	 5   %   �� *PosibleLanguages+
   Command
 6S   �� *	PhotoDrop+   DragEnterCommand 6q   �� *	DragEnter+   NewKeywordCommand 5   %   �� $ 
NewKeyword�   DeleteKeywordCommand 5   %   �� $ DeleteKeyword�   VLKeywordsList 5   %   �� $ 
VlKeywords� �� Mode$ TwoWay=��� 5   
   $��	4 20 4 20q�6   $��Right=�5       $��2��5!       �� Orientation$ 
Horizontal=�5"      .+�5#      �� $��Cancel��6    �� IsCancel . $��96��5&      $��32��5'      $��0 0 8 0q�5(      ��5$      �� *CancelCommand+5(      �� 5)      $
��Save��6    �� 	IsDefault . $��96��5,      $��32��5-      $��8 0 0 0q�5.      ��5*      �� *SaveCommand+5.      5/      50      51      