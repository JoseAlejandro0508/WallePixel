using Godot;
using System;

public partial class MenuControl : Control
{
    public Panel MenuButtonPanel;
    public TextureButton MenuExtended;
    public TextureButton MenuClosed;
    public Panel MenuButtonPanelExtended;
    public override void _Ready()
    {



        
    }

    public void _on_load_file_file_selected(string dir){
        if(!Directory.Exists(dir))return;
        string text = File.ReadAllText(dir);
        GlobalDates.CodeEditInstance.Text=text;


        
    }
    

}
