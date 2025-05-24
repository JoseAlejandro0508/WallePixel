public class Or:BinaryExpression<bool,bool>{
    public Or(BasicValue<bool> left_,BasicValue<bool> right_,Token Operator):base(left_,right_,Operator)
    {
        IDOperator="||";

        Location_=Operator.Position;

    }
    public override void GetValue()
    {
        Value=left.Value || right.Value;
    }



}
