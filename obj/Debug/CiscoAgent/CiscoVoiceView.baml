   M S B A M L   `   `   `  ����)  %Genesyslab.Desktop.Modules.CiscoVoiceE    ?Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent.CiscoVoiceView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5        $&�� MySampleViewInteractionWorksheet��5      $��300.0��5      $��400.0��5      $
��auto��5      $
��auto��6   .��5      � $��10,10q�6     � 	Resources  5   
      BoolToVisConverter  CloseButton��%m   &	        &	      �� 5      6+   �� 5      $��Images/close.png��6(   $��25��6B   $��25��6P   5       � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   $	��5,5q�6   
������� 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      � $��0��6   $
��10,5q�6%   $
��Left=�63   .+�5      �� $��(��6   $	��0,5q�6%   �� 5      $	��2,5q�6U   ��6   ��  �� Path$( "Dialog.DialogStateTimer.TimerLabel�6U   �� 5      $��)��6   $	��0,5q�6%   �� 5      $	��2,5q�6@   ��6   �� $ Dialog.Header�6@   5      � 5       -   5        $��CallActionWaitingWindow��5       $��1��68   $��Center=�6E   $
��10,5q�6b   y�6p   �� *ShowCallWaiting+ �� 	Converter# [  .+�5!      �� $��Please wait��6   5"      � 5#      -   5        $��CallButtons��5#      $��1��6,   $��Center=�69   $
��10,5q�6V   y�6d   �� *ShowCallButtons+# [  .+�5%      �� -   5        $��Transfer��5%   r   $	��2,2q�68   $��Transfer��6E    � ToolTip$ Direct Transfer��6X   $��50��6�   
 � Tag 6   �� $ 	Dialog.ID�y�6�   �� $ Dialog.Transfer�# [  6�   �� 5&      -   5        $
��Hold��5&   c   $	��2,2q�68   $
��Hold��6E   $
 Hold��6T   $��50��6�    6   �� $ 	Dialog.ID�y�6o   �� $ Dialog.Hold�# [  6�   �� 5'      -   5        $��Resume��5'   g   $	��2,2q�68   $��Resume��6E   $ Resume��6V   $��50��6�    6   �� $ 	Dialog.ID�y�6u   �� $ Dialog.Resume�# [  6�   �� 5(      -   5        $��Release��5(   i   $	��2,2q�68   $��Release��6E   $ Release��6W   $��50��6�    6   �� $ 	Dialog.ID�y�6x   �� $ Dialog.Release�# [  6�   5)      � 5*      $��Center=�6   $��2��65   .+�5+      �� $��Dial Number ��6   �� 5,      -   5        $��NumberToDial��5,      $	��100��6.   5-      5.      � 5/   
   $��1��6    50      �� 51      �� 52      .+�54      &   System.Windows.Controls.DataGrid  $��0��6      AutoGenerateColumns . ��. ��6$   �� $* $Dialog.MediaProperties.CallVariables�6�     55      $��1��6    . ��. ��6$   �� $ Dialog.Participants�6p   56      57      58      