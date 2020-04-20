using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// character stats and abilities for player and npc
/// </summary>
public class CharacterData
{
    public int Power { get; set; }
    public int Agility { get; set; }
    public int Intelect { get; set; }
    public int CurrentHp { get; set; }
    public int MaxHp { get; set; }

    public int CurrentLevel { get; set; }
    public int CurrentExp { get; set; }

    public int CurrentActionPoints { get; set; }
    public int MaxActionPoints { get { return GetActionPoints(); } }

    public List<ActionData> Actions { get; set; }

    public CharacterData()
    {
        Actions = new List<ActionData>();

        this.Power = 1;
        this.Agility = 1;
        this.Intelect = 0;
        this.CurrentHp = 3;
        this.MaxHp = 3;

        this.CurrentLevel = 1;
        this.CurrentExp = 0;
    }

    protected virtual int GetActionPoints()
    {
        return 3 + (Agility / 3);
    }
}
