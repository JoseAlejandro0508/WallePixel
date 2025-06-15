using Godot;
using System;

public partial class TutorialClose : TextureButton
{
    public void OnClick(){
        Node root = GetTree().Root;
        ItemList HelpPanel= root.GetNode<ItemList>("RootNode/Tutorial");
        HelpPanel.Visible=false;
    }
}
