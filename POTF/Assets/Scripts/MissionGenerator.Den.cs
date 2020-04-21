using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class MissionGenerator
{
    public List<string> Config_Den_Desc_Pool = new List<string>
    {
        Constants.Mission_Den_Desc_01,
    };

    public List<MissionDraftConfiguration> Config_Den_Pool = new List<MissionDraftConfiguration>
    {
        new MissionDraftConfiguration()
        {
            MaxHostiles = 0,
            RegenPlayer = 1,
            Duration = 3,
        }
    };

}