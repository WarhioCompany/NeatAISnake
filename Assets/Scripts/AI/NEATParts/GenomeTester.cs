using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class GenomeTester
{
    public static List<Genome> TestGenomes(List<Genome> genomes)
    {
        List<Tester> testers = new List<Tester>();
        foreach (Genome genome in genomes)
        {
            Tester tester = new Tester(genome);
            tester.Play();
            testers.Add(tester);
        }

        for (int i = 0; i < testers.Count; i++)
        {
            if (!testers[i].done)
            {
                i = -1;
            }
            else
            {
                Debug.Log($"{i} is DONE");
                genomes[i].fitness = testers[i].getFitness();
                //NEAT.recordings = testers[0].recordings;
            }
        }
        return genomes;
    }
    // private static async void WaitUntilTestFinishes(List<Tester> networks)
    // {
    //     double res;
    //     do
    //     {
    //         res = 1;

    //         foreach (Tester network in networks) res *= network.genome.fitness;

    //         await Task.Delay(100);
    //     } while (res == 0);

    // }
}