using System.Collections.Generic;
using UnityEngine;

public class Genome
{
    public RandomHashSet<ConnectionGene> connectionGenes { get; private set; }
    public RandomHashSet<NodeGene> nodeGenes { get; private set; }
    public double fitness;
    public double adjustedFitness;
    public int specieID;
    public Genome()
    {
        ClearGenome();
    }
    public Genome(int inputSize, int outputSize)
    {
        ClearGenome();
        for (int i = 0; i < inputSize; i++) nodeGenes.Add(new NodeGene(i + 1));
        for (int i = 0; i < outputSize; i++) nodeGenes.Add(new NodeGene(inputSize + i + 1));
        specieID = 0;
    }
    public void AddConnectionGene(ConnectionGene gene)
    {
        connectionGenes.Add(gene);
    }
    public void AddNodeGene(NodeGene gene)
    {
        nodeGenes.Add(gene);
    }
    public void ClearGenome()
    {
        connectionGenes = new RandomHashSet<ConnectionGene>();
        nodeGenes = new RandomHashSet<NodeGene>();
        fitness = 0;
    }
    public double getValueByID(int id) => nodeGenes[id].value;
    public ConnectionGene this[int key]
    {
        get => connectionGenes[key];
    }
    public ConnectionGene GetConnectionGeneByID(int innovationNumber)
    {
        for (int i = 0; i < connectionGenes.Size(); i++)
        {
            if (innovationNumber == connectionGenes[i].innovationNumber)
            {
                return connectionGenes[i];
            }
        }
        return null;
    }
    public void Calculate(List<double> inputs)
    {
        SetUpInputNeurons(inputs);

        HashSet<NodeGene> alreadyCalculated = new HashSet<NodeGene>();
        alreadyCalculated.Clear();

        for (int i = 0; i < NEAT.outputNodesSize; i++)
        {
            CalcValForNeuron(nodeGenes[i + NEAT.inputNodesSise], alreadyCalculated);
        }
    }
    private void SetUpInputNeurons(List<double> inputs)
    {
        if (inputs.Count != NEAT.inputNodesSise) throw new System.ArgumentException();

        for (int i = 0; i < NEAT.inputNodesSise; i++)
        {
            nodeGenes[i].value = inputs[i];
        }
    }
    private double CalcValForNeuron(NodeGene neuron, HashSet<NodeGene> alreadyCalculated)
    {
        for (int i = 0; i < ConnectionGenesCount(); i++)
        {
            if (connectionGenes[i].to == neuron)
            {
                if (alreadyCalculated.Contains(connectionGenes[i].from))
                {
                    neuron.value += connectionGenes[i].weight * connectionGenes[i].from.value;
                }
                else
                {
                    neuron.value += connectionGenes[i].weight * CalcValForNeuron(connectionGenes[i].from, alreadyCalculated);
                }
            }
        }

        neuron.value = AIHelpFunctions.Sigmoid(neuron.value);

        alreadyCalculated.Add(neuron);
        return neuron.value;
    }

    public int ConnectionGenesCount()
    {
        return connectionGenes.Size();
    }
    public int ConnectionGenesCountWithoutDisabledGenes()
    {
        int count = 0;
        for (int i = 0; i < ConnectionGenesCount(); i++) if (connectionGenes[i].enabled) count++;
        return count;
    }
    public int NodeGenesCount()
    {
        return nodeGenes.Size();
    }

    public void Mutate()
    {
        double chance = Random.Range(0, 1f);
        if (chance >= NEAT.addConnection)
        {
            AddConnectionMutation();
        }
        chance = Random.Range(0, 1f);
        if (chance >= NEAT.addNode)
        {
            AddNodeMutation();
        }
        chance = Random.Range(0, 1f);
        if (chance >= NEAT.setConnectionWeight)
        {
            ConnectionGene connectionGene = connectionGenes.Random();
            connectionGene.weight = Random.Range(-2f, 2f);
        }
        chance = Random.Range(0, 1f);
        if (chance >= NEAT.shiftWeightConnection)
        {
            ConnectionGene connectionGene = connectionGenes.Random();
            connectionGene.weight *= Random.Range(0, 2f);
        }
        chance = Random.Range(0, 1f);
        if (chance >= NEAT.enableDisableConnection)
        {
            ConnectionGene connectionGene = connectionGenes.Random();
            connectionGene.enabled = !connectionGene.enabled;
        }
    }
    public void AddConnectionMutation()
    {
        // "ELEGANT" Solution from.innovationNumber < to.innovationNumber {except "to" is outputNode}
        //SO I gonna use that

        ConnectionGene connectionGene = new ConnectionGene(new NodeGene(0), new NodeGene(0));

        NodeGene from;
        do
        {
            from = nodeGenes.Random();
        } while (isNodeOutput(from.innovationNumber));

        int inNumTo;
        do
        {
            inNumTo = Random.Range(NEAT.inputNodesSise + 1, NodeGenesCount() + 1);
        } while (inNumTo <= from.innovationNumber && !isNodeOutput(inNumTo));

        connectionGene.from = from;
        connectionGene.to = new NodeGene(inNumTo);

        Debug.Log($"{from.innovationNumber} to {inNumTo}");

        connectionGene = NEAT.GetConnectionGene(connectionGene);

        connectionGene.weight = Random.Range(-2f, 2f);


        connectionGenes.Add(connectionGene);
    }
    private bool isNodeOutput(int id) => NEAT.inputNodesSise < id && id <= NEAT.inputNodesSise + NEAT.outputNodesSize;

    private void AddNodeMutation()
    {
        ConnectionGene connectionGene = NEAT.generationConnections.Random();
        NodeGene nodeGene = NEAT.GetNode(connectionGene);

        ConnectionGene toNode = NEAT.GetConnectionGene(new ConnectionGene(connectionGene.from, nodeGene));
        toNode.weight = 1;
        ConnectionGene fromNode = NEAT.GetConnectionGene(new ConnectionGene(nodeGene, connectionGene.to));
        fromNode.weight = connectionGene.weight;

        connectionGene.enabled = false;

        nodeGenes.Add(nodeGene);
        connectionGenes.Add(toNode);
        connectionGenes.Add(fromNode);
    }
    public static Genome Crossover(Genome g1, Genome g2)
    {
        return GenomesFunctions.Crossover(g1, g2);
    }
}