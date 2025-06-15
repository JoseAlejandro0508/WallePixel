using Godot;
using System;

public partial class TutorialLines : ItemList
{
    public Dictionary<int,Control> Db;
    public override void _Ready(){
        Node root = GetTree().Root;

        Db=new Dictionary<int,Control>(){
            { 0, root.GetNode<Panel>("RootNode/HelpPanel/Panel2") },
            { 1, root.GetNode<Panel>("RootNode/HelpPanel/Tipos de Datos") },
            { 2, root.GetNode<Panel>("RootNode/HelpPanel/Operaciones") },
            { 3, root.GetNode<Panel>("RootNode/HelpPanel/Condicionales") },
            { 4, root.GetNode<Panel>("RootNode/HelpPanel/Bucles") },
            { 5, root.GetNode<Panel>("RootNode/HelpPanel/Funciones Nativas") },
            { 6, root.GetNode<Panel>("RootNode/HelpPanel/RealTimeComp") },
        };
            foreach(Control c in Db.Values){
                c.Visible=false;
            }
        


        }

    public void _on_tutorial_lines_item_selected(int index){
        foreach(Control c in Db.Values){
            c.Visible=false;
        }
        Db[index].Visible=true;

    }
}
