using Godot;
using System;

public partial class AutoCompilerState : CheckButton
{
    public override void _Ready()
    {
        ButtonPressed=GlobalDates.Autoexecute;
        
    }
    public void _on_pressed(){
        GlobalDates.Autoexecute=ButtonPressed;
        GlobalDates.ConsoleInstance.Text="";
    }
    public override void _Process(double delta)
    {
        ButtonPressed=GlobalDates.Autoexecute;
    }
}
