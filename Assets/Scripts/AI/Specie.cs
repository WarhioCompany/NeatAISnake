public class Specie
{
    public int ID;
    public List<Genome> members;
    public int offspringNumber;
    public int gensSinceImproved;
    public Specie(List<Genome> members){
        this.members = members;
    }
}