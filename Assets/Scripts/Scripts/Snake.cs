using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake
{
    public bool isDead;
    //formula: 10 * (snake.Count + 1)
    public int movesLeft;
    //-2 - border
    //0 - nothing
    //-1 - snake 
    //1 - food
    public Dictionary<Vector2Int, int> field;
    private List<Vector2Int> snake;
    private int SIZE;
    private Vector2Int dir;
    private Vector2Int food;
    public double angle;
    public Snake(int fieldSize)
    {
        SIZE = fieldSize;
        field = new Dictionary<Vector2Int, int>();
        snake = new List<Vector2Int>();
        dir = new Vector2Int(1, 0);
        isDead = false;
        InitField();

        snake.Add(new Vector2Int((int)Mathf.Floor(SIZE / 2), (int)Mathf.Floor(SIZE / 2)));
        field[snake[0]] = -1;

        SpawnFood();

        calcMovesLeft();
    }
    public List<double> GetInfo(int size)
    {
        if (size % 2 == 0) throw new System.Exception("THE SIZE MUST BE ODD NUMBER");

        Debug.Log($"Get info {field.Count}");
        NEAT.recordings = new List<Dictionary<Vector2Int, int>> { field };
        //field size * size
        //and angle 
        List<double> info = new List<double>();

        Debug.Log($"SNAKE HEAD POS: {snake[0]}");

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector2Int pos = snake[0] + new Vector2Int(j - (size - 1) / 2, i - (size - 1) / 2);

                if (isInBorders(pos))
                {
                    Debug.Log($"Adding {field[pos]}");
                    info.Add(field[pos]);
                }
                else
                {
                    Debug.Log("Adding -2");
                    info.Add(-2);
                }
            }
        }
        info.Add(angle);

        return info;
    }
    public Dictionary<Vector2Int, int> GetField() => field;
    public int GetScore()
    {
        return snake.Count;
    }
    ///<summary>Relative | -1 left | 0 forward | 1 right ||| Non-relative | -2 left | -1 down | 1 up | 2 right </summary>
    public void Move(int rotation)
    {
        if (isDead) return;

        Debug.Log($"Moving {rotation}");

        Vector2Int head = ChangeDirection(rotation);

        ChangePosition(head);

        int add = dir.y * -90 + (dir.x == -1 ? -180 : 0);

        angle = Mathf.Atan2(food.y - head.y, food.x - head.x) * Mathf.Rad2Deg + add;

        TestSystem.log = angle.ToString();
    }
    private Vector2Int ChangeDirection(int rot)
    {
        Vector2Int bufHead = snake[0];
        //relative
        if (rot != 0)
        {
            if (dir.y != 0) dir = new Vector2Int(-rot * dir.y, 0);
            else dir = new Vector2Int(0, rot * dir.x);
        }

        bufHead += dir;

        //Non-relative

        // if (rot % 2 != 0) bufHead += new Vector2Int(0, -rot);
        // else bufHead += new Vector2Int(rot / 2, 0);

        return bufHead;
    }
    private void ChangePosition(Vector2Int bufHead)
    {
        if (isValidPosition(bufHead) && movesLeft <= 0)
        {
            snake.Insert(0, bufHead);

            if (field[snake[0]] != 1)
            {
                field[snake[snake.Count - 1]] = 0;
                snake.RemoveAt(snake.Count - 1);
            }
            else
            {
                SpawnFood();
            }

            field[snake[0]] = -1;
        }
        else
        {
            //DEAD
            Debug.Log($"DEAD: {GetScore()}");
            isDead = true;
        }
    }
    private void InitField()
    {
        for (int i = 0; i < SIZE; i++) for (int j = 0; j < SIZE; j++) field.Add(new Vector2Int(j, i), 0);
    }
    private void SpawnFood()
    {
        System.Random random = new System.Random();
        do
        {
            food = new Vector2Int(random.Next(0, SIZE), random.Next(0, SIZE));
        }
        while (field[food] == -1);
        field[food] = 1;
    }
    private bool isValidPosition(Vector2Int pos)
    {
        if (!isInBorders(pos)) return false;
        else if (field[pos] == -1) return false;

        return true;
    }
    private bool isInBorders(Vector2Int pos)
    {
        if (pos.x * pos.y < 0 || pos.x >= SIZE || pos.y >= SIZE)
        {
            return false;
        }
        return true;
    }
    private int calcMovesLeft() => 10 * (snake.Count + 1);

}