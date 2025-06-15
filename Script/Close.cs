using Godot;
using System;

public partial class Close : TextureButton
{
    public void OnClick(){
        Node root = GetTree().Root;
        Control Config= root.GetNode<Control>("RootNode/ConfigDisplay");
        Config.Visible=! Config.Visible;
    }
}
