using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IRandomGenerator
{
    /// <summary>
    /// random from min[inclusive], max[exclusive]
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    float Range(float min, float max);

    /// <summary>
    /// random from min[inclusive], max[exclusive]
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    int Range(int min, int max);
}