using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class NEAT
{
    public static RandomHashSet<ConnectionGene> generationConnections;
    public static Dictionary<ConnectionGene, NodeGene> generationNodes;
    public static int globalInnovationNumber;
    public static int generationCount;


    public static int MAX_NODES = (int)Mathf.Pow(2, 20);

    private int populationSize;
    public static int inputNodesSise, outputNodesSize;
    public static double compThresh;

    public static int sizeOfView;
    public static int fieldSize;

    public static double c1, c2, c3;
    public static double N = 1;
    public List<Genome> genomes;
    public static List<Dictionary<Vector2Int, int>> recordings;

    //CHANCES FOR MUTATIONS 
    public static double addConnection;
    public static double addNode;
    public static double setConnectionWeight;
    public static double shiftWeightConnection;
    public static double enableDisableConnection;

    private int generationNumber;

    public NEAT(int populationSize, int inputNodesSise, int outputNodesSize, int generationNumber)
    {
        this.populationSize = populationSize;
        NEAT.inputNodesSise = inputNodesSise;
        NEAT.outputNodesSize = outputNodesSize;

        NEAT.generationConnections = new RandomHashSet<ConnectionGene>();
        NEAT.generationNodes = new Dictionary<ConnectionGene, NodeGene>();

        NEAT.globalInnovationNumber = 1;
        NEAT.generationCount = 0;
        this.generationNumber = generationNumber;
    }
    public void setGAMERULES(int fieldSize, int sizeOfView)
    {
        NEAT.fieldSize = fieldSize;
        NEAT.sizeOfView = sizeOfView;
    }
    public void setCHANCES(double addConnection, double addNode, double setConnectionWeight, double shiftWeightConnection, double enableDisableConnection)
    {
        NEAT.addConnection = addConnection;
        NEAT.addNode = addNode;
        NEAT.setConnectionWeight = setConnectionWeight;
        NEAT.shiftWeightConnection = shiftWeightConnection;
        NEAT.enableDisableConnection = enableDisableConnection;
    }
    public static ConnectionGene GetConnectionGene(ConnectionGene connectionGene)
    {
        if (generationConnections.Contains(connectionGene))
        {
            connectionGene.innovationNumber = generationConnections[generationConnections.Get(connectionGene)].innovationNumber;
        }
        else
        {
            connectionGene.innovationNumber = globalInnovationNumber;
            generationConnections.Add(connectionGene);

            globalInnovationNumber++;
        }
        return connectionGene;
    }
    public static NodeGene GetNode(ConnectionGene connectionGene)
    {
        if (generationNodes.ContainsKey(connectionGene))
        {
            return generationNodes[connectionGene];
        }
        else
        {
            NodeGene newNode = new NodeGene(generationNodes.ElementAt(generationNodes.Count - 1).Value.innovationNumber + 1);
            generationNodes.Add(connectionGene, newNode);
            return newNode;
        }
    }

    public List<Dictionary<Vector2Int, int>> GetRecordings() => recordings;



    public void StartEvolution()
    {
        genomes = new List<Genome>();
        for (int j = 0; j < populationSize; j++)
        {
            genomes.Add(new Genome(inputNodesSise, outputNodesSize));
        }
    }

    public void NextGeneration()
    {
        if (generationCount > generationNumber) return;
        Debug.Log($"################ {generationCount} ################");
        generationConnections.Clear();
        generationNodes.Clear();


        //List<Genome> result = TestNetworks();
        List<Genome> result = GenomeTester.TestGenomes(genomes);

        double max = result.Max(x => x.fitness);

        Debug.Log($"Max fitness: {max}");

        //SPECIATE
        List<List<Genome>> species = Speciating.Speciate(result);

        //SELECT BEST NETWORKS FROM EACH SPECIES 

        //CROSSOVER
        //MUTATE
        generationCount++;
    }
}
