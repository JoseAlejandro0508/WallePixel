using Godot;
using System;

public partial class SizeSelect : SpinBox
{
    public override void _Ready()
    {
       Value=GlobalDates.BoardSize.x;

    }
    public void _on_value_changed(float value){
        GlobalDates.BoardSize=((int)value,(int)value);
        GlobalDates.BoardInstance.CreateBoard();
    }
    public override void _Process(double delta)
    {
        Value=GlobalDates.BoardSize.x;
    }
}
