   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DubKing.View.VoiceLibraryDubKing.View.VoiceLibrary  5clr-namespace:DubKing.ViewModelDubKing.ViewModel  -clr-namespace:DubKing.ModelDubKing.Model  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem -clr-namespace:DubKing.UtilsDubKing.Utils  3clr-namespace:DubKing.ControlsDubKing.Controls  C ?WPFSpark, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:WPFSpark;assembly=WPFSparkWPFSpark ?$clr-namespace:DubKing.CustomControlsDubKing.CustomControls  0    *DubKing.View.VoiceLibrary.VoiceLibraryList   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:DubKing.View.VoiceLibrary     .vmclr-namespace:DubKing.ViewModel     -modelclr-namespace:DubKing.Model     6sys&clr-namespace:System;assembly=mscorlib     -utilsclr-namespace:DubKing.Utils     2controlclr-namespace:DubKing.Controls     :spark(clr-namespace:WPFSpark;assembly=WPFSpark     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  9customcontrols$clr-namespace:DubKing.CustomControls  -     >�Name$  VoiceTalentsListControl��&   System.Windows.Media.TextOptions   TextFormattingMode$ Display=� � DataContext �� *VoiceTalents+   Locator �� Source# [   � 	Resources ��%    .��� $��Stretch=�$��Stretch=� � RowDefinitions �� $
��Auto ��� .+��  � ColumnDefinitions �� $
��Auto ��� $	��200���� $
��Auto �.+��� $��24��$��24��$��>��$
��Left=�$	��Top=����� *ShowDetailsCommand+� $��Stretch=�$��Stretch=�$��1������ *ShowDetails+  BoolToColumnSpan �� 	Converter# [ .+���  �� 
IsEditable . $	��152��$��32��$��10q����� *Suggestions+	 �� Text	 �� *
FullName+
 �� UpdateSourceTrigger$
 PropertyChanged=�.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ 	LostFocus��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *StartSearch+ J� InputBindings �� 
 �� Key$ Return�� �� Command �� *StartSearch+�� $��Search��$��32��$��80��$��10q����� *StartSearch+�� $��Clear��$��32��$��80��$��10q����� *
ClearSeach+� $��2��$��Stretch=�y��� *ShowDetails+  showdetails# [  �� $
��Auto ��� $
��Auto ��� $
��Auto � �� �� .+��� $��10q�$��1q�
�������
�������._�� $
��Auto�� �� $��24 ��� .+��� 
�������._�� .+��� $��Gender:��$
��Left=�$��24��(� $��Right=�.��  WPFSpark.ToggleSwitch  $��4q� d� 	IsChecked �� *GenderSelected+�� $��1��$��0q� %� 
ScrollUnit$ Pixel=� %� VirtualizationMode$ 	Recycling=� %� CacheLengthUnit$
 Item=� %� CacheLength@  :System.Windows.Controls.VirtualizationCacheLengthConverter$ 20  %� IsVirtualizing . ���� *Gender+���� .R�� ���� .R��� $��LeftToRight=� �� *
IsSelected+ �� Mode$ TwoWay=�.���� ���� *Gender+  GenderConverter# [ �� $��10q�$��1q�
�������$��1��
�������._�� $
��Auto�� �� $��24 ��� .+��� 
�������._�� .+��� $
��Age:��$
��Left=�$��24��(� $��Right=�.��  $��4q� �� *AgeSelected+�� $��1�� �� Orientation$ 
Horizontal=�.+���  . $��52��$��28��$��4q�	 �� *MinAge+���� *
PosibleAge+���� .R��� ���� ��  . $��52��$��28��$��4q�	 �� *MaxAge+���� *
PosibleAge+���� .R��� ���� �� $	��240��$��Stretch=�$��Stretch=�$��10q�$��1q�
�������$��0��$��1��$��2��._�� $
��Auto��$��Stretch=� �� $��24 ��� .+��� 
�������._�� .+��� $��Select Keyword:��$
��Left=�$��24��(� $��Right=�.��  $��4q� �� *KeywordSelected+)   #DubKing.CustomControls.CheckListBox  -     Name$ KeywordList��$��1��$	��360��$��Stretch=� �� SelectionMode$ Multiple=����� *
VLKeywords+   BindableSelectedItems �� *SelectedKeywords+$
 PropertyChanged=�  �� 
TargetType �� Property �� Value  �� 
TargetType! �� RelativeSource" �� Mode# �� AncestorType$ �� AncestorLevel% �� Path��%  '��       �� " ��.���  �@�OverridesDefaultStyle . ��  �@J�SnapsToDevicePixels . ��  � �� �� "  ��.R��� $	��5,2q� �� ! �� $" FindAncestor=�"# ��$$ 1��$% 
IsSelected�$ TwoWay=�$
 PropertyChanged=�.���� ���� *	KeyWord+���� .R�� & � Orientation$& Vertical=�$��80������ ' �� ElementName$' KeywordList��$% ActualWidth��� $��10q�$��1q�
�������$��0��$��2��
�������._�� $
��Auto�� �� $��24 ��� .+��� 
�������._�� .+��� $��Rating:��$
��Left=�$��24��(� $��Right=�.��  $��4q� �� *RatingSelected+� $��1��.+�	   DubKing.Controls.Rating	  $��24��$��80��$��4q�( 	  Value( �� *Rating+�� 
�������$��10q�$��1q�
�������$��1��$��2��._�� $
��Auto�� �� $��24 ��� .+��� 
�������._�� .+��� $��	Language:��$
��Left=�$��24��(� $��Right=�.��  $��4q� �� *LanguageSelected+�� $��Center=�$��Center=�$��1��$ 
Horizontal=�.+��� $��24��$	��120��$��4q����� *PosibleLanguages+	 �� *
Language+$ TwoWay=��� -   ) ��Name$) VoiceTalentsListBox��$��1��$��Stretch=�$��Stretch=����� *FilteredVoiceTalents+* �� SelectedItem* �� *SelectedVoice+$ TwoWay=����� .R�.
   (DubKing.View.VoiceLibrary.VoiceTalentBar
  $��Stretch=����� $' VoiceTalentsListBox��$% ActualWidth�+ 
  NewKeywordCommand+ �� $' VoiceTalentsListControl��$% DataContext.NewKeyword�, 
  VLKeywordsList, �� $' VoiceTalentsListControl��$% DataContext.VLKeywords�$ TwoWay=�- 
  DeleteKeywordCommand- �� $' VoiceTalentsListControl��$% DataContext.DeleteKeyword�