   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DubKing.ViewDubKing.View       DubKing.View.ConfirmDelete   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:DubKing.View       � Title$  ConfirmDelete�� � 
ResizeMode$ NoResize=� � SizeToContent$ WidthAndHeight=� � ShowInTaskbar .   � WindowStyle$
 None=�
���  ��$��2q� � WindowStartupLocation$ CenterScreen=�.��� .+��� $��10q�$��Center=�.+��� -    ��Name$ Title��$��Are You Sure?��$��24(�$
��Bold"�$��10q�$��Center=��� -    ��Name$ Message��$>��8You're about to delete the folowing --::PlaceHolder::--:�� �� TextAlignment$ Center=��� -   $ Object��$
��Bold"�$ Center=�$��10q����� *
ObjectName+�� -   $ Message2��$(��"Are you sure you want to continue?��$ Center=��� $%��This operation can't be undone!��$ Center=��� 	 �� Orientation$	 
Horizontal=�$��Center=�.+��� -   $��Cancel��
 �� 	IsDefault
 . $��80��$��24��$��30 20 30 20q� �� IsCancel . �� -   $��Delete��$��80��$��24��$��30 20 30 20q�