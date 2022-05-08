using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class Tester
{
    private Genome genome;
    public bool done;
    public List<Dictionary<Vector2Int, int>> recordings;

    private HashSet<NodeGene> alreadyCalculated = new HashSet<NodeGene>();

    public Tester(Genome genome)
    {
        this.genome = genome;
        recordings = new List<Dictionary<Vector2Int, int>>();
        done = false;
    }
    public void Play()
    {
        Thread t = new Thread(() =>
        {
            recordings = new List<Dictionary<Vector2Int, int>>();
            Snake snake = new Snake(NEAT.fieldSize);
            while (!snake.isDead)
            {
                genome.Calculate(snake.GetInfo(NEAT.sizeOfView));
                
                Debug.Log($"O1 {genome.nodeGenes[NEAT.inputNodesSise].value}");
                Debug.Log($"O2 {genome.nodeGenes[NEAT.inputNodesSise + 1].value}");
                Debug.Log($"O3 {genome.nodeGenes[NEAT.inputNodesSise + 2].value}");

                int direction = getDirFromOutput();

                // snake.Move((int)System.Math.Round(genome.nodeGenes[inputNodesCount - 1 + outputNodesCount].value));

                snake.Move(direction);

                recordings.Add(snake.GetField());

                //TEST
                break;
            }
            genome.fitness = snake.GetScore();
            done = true;
        });//new ThreadStart(playSnake));
        t.Start();
    }
    private int getDirFromOutput()
    {
        int direction = 0;
        double maxValue = double.MinValue;

        double[] outs = getOutputs();

        for (int i = 0; i < NEAT.outputNodesSize; i++)
        {
            if (outs[i] > maxValue)
            {
                maxValue = outs[i];
                direction = i - 1;
            }
        }
        return direction;
    }
    private double[] getOutputs()
    {
        double[] outputs = new double[NEAT.outputNodesSize];

        for (int i = 0; i < NEAT.outputNodesSize; i++) outputs[i] = genome.nodeGenes[i + NEAT.inputNodesSise].value;

        return outputs;
    }
    public double getFitness() => genome.fitness;
    // private void CalculateOutput()
    // {
    //     alreadyCalculated.Clear();

    //     for (int i = 0; i < outputNodesCount; i++)
    //     {
    //         if (!alreadyCalculated.Contains(genome.nodeGenes[i + inputNodesCount]))
    //         {
    //             CalcValForNeuron(genome.nodeGenes[i + inputNodesCount]);
    //         }
    //     }
    // }
    // private double CalcValForNeuron(NodeGene neuron)
    // {
    //     for (int i = 0; i < genome.ConnectionGenesCount(); i++)
    //     {
    //         if (genome[i].to == neuron)
    //         {
    //             if (alreadyCalculated.Contains(genome[i].from))
    //             {
    //                 neuron.value += genome[i].weight * genome[i].from.value;
    //             }
    //             else
    //             {
    //                 neuron.value += genome[i].weight * CalcValForNeuron(genome[i].from);
    //             }
    //         }
    //     }

    //     neuron.value = AIHelpFunctions.Tanh(neuron.value);

    //     alreadyCalculated.Add(neuron);
    //     return neuron.value;
    // }
}