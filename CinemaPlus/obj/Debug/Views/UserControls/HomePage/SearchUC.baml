   M S B A M L   `   `   `  ����E  ACinemaPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:CinemaPlus.Views.UserControls.HomePage&CinemaPlus.Views.UserControls.HomePage  5      5    /CinemaPlus.Views.UserControls.HomePage.SearchUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2gif"http://wpfanimatedgif.codeplex.com     5      Flocal4clr-namespace:CinemaPlus.Views.UserControls.HomePage     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      $	��763��5
      $
��1500��5         
firstColor#���   5      .��5      �   � RowDefinitions  5   
   �� 5      $	��100 �6   6+   �� 5      $	��656 �6   6,   .+�5   
   � .+�5      ��   secondColor#���  6   $	��Top=�5      $��Center=�5      $	��100��5      $
��1500��5       �� Orientation$ 
Horizontal=�5      .+�5      �� 
������ $	��100��5      $
��1500��6&   .+�5       �� -   5        $��SearchTB��5"        MyTextBoxStyle2#��[ 5       $��46��5!      $��	15,1,35,0q�5#      $��27(�5$        fourthColor#��[ 5%      $��Search For Movie . . .��5&       �� Left$	 400��5'      
 �� Top$ 26��5(      $
��Left=�5)      $��Center=�5*      $	��645��5+      .��6*    J� InputBindings 5,      �� 5-      
 �� Key$ Return��6)    �� Command 66   �� *SearchCommand+6Z   .��5.   1   .  (System.Windows.Interactivity.Interaction   Triggers 5/      /  )System.Windows.Interactivity.EventTrigger  50         	EventName$ 
MouseEnter��6-   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 51   "   6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 68   �� *MouseEnterCommand+52        54      $ 
MouseLeave��6-   .	 55   "     
 68   �� *MouseLeaveCommand+56        58      $ GotFocus��6-   .	 59   "     
 68   �� *IsFocusedCommand+5:        5<      $ 	LostFocus��6-   .	 5=   "     
 68   �� *IsNotFocusedCommand+5>      .��5?   2   5@      �� 5B      $��\Images\clear.png��6    � Cursor$
 Hand��67   $��23��6E   $��20��6Q   $
 1017��6\   $ 38��6o   $
��Left=�6   $��Center=�6�    J� RenderTransformOrigin$ 3.3,0.6&�6�    5C      d� 5D       d� Gesture$ 	LeftClicka�6+    6?   �� *ClearCommand+6b   5F      �� 5H        MyButtonStyle4#��[ 6    �� Right$	 400��5I      $��46��5J      $ 26��5K      $��33(�5L      $��\Fonts\#Ponder-Black)�5M      $��Normal"�5N      $
 Hand��5O      $��55��5P   ?   $
 1045��6J   $	��Top=�6]   ��6   �� *SearchCommand+6]   �� 5Q      $��\Images\Search.png��6   $��32��5R       $��32��5S       $	 400��5T       $ 33��5U       $
 Hand��5V       $��Center=�5W       $	��Top=�5X       $
 1056��5Y        5Z      d� 5[      $ 	LeftClicka�6+    6?   �� *SearchCommand+6c   5]      5^      5_      5`      � 5c   
   $��1��6   .+�5d      �� -   5        $��SearchUCScroll��5g      $
��Auto=�5d      $��Disabled=�5e      $
��1500��5f      $��Center=�5h      .��5i      � -   5        $��SearchedMoviesWrapPanel��5i      $��100,40,100,-15q�5j      $
��1280��64   5k      5l      5m      5o      5p      