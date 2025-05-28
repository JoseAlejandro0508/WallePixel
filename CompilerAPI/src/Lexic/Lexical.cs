public static class LexicDates{
    public static Dictionary<string,TokenInfo>Keywords=new Dictionary<string, TokenInfo>();
    public static Dictionary<string,TokenInfo>Bucles=new Dictionary<string, TokenInfo>();
    public static Dictionary<string,TokenInfo>Operators=new Dictionary<string, TokenInfo>();
    
    

    public static void RegisterKeyword(string keyword,TokenInfo Info){
        Keywords[keyword]= Info;
    }
    public static void RegisterBucle(string Bucle,TokenInfo Info){
        Bucles[Bucle]= Info;
    }
    public static void RegisterOperator(string Oper,TokenInfo Info){
        Operators[Oper]= Info;
    }


    public static void InitLexic(){

        RegisterKeyword("Spawn",new TokenInfo(TokenIDS.SpawnFunc));
        RegisterKeyword("Color",new TokenInfo(TokenIDS.ColorFunc));
        RegisterKeyword("Size",new TokenInfo(TokenIDS.SizeFunc));
        RegisterKeyword("DrawLine",new TokenInfo(TokenIDS.DrawLineFunc));
        RegisterKeyword("DrawCircle",new TokenInfo(TokenIDS.DrawCircleFunc));
        RegisterKeyword("DrawRectangle",new TokenInfo(TokenIDS.DrawRectangleFunc));
        RegisterKeyword("Fill",new TokenInfo(TokenIDS.FillFunc));
        RegisterKeyword("GetActualX",new TokenInfo(TokenIDS.GetActualXFunc));
        RegisterKeyword("GetActualY",new TokenInfo(TokenIDS.GetActualYFunc));
        RegisterKeyword("GetCanvasSize",new TokenInfo(TokenIDS.GetCanvasSizeFunc));
        RegisterKeyword("GetColorCount",new TokenInfo(TokenIDS.GetColorCountFunc));
        RegisterKeyword("IsBrushColor",new TokenInfo(TokenIDS.IsBrushColorFunc));
        RegisterKeyword("IsBrushSize",new TokenInfo(TokenIDS.IsBrushSizeFunc));
        RegisterKeyword("IsCanvasColor",new TokenInfo(TokenIDS.IsCanvasColorFunc));
        RegisterKeyword("IsColor",new TokenInfo(TokenIDS.IsColorFunc));
        RegisterKeyword("if",new TokenInfo(TokenIDS.IF));
        RegisterKeyword("else",new TokenInfo(TokenIDS.ELSE));

        RegisterBucle("while",new TokenInfo(TokenIDS.WHILEBulce));
        RegisterBucle("GoTo",new TokenInfo(TokenIDS.GoToBucle));

        RegisterOperator("*",new TokenInfo(TokenIDS.MultOper));
        RegisterOperator("<-",new TokenInfo(TokenIDS.AssignatorOper));
        RegisterOperator("+",new TokenInfo(TokenIDS.AddOper));
        RegisterOperator("-",new TokenInfo(TokenIDS.RestOper));
        RegisterOperator("/",new TokenInfo(TokenIDS.DivOper));
        RegisterOperator("**",new TokenInfo(TokenIDS.PowOper));
        RegisterOperator("%",new TokenInfo(TokenIDS.ModOper));
        RegisterOperator("==",new TokenInfo(TokenIDS.EqualOper));
        RegisterOperator("<=",new TokenInfo(TokenIDS.LessEqualOper));
        RegisterOperator(">=",new TokenInfo(TokenIDS.MoreEqualOper));
      
        RegisterOperator("<",new TokenInfo(TokenIDS.LessOper));
        RegisterOperator(">",new TokenInfo(TokenIDS.MoreOper));
        RegisterOperator("&&",new TokenInfo(TokenIDS.AndOper));
        RegisterOperator("||",new TokenInfo(TokenIDS.OrOper));

        RegisterOperator(",",new TokenInfo(TokenIDS.SeparatorParam));
        RegisterOperator('"'.ToString(),new TokenInfo(TokenIDS.SeparatorString));
        RegisterOperator("(",new TokenInfo(TokenIDS.OpenParenteses));
        RegisterOperator(")",new TokenInfo(TokenIDS.CloseParenteses));
        RegisterOperator("[",new TokenInfo(TokenIDS.OpenCorchete));
        RegisterOperator("]",new TokenInfo(TokenIDS.CloseCorchete));
        RegisterOperator("{",new TokenInfo(TokenIDS.OpenBlock));
        RegisterOperator("}",new TokenInfo(TokenIDS.CloseBlock));

        
    }

}


public class TokenInfo{
    public TokenIDS tokenIDS;
    //Parseo
    public TokenInfo(TokenIDS tokenIDS){
        this.tokenIDS = tokenIDS;
    }

}

public enum TokenIDS{

    AssignatorOper,//<-
    MultOper,//*
    AddOper,//+
    RestOper,//-
    DivOper,// /
    PowOper,// **
    ModOper,// %

    EqualOper,//==
    MoreEqualOper,// >=
    LessEqualOper,//<=
    MoreOper,// >
    LessOper,//<
    AndOper,// &&
    OrOper,// ||
    SeparatorParam,// ,
    SeparatorString,// "
    OpenParenteses,// (
    CloseParenteses,// )
    OpenCorchete,// [
    CloseCorchete,// ]
    OpenBlock,// {
    CloseBlock,// }
    


    GoToBucle,// GoTo
    WHILEBulce,//while
    SpawnFunc,//Spawn
    ColorFunc,//Color
    SizeFunc,//Size
    DrawLineFunc,//DrawLine
    DrawCircleFunc,//DrawCircle
    DrawRectangleFunc,//DrawRectangle
    FillFunc,//Fill
    GetActualXFunc,//GetActualX
    GetActualYFunc,//GetActualY
    GetCanvasSizeFunc,//GetCanvasSize
    GetColorCountFunc,//GetColorCount
    IsBrushColorFunc,//IsBrushColor
    IsBrushSizeFunc,//IsBrushSize
    IsCanvasColorFunc,//IsCanvasColor
    IsColorFunc,//IsColor
    Tag,// text

    IF,//if
    ELSE,//else
}