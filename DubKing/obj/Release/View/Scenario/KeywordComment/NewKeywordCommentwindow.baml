   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:DubKing.View.Scenario.KeywordComment$DubKing.View.Scenario.KeywordComment  B    <DubKing.View.Scenario.KeywordComment.NewKeywordCommentwindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Dlocal2clr-namespace:DubKing.View.Scenario.KeywordComment       � Title$  NewKeywordCommentwindow�� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � SizeToContent$ Height=� � WindowStartupLocation$ CenterScreen=� � Topmost .  � WindowStyle$
 None=�
����  �$��2q�$	��600��$
��Auto�� � 	Resources  �� 
TargetType	 �� Property
 �� Value �� Triggers P� Property P� Value �� 
TargetType ��Name ��Name��%  '��       '�� �    �� " ��.��� 	 � ��$
 4q��� 	 � ��$
 32�� P�  �@=�HasError . . ��� 	 �@�OverridesDefaultStyle
 . �� 	 � ��

 �  ���� 	 � ��$
 2q��� 	 � ��#
 � ���� 	 � ��
 �� " ��.R��� $ myBorder��#��z��#��z��$��2q�._��� $ PART_ContentHost��#��z��#��z��$��1q�#��z���� 	 �@=�ErrorTemplate
 �� .R��� .+��� 
���  ��$��Right=����� *[0].ErrorContent+�� �� " ��.��� 	 � ��$
 4q��� 	 � ��$

 Left=�.��� .+��� $��X��$��18(�$��24��$��24��$��Right=�$	��Top=�$��Center=�$��Center=� �� IsCancel . 
�������$��0q�� $��24q�$	��Top=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $
��Left=�$��24(�$
��Bold"�  �� Binding �� Value��%�   '��       �� " �� ��  �� *IsNewKeywordBox+ True.a��� 	 � ��$
 Add Keyword Comment:����  �� *IsNewKeywordBox+	 False.a��� 	 � ��$
 Edit Keyword Comment:���� $��1��$��12q�.+��� $��Keyword:���� $��32������ *	Keyword+ �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=��� $��Comment:���� $	��128�� �� TextWrapping$
 Wrap=����� *	Comment+ . $ PropertyChanged=��� -   $��Ok��$��2��$��32��$	��120��$��12q�$��Right=� �� 	IsDefault . 