   M S B A M L   `   `   `  ����C  ?iStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:iStorageiStorage  5   	       iStorage.Login   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:iStorage     5   	   -   5          �Name$  login��5   	    � Title$ iStorage��5   	   $	��300��6   $	��500��6'    � 
ResizeMode$ NoResize=�64    � WindowStartupLocation$ CenterScreen=�6J   $��Times New Roman)�6o   $��20(�6�    � SizeToContent$ WidthAndHeight=�6�   ��5	      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5      $ 	#FFD4AF31��6   $ 1Q�6-   5      .��5      � 	 � ColumnDefinitions	 5   
   �� 5      .+�5   
   �� -   5        
 ��Name$
 login_label��5      $��Login��6%   $��196,20,168,0q�65   $	��Top=�6K   $��50��6c   $	��130��6o   ���	#FF5F5C2A$��33(�6�   $
��Bold"�6�   $��Center=�6�   �� 5   
   -   5        $
 
User_label��5      $��Benutzername��6$   $
��Left=�6;   $��27,64,0,177q�6V   $��Center=�6k   $	��149��6�   $��30��6�   $��20(�6�   $
��Bold"�6�   
���   �6�   �� 5   
   -   5         ��Name$ Username_Input��5      $
��Left=�6*   $��24��6E   $��26,99,0,148q�6Q    �� TextWrapping$
 Wrap=�6f   $��Center=�6z   $	��150��6�   
���   �$��16(�6�   
������ $��2q�6�   �� 5   
   -   5        $
 	Pwd_label��5      $��Passwort��6#   $
��Left=�66   $��27,136,0,105q�6Q   $��Center=�6g   $	��149��6�   $��30��6�   $��20(�6�   $
��Bold"�6�   G� 5   
   -   5         G�Name$ Password_Input��5      $
��Left=�6.   $��26,171,0,78q�6I   $��Center=�6^   $	��150��6y   $��22��6�   
������ 
���   �$��2q�6�   �� 5   
   -   5         ��Name$ Login_Button��5      $��Login��6'   $
��Left=�67   $��
86,220,0,0q�6R   $	��Top=�6f   $	��322��6~   $��27��6�   {�. 
������ $��2q�6�   
���   � �� 	IsDefault . 6  5      5      