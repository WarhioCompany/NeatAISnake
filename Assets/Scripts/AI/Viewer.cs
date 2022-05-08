using System.Collections.Generic;
using UnityEngine;

public static class Viewer
{
    public static List<Dictionary<Vector2Int, int>> GetRecordingOfTheMostCapable(List<Tester> testers)
    {
        double maxFit = double.MinValue;

        Tester best = testers[0];

        foreach (Tester tester in testers)
        {
            if (maxFit < tester.getFitness())
            {
                maxFit = tester.getFitness();
                best = tester;
            }
        }
        return best.recordings;
    }
}
