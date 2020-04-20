using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class MissionGenerator
{
    /// <summary>
    /// configurations for random hostile npc pool
    /// </summary>
    public List<HostileDraftConfiguration> Config_Hostile_Pool = new List<HostileDraftConfiguration>
    {
        new HostileDraftConfiguration("Dog (Melee)",5,1,1,0,1,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog (Melee)",5,1,1,0,1,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog (Melee)",5,1,1,0,1,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog (Melee)",5,1,1,0,1,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog (Melee)",5,1,1,0,1,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog (Melee)",5,1,1,0,1,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog (Melee)",5,1,1,0,1,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog (Melee)",5,1,1,0,1,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog Veteran (Melee)",8,1,1,0,2,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog Veteran (Melee)",8,1,1,0,2,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog Veteran (Melee)",8,1,1,0,2,1, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog Veteran (Melee)",8,1,1,0,2,2, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Dog Veteran (Melee)",8,1,1,0,2,2, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Tourist (Melee)",10,1,1,0,1,2, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Tourist (Melee)",10,1,1,0,1,2, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Tourist (Melee)",10,1,1,0,1,3, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Tourist Veteran (Melee)",15,2,2,1,2,3, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Tourist Veteran (Melee)",15,2,2,1,2,4, new List<ActionData>{
            new Action_Attack()
        }),
        new HostileDraftConfiguration("Hunter (Melee)",20,2,2,1,2,5, new List<ActionData>{
            new Action_Attack(2)
        }),
        new HostileDraftConfiguration("Hunter (Range)",25,2,2,1,2,5, new List<ActionData>{
            new Action_Attack(2,1, AttackTypes.Ranged)
        }),
        new HostileDraftConfiguration("Hunter Veteran (Melee)",35,2,2,1,4,6, new List<ActionData>{
            new Action_Attack(2)
        }),
        new HostileDraftConfiguration("Hunter Veteran (Range)",45,2,2,1,4,7, new List<ActionData>{
            new Action_Attack(2,1, AttackTypes.Ranged)
        })
    };
}