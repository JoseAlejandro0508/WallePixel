using System.Numerics;

public interface IValue{

    public abstract void GetValue();
}
public abstract class BasicValue:IValue{
    public object? Value;
    public Location Location_;
    public abstract void GetValue();
    public abstract bool CheckSemantic(List<Error> ComplierErrors);
}
public abstract class PrimitiveDate:BasicValue{
    public Token TokenValue{get;private set;}

    public PrimitiveDate(Token token){
        TokenValue=token;
        Location_=token.Position;

    }
    override public bool CheckSemantic(List<Error> CE)
    {

        try{
            GetValue();


        }catch{
            CE.Add(new Error($"Error,el valor {TokenValue.Value} no se corresponde con el formato de un numero",Location_));
            return false;

        }
        return true;

    }


    
}
public abstract class BinaryExpression<IN,OUT>:BasicValue<OUT>{
    public BasicValue<IN> left;
    public BasicValue<IN> right;
    public Token Operator;
    public string IDOperator;
    
    public BinaryExpression(BasicValue<IN> left_,BasicValue<IN> right_,Token Operator_){
        Operator=Operator_;
        left=left_;
        right=right_;

    }
    public override bool CheckSemantic(List<Error> CE){

        if(IDOperator!=Operator.Value){
            throw new Exception("El Identificador de la operacion no se corresponde con el operador");
        }
        if(!left.CheckSemantic(CE)){
            CE.Add(new Error("Hay un error al obtener el valor de un operando",left.Location_));
            return false;
        }
        if(!right.CheckSemantic(CE)){
            CE.Add(new Error("Hay un error al obtener el valor de un operando",right.Location_));
            return false;
        }
        try{
            GetValue();
          
        
        }
        catch{
            CE.Add(new Error($"Error al ejecutar la operacion {Operator.Value}",Operator.Position));
            return false;

        }
        return true;
        
    }


}

public class Variable<OUT>:BasicValue<OUT>{
    public Token ID;
    public Token Asignator;
    public BasicValue<OUT> PrimitiveValue;
    public Compiling CR;

    public Variable(Token ID_,BasicValue<OUT> Value_,Token Asignator,Compiling CR){
        PrimitiveValue=Value_;
        ID=ID_;
        Location_=ID.Position;
        this.CR=CR;
        this.Asignator=Asignator;
        
    }
    public override void GetValue(){

        this.Value=PrimitiveValue.Value;



    }
    public override bool CheckSemantic(List<Error> CE){
        dynamic var_=null;
        if(CR.ProgramEnvironment.Check(ID,out var_)){
            bool result=var_.CheckSemantic(CE);
            if(!result)return false;
            this.PrimitiveValue=var_.PrimitiveValue;

           
        }
        if(!PrimitiveValue.CheckSemantic(CE)){
            CE.Add(new Error("Error al obtener el valor asociado a la variable",PrimitiveValue.Location_));
            return false;
        };
        try{
            GetValue();
        }
        catch{
            CE.Add(new Error($"Error al asociar el valor a la variable {ID}",Location_));
            return false;
        }
        
        return true;


    }

}
public abstract class Function<OUT>:BasicValue<OUT>{
    public List<Type> TypesNedded;
    public List<object> Params;
    
    public Function(List<Type> TypesNedded_, List<object> Params_){
        TypesNedded=TypesNedded_;
        Params=Params_;
    
    }
    public override bool CheckSemantic(List<Error> ComplierErrors){
        if(Params.Count!=TypesNedded.Count){
            ComplierErrors.Add(new Error($"Se esperaban {TypesNedded.Count} parametros y se recibieron {Params.Count}",Location_));
            return false;

        }
        for(int i=0;i<TypesNedded.Count;i++){
            if(TypesNedded[i]!=Params[i].GetType()){
                ComplierErrors.Add(new Error($"Se esperaban en el parametro {i} un tipo {TypesNedded[i].ToString()} parametros y se recibio un tipo {Params[i].GetType().ToString()}",Location_));
                return false;
            
            }
        }

        return true;

    }
    
}



