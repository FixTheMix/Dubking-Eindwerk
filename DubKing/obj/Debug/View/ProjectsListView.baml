   M S B A M L   `   `   `  ����B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DubKing.ViewDubKing.View  5      -clr-namespace:DubKing.UtilsDubKing.Utils  5      5clr-namespace:DubKing.ViewModelDubKing.ViewModel  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5	      #    DubKing.View.ProjectsListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:DubKing.View     5      -utilsclr-namespace:DubKing.Utils     5      .vmclr-namespace:DubKing.ViewModel     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5	        � DataContext  5      �� *
Projects+   Locator �� Source# [   � 	Resources 5      ��%    5      .��5      �  � RowDefinitions 5   
   �� 5      $��56 �6   �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��	FilterBox��5      $��32��5      $	��260��5      $
��Left=�5      $��	52 0 12 0q�5      .��6)    J� InputBindings 5      �� 5      
 �� Key$ Return��5   !    �� Command 5   !   �� *StartSearchCommand+ �� CommandParameter 5   !   �� 	 �� ElementName$	 	FilterBox��
 �� Path$

 Text�.��5    )   5!      �� 5"      $��Search��6   $��32��5#      $��80��5$      $��12q�5&   S   ��5%      �� *StartSearchCommand+��5&      �� $	 	FilterBox��$

 Text�6S   �� 5'      -   5        $��Clear��5'      $��32��5(      $��80��5)      $��12q�5,   @   ��5*      �� *StartSearchCommand+��5+      �� $	 	FilterBox��$

 Text�5,   @   5.      �� 50   
   -   5        $��ProjectList��50      $��Stretch=�51      $��1��52      $��Disabled=�53      ��54      �� *
Projects+ �� SelectedItem 55      �� *SelectedProject+ �� Mode$ TwoWay=���57      �� 58      .R�59         DubKing.View.ProjectBar  5;      5<      5>      5@      5C      