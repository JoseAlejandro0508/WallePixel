using Godot;
using System;

public partial class Help : TextureButton
{
    public void OnClick(){
        Node root = GetTree().Root;
        Panel HelpPanel= root.GetNode<Panel>("RootNode/HelpPanel");
        HelpPanel.Visible=!HelpPanel.Visible;
    }
}
