   M S B A M L   `   `   `  ����F  BRoleplayApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RoleplayAppRoleplayApp  5   	       RoleplayApp.DiceRoll   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:RoleplayApp     5   	   
�������  � Title$  DiceRoll��5	   	   $	��600��6   $
��1000��6'   .��5
      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5        $��rollText��5      $��2��5      $��3��5       $��1��5!      $��60(�5"      $
��Bold"�5#      $��Dice roll here��5$      $��Center=�5%      $��Center=�5&      �� 5(   
   -   5         ��Name$ Dice6��5)   	   $��1��5*   	   $��4��5+   	   $��	6 terning��5,   	   $��25(�5-   	   $
��Bold"�5.   	   ���#c7c7c75/      �� 51   
   -   5        $ Dice10��52   	   $��3��53   	   $��4��54   	   $��
10 terning��55   	   $��25(�56   	   $
��Bold"�57   	   ���#c7c7c758      �� 5:   
   -   5        $ Dice20��5;   	   $��5��5<   	   $��4��5=   	   $��
20 terning��5>   	   $��25(�5?   	   $
��Bold"�5@   	   ���#c7c7c75A      �� 5C   
   -   5        $ GoBack��5D   	   $
��Back��5E   	   $��25(�5F   	   $
��Bold"�5G   	   ���#dc31315H      5I      5J      