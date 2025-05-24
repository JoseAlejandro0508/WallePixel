public class Pow:BinaryExpression<int,int>{
    public Pow(BasicValue<int> left_,BasicValue<int> right_,Token Operator):base(left_,right_,Operator)
    {
        IDOperator="%";

        Location_=Operator.Position;

    }
    public override void GetValue()
    {
        Value=(int)Math.Pow((double)left.Value,(double)right.Value);
    }


}