   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DubKing.View.VoiceLibraryDubKing.View.VoiceLibrary  3clr-namespace:DubKing.ControlsDubKing.Controls  .    (DubKing.View.VoiceLibrary.NewVoiceTalent   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:DubKing.View.VoiceLibrary     3controlsclr-namespace:DubKing.Controls       � Title$  NewVoiceTalent��$
��1000�� � WindowStartupLocation$ CenterScreen=� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�$��2q�
����  � � WindowStyle$
 None=� � DataContext ��    Locator �� Source# [   �� Path$ NewVoiceTalent�.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� .+��� $��New Voice-Talent:��$��24(�(   "DubKing.Controls.VoiceTalentFields  $��1��$��10q�	   PosibleLanguages	 �� *PosibleLanguages+
   Command
 �� *	PhotoDrop+   DragEnterCommand �� *	DragEnter+   NewKeywordCommand �� $ 
NewKeyword�   DeleteKeywordCommand �� $ DeleteKeyword�   VLKeywordsList �� $ 
VlKeywords� �� Mode$ TwoWay=��� $��	4 20 4 20q�$��Right=�$��2�� �� Orientation$ 
Horizontal=�.+��� $��Cancel�� �� IsCancel . $��96��$��32��$��0 0 8 0q����� *CancelCommand+�� $
��Save�� �� 	IsDefault . $��96��$��32��$��8 0 0 0q����� *SaveCommand+