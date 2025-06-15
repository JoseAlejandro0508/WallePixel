using Godot;
using System;

public partial class EditorLogic : CodeEdit
{
    public Dictionary<string,Godot.Color> KeywordsColor=new Dictionary<string,Color>(){
        {"fun", Colors.Orange},
        {"while", Colors.Purple},
        {"GoTo", Colors.Purple},
        {"if", Colors.Purple},
        {"else", Colors.Purple},

        

    };
    public override void _Ready()
    {
        InitHiglighting();

        
        
        
    }
    public void InitHiglighting(){
        CodeHighlighter codeHighlighter = new CodeHighlighter();
        SyntaxHighlighter=codeHighlighter;
        codeHighlighter.SymbolColor=Colors.Violet;
        codeHighlighter.NumberColor=Colors.Green;
        codeHighlighter.FunctionColor=Colors.Blue;
        foreach(var value in KeywordsColor){
            codeHighlighter.AddKeywordColor(value.Key,value.Value);
        }

    }
    

}
