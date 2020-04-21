using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class MissionGenerator
{
    public List<string> Config_Supply_Desc_Pool = new List<string>
    {
        "Gift from animal lovers",
    };

    public List<MissionDraftConfiguration> Config_Supply_Pool = new List<MissionDraftConfiguration>
    {
        new MissionDraftConfiguration()
        {
            MaxHostiles = 0,
            RegenForest = 2
        }
    };

}