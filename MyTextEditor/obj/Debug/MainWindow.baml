   M S B A M L   `   `   `  ����G  CMyTextEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyTextEditorMyTextEditor  5   	       MyTextEditor.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:MyTextEditor     5   	   -   5        $��myWindow��5   	     � Title$  Kedit Text Editor��5
   	   $	��450��6#   $	��800��60    � Icon$&  accessories-text-editor-icon.png��6<    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$	 New��5      6J     5   
   -   5        $
 Open��5      6L     5   
   -   5        $ Close��5      6N     5   
   -   5        $
 Save��5      6L     5   
   -   5        $ SaveAs��5      6P    J� InputBindings 5      �� 5   
    �� Command$ SaveAs��6   
 �� Key$ S��6&    �� 	Modifiers$ 
Ctrl+Shiftf�6.   .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��_File��6   .@�5      m� -   5        $��NewFile��5   .   $��	_New File��6   6=   m� 5      -   5        $��OpenFile��5   /   $��
_Open File��6   6?   m� 5      $��Open Folder...��6   {�.  63   m� 5      $��Open _Recent��6   .@�5      o� ��6   ��  �� ElementName$ myWindow��	 �� Path$ 	 TabManager.RecentFilesMenu���5      �� 5      .R�5   "   �� 
 �� Orientation$
 Vertical=�6-   5       5"      5#      m� 5$      $��Reopen with Encoding��6   {�.  69   m� 5%      $��N_ew View into File��6   {�.  68   m� 5&      $��_Save��6    m� Command$
 Save��6*   m� 5'      $��Save with Encoding��6   {�.  67   m� 5(      $��Save _As...��6   $ SaveAs��60   m� 5)      $��	Save A_ll��6   {�6.   �� $ myWindow��$	 TabManager.HasTabs��� 5*      m� 5+      $��New _Window��6   {�.  60   m� 5,      -	   5        $��Close Window��5,      61   �� 5-      m� 5.      $��_Close File��6   $ Close��60   m� 5/      $��Revert File��6   {�.  60   m� 50      -
   5        $$��Close All Files (Ctrl+Shift+W)��50      {�6e   �� $ myWindow��$	 TabManager.HasTabs��� 51      m� 52      -   5        $��E_xit��52      6*   53      54      �� 55   
   -   5         ��Name$ tabControl1��55   *   $	��Top=�6   ��6?   �� $ myWindow��$	 TabManager.Tabs���56      �� 57      .R�58      �� $��stackPanel1��6:   $
 
Horizontal=�6!   .+�59      �� ��6$   �� *Title+�� 5:      $��CloseButton��6�   -   5        $��0q�5:   B   
������ $��20,0,0,0q�6�   $��0q�6�   ��6!   �� *ModifiedIcon+��6V   �� *TabNo+6�   5;      5<       �� ContentTemplate 5>      �� 5?      6!   5A      �� 5B   
   -   5         ��Name$ 
txtContent��5B   '    �� AcceptsReturn . 6;   5C      5D      