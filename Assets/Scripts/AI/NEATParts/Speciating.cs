using System.Collections.Generic;
using UnityEngine;
public static class Speciating
{
    public static List<List<Genome>> Speciate(List<Genome> genomes, List<Genome> prev = null)
    {
        List<List<Genome>> species = new List<List<Genome>>();

        List<Genome> buf = new List<Genome>(genomes);

        for (int i = 0; buf.Count != 0; i++)
        {
            Genome leader;
            if (prev == null) leader = buf[Random.Range(0, buf.Count)];
            else leader = prev[]//Change this

            species.Add(new List<Genome>());
            for (int j = 0; i < genomes.Count; j++)
            {
                if (GetDistance(leader, genomes[j]) <= NEAT.compThresh)
                {
                    genomes[j].specieID = leader.specieID;
                    species[i].Add(genomes[j]);
                    buf.Remove(genomes[j]);
                }
            }
            //IMPORTANT: I am currently deleting single member species 
            if (species[i].Count == 1)
            {
                species.RemoveAt(i);
            }
        }

        foreach (List<Genome> specie in species)
        {
            foreach (Genome genome in specie)
            {
                genome.adjustedFitness = genome.fitness / specie.Count;
            }
        }
    }
    public static int GetNumberOfOffspringForSpecie(double avgAdjFitness, double avgGlobalFitness, int membersCount)
    {
        return (int)System.Math.Round(avgAdjFitness / avgGlobalFitness * membersCount);
    }

    public static double GetDistance(Genome first, Genome second)
    {
        List<Pair<ConnectionGene>> pairs = GenomesFunctions.MatchupGenomes(first, second);

        int notMatchingGenesCount = 0;

        double W = 0;
        bool isExcessFinished = false;
        int D = 0, E = 0;
        for (int i = pairs.Count - 1; i != 0; i--)
        {
            if (first[i] != null && second[i] != null)
            {
                W += System.Math.Abs(first[i].weight - second[i].weight);
            }
            else
            {
                if (isExcessFinished)
                {
                    D++;
                }
                else
                {
                    if ((pairs[pairs.Count - 1].first == null) != (pairs[i].first == null)) isExcessFinished = true;
                    else E++;
                }
            }
        }

        W /= Mathf.Max(first.ConnectionGenesCount(), second.ConnectionGenesCount()) - notMatchingGenesCount;

        // int E = Mathf.Abs(first.ConnectionGenesCountWithoutDisabledGenes() - second.ConnectionGenesCountWithoutDisabledGenes());
        // int D = notMatchingGenesCount - E;

        return (NEAT.c1 * D + NEAT.c2 * E) / Mathf.Max(first.ConnectionGenesCountWithoutDisabledGenes(), second.ConnectionGenesCountWithoutDisabledGenes()) + (NEAT.c3 * W);
    }
}