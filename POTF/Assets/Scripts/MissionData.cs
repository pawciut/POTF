using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MissionData
{
    public string Name;
    public int Id;
    public string Desc;
    public int Duration;
    public int ForestDamage;
    public int Exp;

    public List<HostileData> Hostiles;

    public MissionData(MissionDraftConfiguration missionConfiguration, string name, string desc, int id, List<HostileData> hostiles)
    {
        this.Name = name;
        this.Desc = desc;
        this.Id = id;
        this.Duration = missionConfiguration.Duration;
        this.ForestDamage = missionConfiguration.ForestDamage;
        this.Exp = missionConfiguration.BaseExp;
        this.Hostiles = hostiles;
    }
}