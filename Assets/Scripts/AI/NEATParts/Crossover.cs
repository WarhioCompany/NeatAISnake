using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GenomesFunctions
{

    public static List<Pair<ConnectionGene>> MatchupGenomes(Genome first, Genome second)
    {
        List<Pair<ConnectionGene>> pairs = new List<Pair<ConnectionGene>>();

        //Так, мне надоело думать, поэтому сделаю плохо, но сделаю, прости я из будущего
        for (int i = 0; i < first.connectionGenes.Size(); i++)
        {
            pairs.Add(new Pair<ConnectionGene>(first.connectionGenes.Get(i), second.GetConnectionGeneByID(first.connectionGenes.Get(i).innovationNumber)));
        }
        for (int i = 0; i < second.connectionGenes.Size(); i++)
        {
            if (first.GetConnectionGeneByID(second.connectionGenes.Get(i).innovationNumber) == null)
            {
                pairs.Add(new Pair<ConnectionGene>(null, second.connectionGenes.Get(i)));
            }
        }
        pairs = pairs.OrderBy(x => (x.first != null ? x.first.innovationNumber : x.second.innovationNumber)).ToList();
        return pairs;
    }
    public static Genome Crossover(Genome first, Genome second)
    {
        List<Pair<ConnectionGene>> pairs = MatchupGenomes(first, second);

        //Print
        foreach (Pair<ConnectionGene> pair in pairs)
        {
            string fp = pair.first != null ? $"ID: {pair.first.innovationNumber} con: {pair.first.from.innovationNumber}->{pair.first.to.innovationNumber}" : "null";
            string sp = pair.second != null ? $"ID: {pair.second.innovationNumber} con: {pair.second.from.innovationNumber}->{pair.second.to.innovationNumber}" : "null";
            Debug.Log($"FIRST: {fp}");
            Debug.Log($"SECOND: {sp}");
        }


        bool firstIsFitter = first.fitness > second.fitness;

        Genome genome = new Genome();

        foreach (Pair<ConnectionGene> pair in pairs)
        {
            if (pair.first != null && pair.second != null)
            {
                genome.AddConnectionGene(Random.Range(0, 2) == 0 ? pair.first : pair.second);
            }
            else if ((firstIsFitter ? pair.first : pair.second) != null)
            {
                genome.AddConnectionGene(firstIsFitter ? pair.first : pair.second);
            }
            AddNode(genome[genome.ConnectionGenesCount() - 1].to.innovationNumber, genome);
            AddNode(genome[genome.ConnectionGenesCount() - 1].from.innovationNumber, genome);
        }
        return genome;
    }
    private static void AddNode(int nodeID, Genome genome)
    {
        genome.nodeGenes.Add(new NodeGene(nodeID));
    }
    public static Genome OldCrossover(Genome first, Genome second)
    {
        Genome genome = new Genome();
        Genome max = first.fitness > second.fitness ? first : second;

        int i;
        for (i = 0; i < Mathf.Min(first.ConnectionGenesCount(), second.ConnectionGenesCount()); i++)
        {
            if (first[i].innovationNumber != second[i].innovationNumber) break;
            genome.AddConnectionGene(Random.Range(0, 2) == 0 ? first[i] : second[i]);
        }
        for (; i < max.ConnectionGenesCount(); i++) genome.AddConnectionGene(max[i]);

        return genome;
    }
}