   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DubKingDubKing  3clr-namespace:DubKing.ControlsDubKing.Controls  +clr-namespace:DubKing.ViewDubKing.View  -clr-namespace:DubKing.UtilsDubKing.Utils      DubKing.View.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:DubKing     2controlclr-namespace:DubKing.Controls     +viewclr-namespace:DubKing.View     -utilsclr-namespace:DubKing.Utils       � Title$  DubKing 1.0��$	��450��$	��800�� � WindowState$ 	Maximized=� � DataContext �� *Main+   Locator �� Source# [   � 	Resources 1  +DubKing.Utils.UserNameToVisibilityConverter  userConverter��%   &	         .��� -    �Name$ MainGrid�� � RowDefinitions �� $��32 ��� $��* ��� $��32 � � ColumnDefinitions �� $��32 ��� $��32 ��� $��32 ��� $��* ��� $��32 ��� $��32 ��� $��32 ��� $��32 �.+�#   DubKing.Controls.LoginControl  $��3��$��8�� K� ZIndex$ 1��y��� *
UserName+	 �� 	Converter#	 [ �� -   
 ��Name$
 btnSettingsBar��$
��Left=�$��0q�  MainNavigationButton#���  ���� *SettingsBtnPosition.Column+���� *SettingsBtnPosition.Row+���� * SettingsBtnPosition.ColumnSpan+���� *SettingsBtnPosition.RowSpan+���� *SettingsMenuBarCommand+.��� .+�(� $��1q�.����   Icons.Settings#��[ �� $��Settings�� � LayoutTransform V� .~��� �� ��  �� Angle �� *SettingsBtnPosition.Rotation+T� �� -   $
 btnScheduleBar��$��Schedule��$
��Left=�$
��20 0q�#���  ���� *ScheduleBtnPosition.Column+���� *ScheduleBtnPosition.Row+���� * ScheduleBtnPosition.ColumnSpan+���� *ScheduleBtnPosition.RowSpan+���� *ScheduleMenuBarCommand+ V� .~��� �� ��  �� *ScheduleBtnPosition.Rotation+T� �� -   $
 btnVoiceLibraryBar��$��Voice Library��$
��Left=�$
��20 0q�#���  ���� * VoiceLibraryBtnPosition.Column+���� *VoiceLibraryBtnPosition.Row+���� *$"VoiceLibraryBtnPosition.ColumnSpan+���� *!VoiceLibraryBtnPosition.RowSpan+���� *VoiceLibraryMenuBarCommand+ V� .~��� �� ��  �� *" VoiceLibraryBtnPosition.Rotation+T� �� -   $
 btnActiveActors��$��Active Actors��$
��Left=�$
��20 0q�#���  ���� *ActiveActorsBtnPosition.Row+���� *!ActiveActorsBtnPosition.RowSpan+���� * ActiveActorsBtnPosition.Column+���� *$"ActiveActorsBtnPosition.ColumnSpan+���� *ActiveActorsMenuBarCommand+ V� .~��� �� ��  �� *" ActiveActorsBtnPosition.Rotation+T� �� -   $
 btnProjectsBar��$��Projects��$
��Left=�$
��20 0q�#���  ���� *ProjectBtnPosition.Row+���� *ProjectBtnPosition.RowSpan+���� *ProjectBtnPosition.Column+���� *ProjectBtnPosition.ColumnSpan+���� *ProjectMenuBarCommand+ V� .~��� �� ��  �� *ProjectBtnPosition.Rotation+T� � $��8��$��Right=�.+�o� $��0��$��Stretch=�$��Stretch=�
���   �$�� 0 2 2 2q�����  �� 
TargetType" m�.���  �� Property � �� �� Value$
 Left=���  � ��$ Center=���  � ��
 ����� ��%    .@�m� -   $��btnUser��
�������$��Stretch=�$��-7 0 0 0q���� $��Stretch=�$��Stretch=� �� $��80 ��� $��40 �.+��� $��0q�$
��Left=����� *
UserName+��   Icons.UserAvatar#��[ $��1��.@�m� $��Logout�� m� Command �� *LogoutCommand+m� -   $��MainMenu��
�������$��Center=�$��Center=�$��Stretch=����� *" BodyContent.DataContext.MainMenu+����   Icons.MenuDots#��[ $��28��$��4q� � -	     �Name$ frame��  � NavigationUIVisibility$ Hidden=����� *BodyContent+���� *BodyPosition.Column+���� *BodyPosition.Row+���� *BodyPosition.ColumnSpan+���� *BodyPosition.RowSpan+