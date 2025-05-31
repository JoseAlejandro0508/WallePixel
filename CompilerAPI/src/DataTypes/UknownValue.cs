
public class UknownValue:BasicValue{
    public object Date;
    public UknownValue(object Date){
        this.Date = Date;
    }
    public override void GetValue(List<Error> CE)
    {
        Value=Date;
        
    }

    override public bool CheckSemantic(List<Error> CE)
    {

        if(Date is not ValueType)return false;
        try{
            GetValue(CE);


        }catch{
            return false;

        }
        return true;

    }

}