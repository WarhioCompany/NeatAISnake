using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
    public static string log;

    int inputSize;
    int outputSize;
    NEAT neat;
    FieldManager fieldManager;
    // Start is called before the first frame update
    void Start()
    {
        inputSize = 26;
        outputSize = 3;

        neat = new NEAT(1, inputSize, outputSize, 500);
        neat.setGAMERULES(50, 5);
        neat.setCHANCES(0.1, 0.1, 0.3, 0.1, 0.1);
        neat.StartEvolution();
        // // ShowGenes viewer = FindObjectOfType<ShowGenes>();
        // // for (int i = 0; i < 3; i++)
        // // {
        // //     Genome first = new Genome(inputSize, outputSize);
        // //     first.AddConnectionMutation();
        // //     viewer.Show(first.connectionGenes.getData());
        // // }
        fieldManager = GameObject.Find("Field").GetComponent<FieldManager>();
    }
    IEnumerator idk()
    {
        yield return new WaitUntil(() => NEAT.recordings != null);
        yield return fieldManager.Replay(NEAT.recordings);
    }
    bool a = true;
    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            neat.NextGeneration();
            StartCoroutine("idk");
            a = false;
        }
    }
}
