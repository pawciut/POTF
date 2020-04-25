using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UnityRandomGenerator : IRandomGenerator
{
    public float Range(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    public int Range(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
