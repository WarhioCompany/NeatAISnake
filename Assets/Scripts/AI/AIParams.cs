using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIParams : MonoBehaviour
{
    public int populationSize;
    public float mutationChance;

    public float c1, c2, c3;

    public float compThr;

    private void Start()
    {
        // NEAT neat = new NEAT(populationSize, 25, 1);
        // neat.setGAMERULES(50, 50);
        // neat.StartEvolution();
    }
}
