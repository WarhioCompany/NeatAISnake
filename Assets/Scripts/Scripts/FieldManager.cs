using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public int size;
    public GameObject cellPrefab;
    //private List<List<GameObject>> field;
    private Dictionary<Vector2Int, GameObject> field;
    // Start is called before the first frame update
    void Awake()
    {
        field = new Dictionary<Vector2Int, GameObject>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                field.Add(new Vector2Int(j, i), Instantiate(cellPrefab, new Vector3(j * 0.2f, -i * 0.2f) + transform.position, Quaternion.identity, transform));
            }
        }
    }
    public IEnumerator Redraw(Dictionary<Vector2Int, int> field)
    {

        foreach (Vector2Int pos in field.Keys)
        {
            if (!field.ContainsKey(pos))
            {
                this.field[pos].GetComponent<SpriteRenderer>().color = Color.gray;
            }
            switch (field[pos])
            {
                case 0:
                    ChangeColor(pos, Color.white);
                    break;
                case -1:
                    ChangeColor(pos, Color.black);
                    break;
                case 1:
                    ChangeColor(pos, Color.red);
                    break;
            }
        }
        yield return null;
    }
    private void ChangeColor(Vector2Int pos, Color color)
    {
        field[pos].GetComponent<SpriteRenderer>().color = color;
    }
    public IEnumerator Replay(List<Dictionary<Vector2Int, int>> recordings)
    {
        Debug.Log(recordings.Count);
        foreach (Dictionary<Vector2Int, int> record in recordings)
        {
            yield return Redraw(record);
            yield return new WaitForSeconds(.5f);
        }
    }
}
