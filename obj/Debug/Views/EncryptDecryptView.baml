   M S B A M L   `   `   `  ����]  YEncryptionDecriptionHashGeneration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullm;clr-namespace:EncryptionDecryptionHashGeneration.ViewModels-EncryptionDecryptionHashGeneration.ViewModels  5      A    ;EncryptionDecryptionHashGeneration.Views.EncryptDecryptView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Q	viewModel;clr-namespace:EncryptionDecryptionHashGeneration.ViewModels     5        � 	Resources  5	      Q  KEncryptionDecryptionHashGeneration.ViewModels.EncryptionDecryptionViewModel   BBVM��%   &	          5
   
   6A   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��344 �6   �� 5      $	��344 �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��5      $��0��5      $��2��5      $
��6,10q�6)   .+�5       �� $��1q�6   
���   �$
��6,10q�6=   ._�5!      �� -   5        $��FileAddress��5"      $	��450��5#      $��20(�5$      $
��6,10q�6&   ��5!      ��  �� Source# [   �� Path$ FileAddress�5$   &   5%      �� 5&      -   5        $��BrowseButton��5&      $��Browse��5'      $��30(�5)      $
��Bold"�5*      $��Comic Sans MS)�5+      $��Center=�5,      $��6,10,6,0q�61   $��50��6C   $	��Top=�6O   ��5(      �� # [  $ BrowseButtonCommand�5,   O   5-      �� 5.   
   $��MD5 Hash��6   $��20(�5/      $
��Bold"�50      $��1��51      $��0��52      $��2��53      $
��6,10q�6(   �� 54   
   $��1q�6   
���   �$
��6,10q�69   $��2��6J   $��0��55      $��2��56      $��4��57      ._�58      �� -   5        $��
HashedText��59      $��20(�5:      $
��6,10q�5;      ��58      �� # [  $ 
HashedText�5;      5<      �� 5=   
   $ 
Horizontal=�6   $��6��5>      $��0��5?      $
��6,10q�6%   $��2��5@      .+�5A      �� $	��Key��6   $��20(�5B      $
��Bold"�5C      $��Comic Sans MS)�5D      $
��6,10q�5E      �� 5F      -   5        $��KeyText��5F      $	��600��5H      $��20(�5I      ��5G      �� # [  $ KeyText�5I      5J      �� 5K   
   $ 
Horizontal=�6   $��7��5L      $��1��5M      $
��6,10q�6%   .+�5N      �� -   5        $��EncryptButton��5N      {�. $��Encrypt��5P      $��30(�5R      $
��Bold"�5S      $��Comic Sans MS)�5T      $��Center=�5U      $
��6,10q�61   ��5Q      �� # [  $ EncryptButtonCommand�5U   1   �� 5V      -   5        $��DecryptButton��5V      {�. $��Decrypt��5X      $��30(�5Y      $
��Bold"�5Z      $��Comic Sans MS)�5[      $��Center=�5]      $
��6,10q�62   ��5\      �� # [  $ DecryptButtonCommand�5]   2   5^      5_      5a      