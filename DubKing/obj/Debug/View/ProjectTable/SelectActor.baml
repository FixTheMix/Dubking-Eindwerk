   M S B A M L   `   `   `  ����B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DubKing.View.ProjectTableDubKing.View.ProjectTable  5   	   E'clr-namespace:DubKing.View.VoiceLibraryDubKing.View.VoiceLibrary  5   	   +    %DubKing.View.ProjectTable.SelectActor   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:DubKing.View.ProjectTable     5   	   @voiceLibrary'clr-namespace:DubKing.View.VoiceLibrary     5   	     � Title$  SelectActor��5	   	   $	��750��6   $
��1000��6*    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   
����  �$��2q�5   	    � DataContext 5      ��  �� ElementName$ VlList�� �� Path$ DataContext�.��5      �  � RowDefinitions 5   
   �� 5      $	��10* �6   �� 5      .+�5   
   0   *DubKing.View.VoiceLibrary.VoiceLibraryList  -   5          Name$ VlList��5   (   6:   � 5   
   $��1��6   	 � ColumnDefinitions	 5      �� 5      $��1* �6#   60   �� 5      $��3* �6#   60   .+�5      �� $��New Voice-Talent��6   $��32��5      $	��120��5      $
��Left=�5      $��24 10 10 10q�5       ��5!      �� *NewVoiceTalentCommand+6B   �� 5"      $��1��6   
 �� Orientation$
 
Horizontal=�5#      $��RightToLeft=�5$      .+�5%      �� $��Select��6   $��32��5&      $	��120��5'      $
��Left=�5)      $��24 10 10 10q�5*      ��5(      �� *SelectActor+5*   1   �� 5+      -   5        $��Cancel��5+      $��32��5,      $	��120��5-      $
��Left=�5.      $��24 10 10 10q�50      61   �� 51      $��No Actor��6   $��32��52      $	��120��53      $
��Left=�54      $��24 10 10 10q�56      ��55      �� *
ClearActor+56   1   57      58      59      5:      