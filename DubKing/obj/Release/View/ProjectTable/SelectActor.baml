   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DubKing.View.ProjectTableDubKing.View.ProjectTable  E'clr-namespace:DubKing.View.VoiceLibraryDubKing.View.VoiceLibrary  +    %DubKing.View.ProjectTable.SelectActor   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:DubKing.View.ProjectTable     @voiceLibrary'clr-namespace:DubKing.View.VoiceLibrary       � Title$  SelectActor��$	��750��$
��1000�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�
����  �$��2q� � DataContext ��  �� ElementName$ VlList�� �� Path$ DataContext�.���  � RowDefinitions �� $	��10* ��� .+�0   *DubKing.View.VoiceLibrary.VoiceLibraryList  -     Name$ VlList��� $��1��	 � ColumnDefinitions	 �� $��1* ��� $��3* �.+��� $��New Voice-Talent��$��32��$	��120��$
��Left=�$��24 10 10 10q����� *NewVoiceTalentCommand+�� $��1��
 �� Orientation$
 
Horizontal=�$��RightToLeft=�.+��� $��Select��$��32��$	��120��$
��Left=�$��24 10 10 10q����� *SelectActor+�� -   $��Cancel��$��32��$	��120��$
��Left=�$��24 10 10 10q��� $��No Actor��$��32��$	��120��$
��Left=�$��24 10 10 10q����� *
ClearActor+