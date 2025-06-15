using Godot;
public static class GlobalDates{
    public static Dictionary<string,Color> ColorsDictionary=new Dictionary<string,Color>{
        {"Red", Colors.Red},
        {"Green", Colors.Green},
        {"Blue", Colors.Blue},
        {"Yellow", Colors.Yellow},
        {"Orange", Colors.Orange},
        {"Purple", Colors.Purple},
        {"Black",Colors.Black},
        {"White", Colors.White},
        {"Transparent",Colors.Transparent}
    };
    public static (int x,int y)ActualPos=(0,0);
    public static (int x,int y)BoardSize=(50,50);
    public static BoardControl BoardInstance;
    public static CodeEdit CodeEditInstance;
    public static CodeEdit ConsoleInstance;
    public static string BrushColor="Transparent";
    public static string BoardColor="White";
    public static int BrushSize=1;
    public static int MaxBucleable=1000;

    public static bool Autoexecute=true;
    public static bool FinishCompiling=false;
    public static string[,] BoardMatrix;
    public static void InitBoardMatrix(){
        BoardMatrix=new string[BoardSize.x,BoardSize.y]; 
        for(int i=0;i<BoardSize.x;i++){
            for(int j=0;j<BoardSize.y;j++){

                BoardMatrix[i,j]=BoardColor;
            }

        }
    }
    public static bool IsValidPos(IEnumerable<(int x,int y)> Positions){
        foreach(var pos in Positions){
            if(pos.x<0 || pos.x>BoardSize.x || pos.y<0 || pos.x>BoardSize.y)return false;
        }
        return true;
    }
    
    

}