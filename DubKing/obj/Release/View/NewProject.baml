   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DubKing.ViewDubKing.View  5clr-namespace:DubKing.ViewModelDubKing.ViewModel  -clr-namespace:DubKing.ModelDubKing.Model  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 3clr-namespace:DubKing.ControlsDubKing.Controls      DubKing.View.NewProject   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:DubKing.View     .vmclr-namespace:DubKing.ViewModel     -modelclr-namespace:DubKing.Model     6sys&clr-namespace:System;assembly=mscorlib     2controlclr-namespace:DubKing.Controls       � Title$  
NewProject��$
��Auto��$	��800�� � WindowStyle$
 None=�$��2q�
����  � � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � SizeToContent$ Height=� � DataContext �� *
NewProject+   Locator �� Source# [   � 	Resources ��%    .��� $��20q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��New Project��$��24(�$   DubKing.Controls.ProjectFields  $��1���� $��0 20 0 0q�	 �� Orientation$	 
Horizontal=�$��Right=�$��Center=�$��2��$��2��$��32��.+��� $��Cancel��$	��128��$
��32 0q�
 �� IsCancel
 . ���� *CancelCommand+�� $
��Save��$	��128��$��32 0 8 0q� �� 	IsDefault . ���� *SaveProjectCommand+