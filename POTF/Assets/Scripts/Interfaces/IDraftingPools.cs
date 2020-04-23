using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraftingPools 
{
    IEnumerable<HostileDraftConfiguration> Config_Hostile_Pool { get; }

}
