using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlay : MonoBehaviour
{
    public float delay;
    private float _d;
    Snake snake;
    // Start is called before the first frame update
    void Start()
    {
        snake = new Snake(50);
    }

    // Update is called once per frame
    int rot = 0;
    void Update()
    {
        _d -= Time.deltaTime;
        // if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        // {
        //     rot = (int)Input.GetAxisRaw("Horizontal");//(int)(Input.GetAxisRaw("Horizontal") != 0 ? Input.GetAxisRaw("Horizontal") * 2 : Input.GetAxisRaw("Vertical"));
        // }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rot = -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rot = 1;
        }
        if (_d < 0)
        {
            _d = delay;
            snake.Move(rot);
            rot = 0;

            StartCoroutine(GetComponent<FieldManager>().Redraw(snake.field));
        }
    }
}
