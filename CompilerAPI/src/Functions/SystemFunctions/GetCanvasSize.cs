public class GetCanvasSize:Calleable{
    public GetCanvasSize(Token Caller_,Compiling CR_):base(Caller_,0,CR_){

        TypeReturn=FunctionType.Return;
    }

    protected override bool CheckType(List<object> Arguments){

        return true;

    }
    protected override void call(List<object> Arguments){
        Value=GlobalDates.BoardSize.x;
        return;
        

    }


}
