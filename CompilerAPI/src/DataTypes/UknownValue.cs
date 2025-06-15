
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

        if(Date is not ValueType && Date is not string){
            CE.Add(new Error("El token no pose un formato de valor valido",null,ErrorType.SemanticError));
            return false;
        }
        try{
            GetValue(CE);


        }catch{
            return false;

        }
        return true;

    }

}