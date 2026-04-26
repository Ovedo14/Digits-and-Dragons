using System.Collections.Generic;

public class LaneEquation
{
    public OperationType OpType;
    public List<float> Terms;

    public LaneEquation(OperationType opType, List<float> terms)
    {
        OpType = opType;
        Terms = terms;
    }
}