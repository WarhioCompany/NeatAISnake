using System.Collections.Generic;
using UnityEngine;

public static class AIHelpFunctions
{
    public static double Sigmoid(double x)
    {
        if (x < -45.0) return 0;
        else if (x > 45.0) return 1;
        else return 1 / (1 + System.Math.Exp(-x));
    }
    public static double Tanh(double x)
    {
        return System.Math.Tanh(x);
    }
}