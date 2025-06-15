public class Fill : Calleable
{
    public Fill(Token Caller_, Compiling CR_) : base(Caller_, 0, CR_)
    {

        TypeReturn = FunctionType.Void;
    }

    protected override bool CheckType(List<object> Arguments)
    {

        return true;

    }

    protected override void call(List<object> Arguments)
    {
        (int x,int y)pos;
        pos.x=GlobalDates.ActualPos.x;
        pos.y=GlobalDates.ActualPos.y;
        string[,] Matrix=GlobalDates.BoardMatrix;
        Queue<(int x,int y)>queue_=new Queue<(int x,int y)>();
        queue_.Enqueue(pos);
        List<(int x,int y)>Mov=[(0,1),(0,-1),(1,0),(-1,0)];
        string color=Matrix[pos.x,pos.y];
        bool[,] Visited=new bool[GlobalDates.BoardSize.x,GlobalDates.BoardSize.y];
        while(queue_.Count!=0){
            (int x,int y)actual=queue_.Dequeue();
            
            GlobalDates.BoardInstance.SetPixel(actual.x,actual.y,GlobalDates.BrushColor);
            foreach(var dir in Mov){
                if(actual.x+dir.x<GlobalDates.BoardSize.x && actual.x+dir.x>=0 && actual.y+dir.y<GlobalDates.BoardSize.y && actual.y+dir.y>=0 && Matrix[actual.x+dir.x,actual.y+dir.y]==color && !Visited[actual.x+dir.x,actual.y+dir.y]){
                    queue_.Enqueue((actual.x+dir.x,actual.y+dir.y));
                    Visited[actual.x+dir.x,actual.y+dir.y]=true;

                }
            }
        } 
        




    }


}