   M S B A M L   `   `   `  ����C  ?FlowShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:FlowShop.ViewsFlowShop.Views  5      "    FlowShop.Views.AlgoritmaView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:FlowShop.Views     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      -   5        $��	Algoritma��5      
�������$��Stretch=�5	      $	��Top=�5
      $��5q�5        � 	Resources  5       �� 
TargetType �� Property �� Value��%�   '��       ']� p     �� 5   
   " ��6   .�5      ��  � ��$ 60��6&   �� 5       � ��$ Center=�69   5      �� 5   
   " ]�6   .�5      ��  � ��$	 1.5q�6'   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5   
   �� $��1q�6   
���   �$	��177��5      ._�5      �  � ColumnDefinitions 5      �� 5      $
��auto �6'   �� 5       �� 5!      $
��auto �6'   .+�5#      �  5$      �� 5%      $
��auto �6(   �� 5&      .+�5(      �� $��Data Pekerjaan��6   $��SemiBold"�5)      $	��Top=�5*      $��Center=�5+      �� 5,      $��1��5-      $��Center=�5.      $
��Auto=�5/      ��5,      �� *
DataView+��50      �� 51      " ��6$   .�52   "   ��  � ��$ 0 0 5 0q�6;   53      55      56      �� 57      $��1��6   $��Proses Penjadwalan��58      $��25(�59      $��Center=�5:      $��Center=�5;      �� 5<      $��2��6   $��Right=�5=      .+�5>      ]� .+�5?      �� $��Jumlah Generasi: ��6    $
��Left=�5@       �� 5A      $��Right=�6"   ��6>   �� *JumlahGenerasi+ �� Mode$ TwoWay=�5B      ]� 5C      .+�5D      �� $��Jumlah Chromosome: ��6    $
��Left=�5E       �� 5F      $��Right=�6"   ��6>   �� *JumlahChromosome+5G      ]� 5H      .+�5I      �� $��Mutation Rate (%): ��6    $
��Left=�5J       �� 5K      $��Right=�6"   ��6>   �� *MutationRate+5L      ]� 5M      .+�5N      �� $��Crossover Rate (%): ��6    �� 5O      $��Right=�6"   ��6>   �� *CrossoverRate+5P      �� 5Q       �� Orientation$ 
Horizontal=�6!   $��Right=�5R   !   .+�5S      �� $��60��6!   $��27��5T      $	��1.5q�5U      $��Reset!��5V      $��Right=�5W      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5X      +  %Microsoft.Xaml.Behaviors.EventTrigger  5Y   "   	   	EventName$	 Click��61   *  $Microsoft.Xaml.Behaviors.TriggerBase
   Actions.
 5Z   &   4  .Microsoft.Xaml.Behaviors.Core.CallMethodAction     
MethodName$ resetBtnClicked��5[   5      TargetObject 5Z   9   �� 5[   5   5\   #   5^      �� 5_      $��60��6!   $��27��5`      $	��1.5q�5a      $��Mulai!��5b      $��Right=�5c       5d        5e   "   $	 Click��61   .
 5f   &     $ doneBtnClicked��5g   5    5f   9   �� 5g   5   5h   #   5j      5k      5l      5m      5n      �� 5o   
   $��1��6   .+�5p      �� $��Proses Genetic Algorithm��6   $��Center=�67   �� 5q      $	��300��5r      $
��Auto=�5s      ��5q      �� *
ProsesText+5s      � 5t      $	��0 5q�6   .+�5u      � $��70��6   $��15��5v      $
��Left=�5w       � Maximum 5x      �� *JumlahGenerasi+ � Value 5y      �� *GenerasiSekarang+5z      5{      5|      5}      