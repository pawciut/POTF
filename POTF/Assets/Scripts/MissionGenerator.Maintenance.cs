using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class MissionGenerator
{
    public List<string> Config_Maintenance_Desc_Pool = new List<string>
    {
        Constants.Mission_Maintenance_Desc_01,
        Constants.Mission_Maintenance_Desc_02
    };

    public List<MissionDraftConfiguration> Config_Maintenance_Pool = new List<MissionDraftConfiguration>
    {
        new MissionDraftConfiguration()
        {
            MaxHostiles = 0,
            ForestDamage = 1,
        },new MissionDraftConfiguration()
        {
            MaxHostiles = 0,
            ForestDamage = 1,
        },new MissionDraftConfiguration()
        {
            MaxHostiles = 0,
            ForestDamage = 2,
        },new MissionDraftConfiguration()
        {
            MaxHostiles = 0,
            ForestDamage = 2,
        },new MissionDraftConfiguration()
        {
            MaxHostiles = 0,
            ForestDamage = 3,
        },
        new MissionDraftConfiguration()
        {
            MaxHostiles = 1,
            ForestDamage = 1,
        },
        new MissionDraftConfiguration()
        {
            MaxHostiles = 1,
            ForestDamage = 2,
        },new MissionDraftConfiguration()
        {
            MaxHostiles = 3,
        },
        new MissionDraftConfiguration()
        {
            MaxHostiles = 2,
            ForestDamage = 2,
        },
        new MissionDraftConfiguration()
        {
            MaxHostiles = 2,
            ForestDamage = 3,
        },
        new MissionDraftConfiguration()
        {
            MaxHostiles = 3,
            ForestDamage = 3,
        },
    };

}