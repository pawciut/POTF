using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HostileDraftConfiguration : CharacterData
{
    public string Name = "Hostile";
    public int BaseExp = 5;
    public int MinPlayerLevel = 1;
    protected HostileDraftConfiguration()
    {

    }
    public HostileDraftConfiguration(string name, int baseExp, int power, int agility, int intelect, int maxHp, int minPlayerLevel, List<ActionData> actions)
    {
        this.Name = name;
        this.BaseExp = baseExp;
        this.Power = power;
        this.Agility = agility;
        this.Intelect = intelect;
        this.CurrentHp = maxHp;
        this.MaxHp = maxHp;
        this.MinPlayerLevel = minPlayerLevel;

        this.Actions = actions;
    }
}