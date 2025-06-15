using Godot;
using System;

public partial class ClearBoard : TextureButton
{
    public void OnClick(){
        GlobalDates.BoardInstance.CreateBoard();
    }
}
