public class GetActualX:Calleable{
    public GetActualX(Token Caller_,Compiling CR_):base(Caller_,0,CR_){

        TypeReturn=FunctionType.Return;
    }

    protected override bool CheckType(List<object> Arguments){

        return true;

    }
    protected override void call(List<object> Arguments){
        Value=GlobalDates.ActualPos.x;
        return;
        

    }


}
public class GetActualY:Calleable{
    public GetActualY(Token Caller_,Compiling CR_):base(Caller_,0,CR_){

        TypeReturn=FunctionType.Return;
    }

    protected override bool CheckType(List<object> Arguments){

        return true;

    }
    protected override void call(List<object> Arguments){
        Value=GlobalDates.ActualPos.y;
        return;
        

    }


}