using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// data for event - mission occuring in map scene -> mission scene?
/// </summary>
public class MissionData
{
    public string Name;
    public int Id;
    public string Desc;
    public int Duration;
    public int ForestDamage;
    public int Exp;
    public int RegenPlayer;
    public int RegenForest;
    public MissionTypes MissionType;


    public List<HostileData> Hostiles;

    public MissionData(MissionTypes missionType, MissionDraftConfiguration missionConfiguration, string name, string desc, int id, List<HostileData> hostiles)
    {
        this.MissionType = missionType;
        this.Name = name;
        this.Desc = desc;
        this.Id = id;
        this.Duration = missionConfiguration.Duration;
        this.ForestDamage = missionConfiguration.ForestDamage;
        this.Exp = missionConfiguration.BaseExp;
        this.Hostiles = hostiles;
        this.RegenPlayer = missionConfiguration.RegenPlayer;
        this.RegenForest = missionConfiguration.RegenForest;
    }
}