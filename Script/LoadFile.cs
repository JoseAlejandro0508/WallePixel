using Godot;
using System;

public partial class LoadFile : FileDialog
{

    public override void _Ready()
    {
        SetFilters(new string[] {
          "*.pw ; Walle Code",

      });

        
    }

    public void LoadFileButton(string dir){
        Node root = GetTree().Root;
        AcceptDialog acceptDialog= root.GetNode<AcceptDialog>("RootNode/AcceptDialog");
        if(!dir.EndsWith(".pw")){
            acceptDialog.DialogText= "Extension de archivo invalida";
            acceptDialog.Visible=true;
            return;
        }
        string text = File.ReadAllText(dir);
        GlobalDates.CodeEditInstance.Text=text;
    }
    

}
