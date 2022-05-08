using System.Collections.Generic;
using UnityEngine;
public class RandomSelector<T>
{
    private List<T> objects;
    private List<double> scores;
    private double total = 0;
    public void add(T obj, double score)
    {
        objects.Add(obj);
        scores.Add(score);
        total += score;
    }

    public T random()
    {
        double v = Random.Range(0, (float)total);

        double c = 0;
        for (int i = 0; i < objects.Count; i++)
        {
            c += scores[i];
            if (c > v)
            {
                return objects[i];
            }
        }
        return default(T);
    }
    public void reset()
    {
        objects.Clear();
        scores.Clear();
        total = 0;
    }
}