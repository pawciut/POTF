using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Hero character stats and abilities
/// </summary>
public class HeroData
{
    public int Power { get; set; }
    public int Agility { get; set; }
    public int Intelect { get; set; }
    public int CurrentHp { get; set; }
    public int MaxHp { get; set; }

    public int CurrentLevel { get; set; }
    public int CurrentExp { get; set; }

    public HeroData()
    {
        this.Power = 1;
        this.Agility = 1;
        this.Intelect = 0;
        this.CurrentHp = 3;
        this.MaxHp = 3;

        this.CurrentLevel = 1;
        this.CurrentExp = 0;
    }
}
