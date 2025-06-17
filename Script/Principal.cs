using Godot;
using System;
using System.Collections.Generic;

public partial class Principal : Control
{
    public Panel MenuButtonPanel;
    public TextureButton MenuExtended;
    public TextureButton MenuClosed;
    public Panel MenuButtonPanelExtended;
    public FileDialog LoadFile;
    public FileDialog SaveFile;
    public string CodeText;
    public Compiling Compiler;
    public TextureButton CompileButton;
    public AcceptDialog CompilingInfo;
    public bool Blocked = false;
    public override void _Ready()
    {
        List<Error> CE = new List<Error>();
        Environment Env = new Environment(CE);
        GlobalDates.BoardInstance = GetNode<BoardControl>("BoardContainer/Board");
        GlobalDates.CodeEditInstance = GetNode<CodeEdit>("EditorContainer/CodeEdit");
        GlobalDates.ConsoleInstance = GetNode<CodeEdit>("EditorContainer/Console");

        CompileButton = GetNode<TextureButton>("MenuButtons/CompileButton");
        CompileButton.Pressed += CompilateCode;
        MenuButtonPanel = GetNode<Panel>("MenuButtonPanel");
        MenuButtonPanelExtended = GetNode<Panel>("MenuButtons");
        MenuExtended = GetNode<TextureButton>("MenuButtonPanel/MenuButton");
        MenuClosed = GetNode<TextureButton>("MenuButtons/MenuButton");
        LoadFile = GetNode<FileDialog>("LoadFile");
        SaveFile = GetNode<FileDialog>("SaveFile");

        Node root = GetTree().Root;
        CompilingInfo = root.GetNode<AcceptDialog>("RootNode/AcceptDialog");
        CompilingInfo.Confirmed += ActivateAutoExecute;





    }

    public void ReadCodeFromEditor()
    {
        CodeText = GlobalDates.CodeEditInstance.Text;
        List<Error> CE = new List<Error>();
        List<string> Logs = new List<string>();
        Environment Env = new Environment(CE);
        LexicalAnalizer lex = new LexicalAnalizer(CodeText, "a");
        lex.GetTokens(CE);
        TokenStream stream = new TokenStream(lex.Tokens, CE);
        Parser parser = new Parser(stream, CE, Env);
        Compiler = new Compiling(Env, CE, Logs, parser);

    }
    public void CompilateCode()
    {
        try
        {
            Blocked = true;
            GlobalDates.ActualPos = (0, 0);
            Blocked = true;
            ReadCodeFromEditor();
            Compiler.GetAllDeclarations();
            Compiler.Interprete();


        }
        catch
        {

        }
        GlobalDates.FinishCompiling = true;
        DisplayErrors();
        DisplayLogs();
        CompilingInfo.DialogText = $"Compilacion completada con {Compiler.CE.Count}errores";
        CompilingInfo.Visible = true;
        Blocked = true;

    }
    public void ActivateAutoExecute()
    {
        Blocked=false;
        GlobalDates.FinishCompiling = false;
    }
    public void CompilateCode(bool AutoExecute = false)
    {
        if (Blocked) return;
        try
        {
            Blocked = true;
            GlobalDates.ActualPos = (0, 0);
            Blocked = true;
            ReadCodeFromEditor();
            Compiler.GetAllDeclarations();
            if (GlobalDates.Autoexecute){
                 Compiler.Interprete(AutoExecute);

             }
           


        }
        catch
        {

        }
        DisplayErrors();
        DisplayLogs();

        Blocked = false;

    }
    public void PressMenuExtended()
    {

        MenuButtonPanel.Visible = !MenuButtonPanel.Visible;
        MenuButtonPanelExtended.Visible = !MenuButtonPanelExtended.Visible;

    }
    public void ImportButton()
    {
        LoadFile.Visible = true;

    }
    public void ExportButton()
    {
        SaveFile.Visible = true;

    }




    public void DisplayErrors()
    {
        GlobalDates.ConsoleInstance.Text = "";
        foreach (Error error in Compiler.CE)
        {
            GlobalDates.ConsoleInstance.Text += error.ToString();
        }
    }
    public void DisplayLogs()
    {

        foreach (string log in Compiler.Logs)
        {
            GlobalDates.ConsoleInstance.Text += $"[ Log ]{log}";
        }
    }

    public void Autoexecute()
    {
        CompilateCode(AutoExecute: true);
    }
    public void _on_code_edit_text_changed(){
        Autoexecute();

    }

    public override void _Process(double delta)
    {
        


        //ReadCodeFromEditor();
        //Compiler.GetAllDeclarations();

    }







}
