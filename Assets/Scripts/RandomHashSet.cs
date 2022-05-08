using System.Collections.Generic;
using UnityEngine;
public class RandomHashSet<T>
{
    private HashSet<T> set;
    private List<T> data;
    public RandomHashSet()
    {
        set = new HashSet<T>();
        data = new List<T>();
    }
    public bool Contains(T elem) => set.Contains(elem);
    public void Add(T elem)
    {
        if (!Contains(elem))
        {
            set.Add(elem);
            data.Add(elem);
        }
    }
    public T Get(int id) => data[id];
    public int Get(T elem) => data.IndexOf(elem);
    public void Clear()
    {
        set.Clear();
        data.Clear();
    }
    public int Size() => set.Count;
    public T Random()
    {
        return data[UnityEngine.Random.Range(0, Size())];
    }
    public void Remove(int id)
    {
        if (id < 0 || id >= Size())
        {
            throw new System.IndexOutOfRangeException();
        }
        set.Remove(data[id]);
        data.RemoveAt(id);
    }
    public T this[int key]
    {
        get => Get(key);
    }
    public List<T> getData() => data;
}