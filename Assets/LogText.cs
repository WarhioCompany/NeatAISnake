﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour
{
    void FixedUpdate()
    {
        GetComponent<Text>().text = TestSystem.log;
    }
}
