   M S B A M L   `   `   `  ����E  ARentaAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RentaAdmin
RentaAdmin  5   	   =#clr-namespace:RentaAdmin.ViewModelsRentaAdmin.ViewModels  5   	       RentaAdmin.Autoresation   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:RentaAdmin     5   	   5model#clr-namespace:RentaAdmin.ViewModels     5   	   -   5          � Title$  Autoresation��5	   	   $	��200��6   $	��300��6+    � WindowStartupLocation$ CenterScreen=�67    � WindowStyle$
 None=�6\    � 	Resources 5
      1   +RentaAdmin.ViewModels.ViewModelAutorisation   	ViewModel �� 
TargetType �� Property �� Value(  "System.Windows.Controls.DatePicker��%�  &	        '��      '  �     '�� �     '��       5   
   6&   �� 5   
   " ��6   .�5      ��  � ��$ Center=�64   �� 5       � ��$ Center=�62   5      �� 5   
   "  6   .�5      ��  � ��$ Center=�62   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   5      5      .��5      �  � DataContext# �   6    � RowDefinitions 5    
   �� 5!      $
��145* �6   �� 5"      $	��39* �6   .+�5%   
   � $��0��6   $��5q�6    5&      �� 5'      �� 5(      �� 5)      	 � ColumnDefinitions	 5,      �� 5-      �� 5.      .+�51      �� $��0��6   $��0��6%   $��
Firstname:��65   �� 52      $��1��6   $��0��6%   $��	Lastname:��65   �� 53      $��2��6   $��0��6%   $��	Password:��65   �� 55      $��0��6   $��1��6#   ��56      �� *Admin.Firstname+
 �� UpdateSourceTrigger$
 PropertyChanged=��� 57      $��1��6   $��1��6#   ��58      �� *Admin.Lastname+$
 PropertyChanged=��� 59      $��2��6   $��1��6#   ��5:      �� *Admin.Password+$
 PropertyChanged=�5;      A� 5=   
   $��1��6    A� Columns$ 3��6#   .+�5>      �� $��Log in��6   ��6&   �� *LogIn+�� 5?      $��Sing up��6   ��6'   �� *SingUp+�� 5@      -   5        $��Cancel��5@      6&   5A      5B      5C      