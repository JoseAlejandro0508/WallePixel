public class Less:BinaryExpression<int,bool>{
    public Less(BasicValue<int> left_,BasicValue<int> right_,Token Operator):base(left_,right_,Operator)
    {
        IDOperator="<";

        Location_=Operator.Position;

    }
    public override void GetValue()
    {
        Value=left.Value < right.Value;
    }



}
