   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DubKing.ViewDubKing.View  %    DubKing.View.ErrorMessageDialog   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:DubKing.View       � Title$  ErrorMessageDialog�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � SizeToContent$ WidthAndHeight=� � Topmost .  � WindowStartupLocation$ CenterScreen=�
���  ��$��2q�.��� $��20q� � RowDefinitions �� $��1* ��� $��2* ��� $��1* �.+��� -    ��Name$ Title��$��Center=�$��	15 0 15 0q�$��24(�$
��Bold"����� *DialogTitle+�� -    ��Name$ Message��$��1��$
��Wrap=�$	��320��	 �� TextAlignment$	 Center=�$��30q����� *MessageText+�� -   
 ��Name$
 MessageButton��$��2��$��32��$	��120��$��Right=�$��15q� �� 	IsDefault .  �� IsCancel . ���� *
ButtonText+