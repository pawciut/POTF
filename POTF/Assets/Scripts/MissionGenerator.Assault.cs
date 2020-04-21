using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class MissionGenerator
{
    public List<string> Config_Assault_Desc_Pool = new List<string>
    {
        "Some hooligans are attacking animals",
        "Teenagers are attacking animals"
    };

    public List<MissionDraftConfiguration> Config_Assault_Pool = new List<MissionDraftConfiguration>
    {
        new MissionDraftConfiguration()
        {
            MaxHostiles = 1,
            ForestDamage = 2,
        },new MissionDraftConfiguration()
        {
            MaxHostiles = 1,
            ForestDamage = 2,
        },new MissionDraftConfiguration()
        {
            MaxHostiles = 1,
            ForestDamage = 3,
        },
        new MissionDraftConfiguration()
        {
            MaxHostiles = 2,
            ForestDamage = 2,
            Duration = 4
        },
        new MissionDraftConfiguration()
        {
            MaxHostiles = 2,
            ForestDamage = 4,
            Duration = 5,
        },
        new MissionDraftConfiguration()
        {
            MaxHostiles = 3,
            ForestDamage = 5,
            Duration = 8,
        },
    };

}