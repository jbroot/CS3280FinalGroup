   M S B A M L   `   `   `  ����J  FGroupAssignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:GroupAssignment.MainGroupAssignment.Main  5   	   "    GroupAssignment.Main.wndMain   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:GroupAssignment.Main     5   	     � Title$  Invoice Main��5   	   $	��450��6   $	��800��6+   ���	#FF428EDA.��5	      � .+�5
   
   ]� $��Stretch=�6   $	��Top=�62   $��0,0,0,0q�6J   $��2��6[   .+�5      o� $	��Top=�6   .@�5      m� $��Invoice��6   .@�5      m� -   5         m�Name$ 
menuSearch��5      $��Search for Invoice��63   6O   5      m� 5      $��	Configure��6   .@�5      m� -   5        $ 	menuItems��5      $��Update Items��62   6H   5      5      5      �� 5   
   -   5         ��Name$ labelInvoiceNumber��5      $��Invoice Number��6,   $
��Left=�6E   $��8,26,0,0q�6`   $	��Top=�6r    J� RenderTransformOrigin$ 0.5,0.5&�6�   �� 5   
   -   5        $ labelSelectItem��5      $��Select Item��6)   $
��Left=�6?   $��	33,50,0,0q�6Z   $	��Top=�6m   �� 5   
   -   5         ��Name$ comboBoxItemSelection��5      $
��Left=�62   $��
109,52,0,0q�6M   $	��Top=�6a   $	��204��6y   6�   �� 5   
   -   5        $ 
labelPrice��5      $��Price��6$   $
��Left=�64   $��	66,75,0,0q�6O   $	��Top=�6b   �� 5   
   -   5         ��Name$ textBoxPrice��5      $
��Left=�6(   $��23��6C   $��
109,77,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��120��6�   {�.  6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ datePickerInvoiceDate��5      $
��Left=�64   $��
320,26,0,0q�6O   $	��Top=�6c   �� 5   
   -	   5         ��Name$ 	buttonAdd��5      $��Add Item��6$   $
��Left=�67   $��
238,79,0,0q�6R   $	��Top=�6f   $��75��6~   6�   &   System.Windows.Controls.DataGrid  5    
   -
   5        	  Name$	 dataGridLineItems��5       $��Stretch=�6.   $��10,105,10,38q�6L   $��Stretch=�6b   
   AutoGenerateColumns
 .  4  .System.Windows.Automation.AutomationProperties   IsColumnHeader .    CanUserAddRows .  ��6�   �� *	lineItems+   Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      -   5        dataGridTextColumnItemCode��,  &System.Windows.Controls.DataGridColumn   Header$
 Code��5#   I      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6W   �� *item.ItemCode+   ClipboardContentBinding 6y   Z� 6�     5$      -   5        dataGridTextColumnItemDesc��$ Description��5$   I    .  6^   �� *item.ItemDesc+ 6�   Z� 6�     5%      -   5        dataGridTextColumnItemCost��$
 Cost��5%   I    .  6W   �� *item.ItemCost+ 6y   Z� 6�   .@�5'      =  7System.Windows.Controls.Primitives.DataGridColumnHeader  5(      �� 5)   
   -   5        $ 
labelTotal��5)      $��Total��6$   $��Right=�64   $��	0,0,136,9q�6P   $��Bottom=�6c   �� 5*   
   -   5        $ textBoxTotal��5*      $��Right=�6(   $��23��6D   $��	0,0,11,10q�6P   $
 Wrap=�6c   $��TextBox��6w   $��Bottom=�6�   $	��120��6�   {�.  6�   �� 5+   
   -   5        $ labelInvoiceNumberData��5+      $�� ��60   $
��Left=�6;   $��
109,26,0,0q�6V   $	��Top=�6j   $ 0.5,0.5&�6�   �� 5,   
   -   5        $ 	buttonNew��5,      $	��New��6$   $
��Left=�62   $��	11,0,0,11q�6M   $��Bottom=�6`   $��75��6{   6�   �� 5-   
   -   5        $ 
buttonEdit��5-      $
��Edit��6%   $
��Left=�64   $��	91,0,0,11q�6O   $��Bottom=�6b   $��75��6}   6�   �� 5.   
   -   5        $ 
buttonSave��5.      $
��Save��6%   $
��Left=�64   $��
368,0,0,11q�6O   $��Bottom=�6c   $��75��6~   6�   �� 5/   
   -   5        $ buttonClear��5/      $��Clear��6&   $
��Left=�66   $��
448,0,0,11q�6Q   $��Bottom=�6e   $��75��6�   6�   �� 50   
   -   5        $ buttonDelete��50      $��Delete Invoice��6'   $
��Left=�6@   $��
172,0,0,11q�6[   $��Bottom=�6o   $��90��6�   6�   �� 51   
   -   5        $��lblRemoveItemHint��51      $O��ITo remove an item, select it in the list and press delete on the keyboard��6)   $y�Hidden=�6}   $��Right=�6�   $��	0,88,13,0q�6�   $	��Top=�6�   52      53      