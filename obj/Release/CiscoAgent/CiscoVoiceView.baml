   M S B A M L   `   `   `  ���� )  %Genesyslab.Desktop.Modules.CiscoVoiceE    ?Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent.CiscoVoiceView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $&�� MySampleViewInteractionWorksheet��$��300.0��$��400.0��$
��auto��$
��auto��.��� $��10,10q�  � 	Resources     BoolToVisConverter  CloseButton��%G   &	        &	      �� �� $��Images/close.png��$��25��$��25�� � RowDefinitions �� $
��auto ��� $��* �.+�� $��0��$	��5,5q�
������� � ColumnDefinitions �� $
��auto ��� $��* �.+�� $��0��$
��10,5q�.+��� $	��0,5q�����  �� Path$ Dialog.Header�� $��1��$��Right=�$
��10,5q�.+��� -   $��Answer��$	��2,2q�$��Answer�� � ToolTip$ Answer��
 � Tag �� $ 	Dialog.ID�y��� $ Dialog.Answer� �� 	Converter# [  �� -   $��Transfer��$	��2,2q�$��SS Transfer��$ Direct Transfer�� �� $ 	Dialog.ID�y��� $ Dialog.Transfer�# [  �� -   $��	CTransfer��$	��2,2q�$��Transfer��$ Transfer�� �� $ 	Dialog.ID�y��� $ Dialog.CTransfer�# [  �� -   $��Consult��$	��2,2q�$��Consult��$ Consult�� �� $ 	Dialog.ID�y��� $ Dialog.Consult�# [  �� -   $��
Conference��$	��2,2q�$��
Conference��$ 
Conference�� �� $ 	Dialog.ID�y��� $ Dialog.Conference�# [  �� -   $��Keypad��$	��2,2q�$��Keypad��$ Keypad�� �� $ 	Dialog.ID�y��� $ Dialog.SendDTMF�# [  �� -   $
��Hold��$	��2,2q�$
��Hold��$
 Hold�� �� $ 	Dialog.ID�y��� $ Dialog.Hold�# [  �� -	   $��Resume��$	��2,2q�$��Resume��$ Resume�� �� $ 	Dialog.ID�y��� $ Dialog.Resume�# [  �� -
   $��Release��$	��2,2q�$��Release��$ Release�� �� $ 	Dialog.ID�y��� $ Dialog.Release�# [  � $��1�� �� �� .+�&   System.Windows.Controls.DataGrid  $��0��   AutoGenerateColumns . ��. ���� $* $Dialog.MediaProperties.CallVariables�  $��1�� . ��. ���� $ Dialog.Participants�