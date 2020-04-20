using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HostileData : HostileDraftConfiguration
{
    public HostileData(HostileDraftConfiguration configuration)
    {
        this.Power = configuration.Power;
        this.Agility = configuration.Agility;
        this.Intelect = configuration.Intelect;
        this.CurrentHp = configuration.CurrentHp;
        this.MaxHp = configuration.MaxHp;

        this.CurrentLevel = configuration.CurrentLevel;
        this.CurrentExp = configuration.CurrentExp;

        this.CurrentActionPoints = configuration.Power;

        this.Actions = configuration.Actions;

        this.Name = configuration.Name;
        this.BaseExp = configuration.BaseExp;
        this.MinPlayerLevel = configuration.MinPlayerLevel;
}
}