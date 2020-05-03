using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum TargetTypes
{
    Enemy,
    Friendly,
    Trap,
    /// <summary>
    /// That can be fixed
    /// </summary>
    Structure,
    Obstacle_Destructable,
    Obstacle_Indestructable,
    /// <summary>
    /// object that can be gathered
    /// </summary>
    Resource,
    Item,
}

