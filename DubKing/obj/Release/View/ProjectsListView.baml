   M S B A M L   `   `   `  ���� B  >DubKing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DubKing.ViewDubKing.View  -clr-namespace:DubKing.UtilsDubKing.Utils  5clr-namespace:DubKing.ViewModelDubKing.ViewModel  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity #    DubKing.View.ProjectsListView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:DubKing.View     -utilsclr-namespace:DubKing.Utils     .vmclr-namespace:DubKing.ViewModel     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity       � DataContext  �� *
Projects+   Locator �� Source# [   � 	Resources ��%    .���  � RowDefinitions �� $��56 ��� .+���  �� Orientation$ 
Horizontal=�.+��� -   $��	FilterBox��$��32��$	��260��$
��Left=�$��	52 0 12 0q�.�� J� InputBindings �� 
 �� Key$ Return�� �� Command �� *StartSearchCommand+ �� CommandParameter �� 	 �� ElementName$	 	FilterBox��
 �� Path$

 Text�.���� $��Search��$��32��$��80��$��12q����� *StartSearchCommand+���� $	 	FilterBox��$

 Text��� -   $��Clear��$��32��$��80��$��12q����� *StartSearchCommand+���� $	 	FilterBox��$

 Text��� -   $��ProjectList��$��Stretch=�$��1��$��Disabled=����� *
Projects+ �� SelectedItem �� *SelectedProject+ �� Mode$ TwoWay=����� .R�   DubKing.View.ProjectBar  