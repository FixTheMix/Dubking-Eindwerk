   M S B A M L   `   `   `  ����B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DubKing.ViewDubKing.View  5   	   %    DubKing.View.ErrorMessageDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:DubKing.View     5   	     � Title$  ErrorMessageDialog��5   	    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � SizeToContent$ WidthAndHeight=�5   	    � Topmost .  � WindowStartupLocation$ CenterScreen=�5   	   
���  ��$��2q�6@   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6   �� 5      $��1* �6   .+�5   
   �� -   5         ��Name$ Title��5      $��Center=�6   $��	15 0 15 0q�6C   $��24(�5   0   $
��Bold"�6>   ��6   �� *DialogTitle+6>   �� 5   
   -   5         ��Name$ Message��5      $��1��6%   $
��Wrap=�62   $	��320��6F   	 �� TextAlignment$	 Center=�6R   $��30q�6i   ��5      �� *MessageText+�� 5   
   -   5        
 ��Name$
 MessageButton��5      $��2��5   L   $��32��6Y   $	��120��6e   $��Right=�6q   $��15q�6�    �� 	IsDefault .  �� IsCancel . ��6-   �� *
ButtonText+5      5      5      