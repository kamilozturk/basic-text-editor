   M S B A M L   `   `   `  ���� G  CMyTextEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyTextEditorMyTextEditor      MyTextEditor.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:MyTextEditor     -   $��myWindow��  � Title$  Kedit Text Editor��$	��450��$	��800�� � Icon$&  accessories-text-editor-icon.png�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$	 New��  -   $
 Open��  -   $ Close��  -   $
 Save��  -   $ SaveAs�� J� InputBindings ��  �� Command$ SaveAs��
 �� Key$ S�� �� 	Modifiers$ 
Ctrl+Shiftf�.��]� .+�o� $	��Top=�.@�m� $��_File��.@�m� -   $��NewFile��$��	_New File��m� -   $��OpenFile��$��
_Open File��m� $��Open Folder...��{�.  m� $��Open _Recent��.@�o� ����  �� ElementName$ myWindow��	 �� Path$ 	 TabManager.RecentFilesMenu����� .R��� 
 �� Orientation$
 Vertical=�m� $��Reopen with Encoding��{�.  m� $��N_ew View into File��{�.  m� $��_Save�� m� Command$
 Save��m� $��Save with Encoding��{�.  m� $��Save _As...��$ SaveAs��m� $��	Save A_ll��{��� $ myWindow��$	 TabManager.HasTabs��� m� $��New _Window��{�.  m� -	   $��Close Window���� m� $��_Close File��$ Close��m� $��Revert File��{�.  m� -
   $$��Close All Files (Ctrl+Shift+W)��{��� $ myWindow��$	 TabManager.HasTabs��� m� -   $��E_xit���� -    ��Name$ tabControl1��$	��Top=����� $ myWindow��$	 TabManager.Tabs����� .R��� $��stackPanel1��$
 
Horizontal=�.+��� ���� *Title+�� $��CloseButton��-   $��0q�
������ $��20,0,0,0q�$��0q����� *ModifiedIcon+���� *TabNo+ �� ContentTemplate �� �� -    ��Name$ 
txtContent�� �� AcceptsReturn . 