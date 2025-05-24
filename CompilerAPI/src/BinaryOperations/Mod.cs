public class Mod:BinaryExpression<int,int>{
    public Mod(BasicValue<int> left_,BasicValue<int> right_,Token Operator):base(left_,right_,Operator)
    {
        IDOperator="%";

        Location_=Operator.Position;

    }
    public override void GetValue()
    {
        Value=(int)left.Value%right.Value;
    }




}