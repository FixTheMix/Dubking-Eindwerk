   M S B A M L   `   `   `  ����B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DubKing.ViewDubKing.View  5   	   5clr-namespace:DubKing.ViewModelDubKing.ViewModel  5   	   -clr-namespace:DubKing.ModelDubKing.Model  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	   	   3clr-namespace:DubKing.ControlsDubKing.Controls  5
   	       DubKing.View.NewProject   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:DubKing.View     5   	   .vmclr-namespace:DubKing.ViewModel     5   	   -modelclr-namespace:DubKing.Model     5   	   6sys&clr-namespace:System;assembly=mscorlib     5	   	   2controlclr-namespace:DubKing.Controls     5
   	     � Title$  
NewProject��5   	   $
��Auto��6   $	��800��6*    � WindowStyle$
 None=�66   $��2q�6I   
����  � � 
ResizeMode$ NoResize=�6p    � WindowStartupLocation$ CenterScreen=�6�    � SizeToContent$ Height=�6�    � DataContext 5   	   �� *
NewProject+   Locator �� Source# [   � 	Resources 5      ��%    5      .��5      � $��20q�5   	    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��New Project��6   $��24(�5      $   DubKing.Controls.ProjectFields  5   
   $��1��6    �� 5   
   $��0 20 0 0q�5      	 �� Orientation$	 
Horizontal=�5      $��Right=�5      $��Center=�5       $��2��5!      $��2��5"      $��32��5#      .+�5$      �� $��Cancel��5%      $	��128��5&      $
��32 0q�5'      
 �� IsCancel
 . ��5)      �� *CancelCommand+�� 5*      $
��Save��5+      $	��128��5,      $��32 0 8 0q�5-       �� 	IsDefault . ��5/      �� *SaveProjectCommand+50      51      52      