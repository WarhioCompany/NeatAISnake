public class ConnectionGene : Gene
{
    public NodeGene from;
    public NodeGene to;
    public double weight;
    public bool enabled;
    public ConnectionGene(NodeGene from, NodeGene to) : base(-1)
    {
        this.from = from;
        this.to = to;
        this.enabled = true;
    }
    public ConnectionGene(int innovationNumber, NodeGene from, NodeGene to, double weight) : this(from, to)
    {
        this.innovationNumber = innovationNumber;
        this.weight = weight;
    }
    public ConnectionGene(int innovationNumber, NodeGene from, NodeGene to, double weight, bool enabled) : this(innovationNumber, from, to, weight)
    {
        this.enabled = enabled;
    }

    
    public override bool Equals(object obj)
    {
        //return obj.GetHashCode() == GetHashCode();
        //     return true;
        if (obj == null || GetType() != obj.GetType()) return false;

        return (obj as ConnectionGene).from == from && (obj as ConnectionGene).to == to;

    }

    public override int GetHashCode()
    {
        //return 1;
        return NEAT.MAX_NODES * to.innovationNumber + from.innovationNumber;
    }
}