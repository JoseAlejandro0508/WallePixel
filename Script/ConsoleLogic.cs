using Godot;
using System;

public partial class ConsoleLogic: CodeEdit
{
    
    public override void _Ready()
    {
        InitHiglighting();

        
        
        
    }
    public void InitHiglighting(){
        CodeHighlighter codeHighlighter = new CodeHighlighter();
        SyntaxHighlighter=codeHighlighter;

        codeHighlighter.NumberColor=Colors.Green;
        codeHighlighter.SymbolColor=Colors.Purple;
        
        codeHighlighter.AddKeywordColor("SemanticError",Colors.Yellow);
        codeHighlighter.AddKeywordColor("LexicError", Colors.Red);
        codeHighlighter.AddKeywordColor("Unknown", Colors.Purple);
        codeHighlighter.AddKeywordColor("SintacticError", Colors.Orange);
        codeHighlighter.AddKeywordColor("RuntimeError", Colors.LightGreen);
        codeHighlighter.AddKeywordColor("Log", Colors.Green);

    }
    

}
