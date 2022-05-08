using UnityEngine;
public class NodeGene : Gene
{
    public double value;
    public NodeGene(int innovationNumber) : base(innovationNumber)
    {

    }
    public static bool operator ==(NodeGene left, NodeGene right)
    {
        return left.Equals(right);
    }
    public static bool operator !=(NodeGene left, NodeGene right)
    {
        return !left.Equals(right);
    }
    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        return (obj as NodeGene).innovationNumber == innovationNumber;
    }
    public override int GetHashCode()
    {
        return innovationNumber;
    }
}