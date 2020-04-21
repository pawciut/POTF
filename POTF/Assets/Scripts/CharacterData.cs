using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// character stats and abilities for player and npc
/// </summary>
public class CharacterData
{
    public string Name { get; set; }
    public int Power { get; set; }
    public int Agility { get; set; }
    public int Intelect { get; set; }
    public int CurrentHp { get; set; }
    public int MaxHp { get; set; }

    public int CurrentLevel { get; set; }
    public int CurrentExp { get; set; }

    public int CurrentActionPoints { get; set; }
    public int MaxActionPoints { get { return GetMaxActionPoints(); } }

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

    protected virtual int GetMaxActionPoints()
    {
        return 3 + (Agility / 3);
    }

    public void RefreshAP()
    {
        CurrentActionPoints = GetMaxActionPoints();
    }

    public bool ApplyDamage(Action_Attack attack)
    {
        var dodge = Actions.FirstOrDefault(a => a.ActionType == ActionTypes.Dodge);
        if (dodge != null)
        {
            var dodgeRoll = UnityEngine.Random.Range(0f, 1f);
            var dodgeChance = dodge.ChanceOfSuccess(this, attack);
            if (dodgeRoll <= dodgeChance)
            {
                Debug.Log($"{Name} dogged {attack.ToString()} from {attack.Actor?.Name}");
                //dodged
                return false;
            }
        }

        CurrentHp -= attack.GetDamage();
        return true;

    }
}
