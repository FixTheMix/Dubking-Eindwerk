   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:DubKing.View.ScenarioDubKing.View.Scenario  c6clr-namespace:DubKing.Controls.Scenario.KeywordComment(DubKing.Controls.Scenario.KeywordComment  E'clr-namespace:DubKing.Controls.ScenarioDubKing.Controls.Scenario  H DDubKing.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:DubKing.Model;assembly=DubKing.ModelDubKing.Model c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity (    "DubKing.View.Scenario.ScenarioView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:DubKing.View.Scenario     Jkeyword6clr-namespace:DubKing.Controls.Scenario.KeywordComment     <controls'clr-namespace:DubKing.Controls.Scenario     Dmodel2clr-namespace:DubKing.Model;assembly=DubKing.Model     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     -     �Name$  ScenarioWindow�� � Title$ ScenarioView��$	��450��$	��800�� � WindowState$ 	Maximized=� � WindowStartupLocation$ CenterScreen=� � DataContext ��    Locator �� Source# [   �� Path$ Scenario� � 	Resources   ItemTempate+   %DubKing.Controls.Scenario.LineControl  Name	   
IsRecorded
   CharacterName   TimeCode   LineText   SelectedText �� ElementName �� Mode  SelectedTemplate3   -DubKing.Controls.Scenario.LineControlSelected   SelectedText �� 
TargetType  ContainerStyle �� Property �� Value �� Triggers P� Property P� Value'  !DubKing.Model.ProjectModuleAccess   ReadOnly �� Value �� Binding+   %DubKing.Controls.Scenario.LineComment��%  &	       &	 �     &	 v    7  7  '  �    '  S    �� .R�  $ theItem��$��1q�	 �� *
IsRecorded+
 �� *Character.Name+ �� *TimecodeWithOffset+ �� *Text+ �� $ ScenarioWindow��$ SelectedText�$ OneWayToSource=��� .R��� 
���   �$��2q�._�  $��1q� �� $ ScenarioListBox��$ DataContext� �� $ ScenarioWindow��$ SelectedText�$ OneWayToSource=��� " ��.���  � ��8    P�  �@��
IsSelected . . ���  � ��8  �� "   P�  � {� .  . ���  � ��
 �   ��� # Z  �� *ActiveUser.ProjectAccess+.a���  � {� .  �� "   P�  � {� .  . ���  � ��
 �   ��� # Z  �� *ActiveUser.ProjectAccess+.a���  � {� .  .���� $��2q�
�������._��  � RowDefinitions �� $
��Auto ��� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� $��3* ��� $��1* �.+�4   .DubKing.Controls.Scenario.ScenarioSearchFields  $��1���� 
���   �$��2q�$��2��._���  �� Orientation$ 
Horizontal=�$��32�� ��%t   '��       �� " ��.���  � ��$ Center=���  � ��$ 16(���  � ��$ 2q���  � ��$
 Bold"�.+��� 
�������$��0q�$��30��$��64��$
��Left=����� *Close+.����   Icons.Back_Arrow#��[ �� ���� *Episode.Project.Customer+�� $	�� - ���� ���� *Episode.Project.Title+�� $	�� - ����  ��%  '��       �� " ��.���  � �� �� *Episode.CustomCode+��  � ��$ Center=���  � ��$ 16(���  � ��$ 2q���  � ��$
 Bold"� ��  �� *Episode.CustomCodeToggle+	 false.a���  � �� �� *Episode.Number+�� $	�� - ���� ���� *Episode.Title+� $��2��$��32��$��2q�$��Right=�.+�o� $��0��$��Stretch=�$��Stretch=�$��1 1 2 0q����� " m�.���  � ��$
 Left=���  � ��$ Center=���  � ��
 �������  � ��$ 0q�.@�m� -   $��btnUser��
�������$��Stretch=�$��-7 0 0 0q�$��0q���� $��Stretch=�$��Stretch=� �� $��80 ��� $��40 �.+��� $��0q�$
��Left=����� *ActiveUser.UserName+��   Icons.UserAvatar#��[ $��1��.@�m� $��Logout�� m� Command �� *LogOut+m� -   $��MainMenu��
�������$��Center=�$��Center=�$��Stretch=����� *
MainMenu+����   Icons.MenuDots#��[ $��28��$��4q��� -    ��Name$ ScenarioListBox��$��2��$��Stretch=�#��[ ���� *FilteredLines+ �� SelectedItem �� *SelectedLine+$ TwoWay=��� $��1��$��1��$��3��
�������$��2q�._��  �� $
��Auto ��� $
��Auto ��� .+�  �� 
�������$��2��$��1��A   ;DubKing.Controls.Scenario.KeywordComment.KeywordCommentList  $��2��    SelectedText  �� $ ScenarioWindow��$ SelectedText�$ OneWay=�/   )DubKing.Controls.Scenario.ScenarioButtons  $��3��