using System.Collections.Generic;

public interface ICharacterData
{
    List<ActionData> Actions { get; set; }
    int Agility { get; set; }
    int CurrentActionPoints { get; set; }
    int CurrentExp { get; set; }
    int CurrentHp { get; set; }
    int CurrentLevel { get; set; }
    int Intelect { get; set; }
    int MaxActionPoints { get; }
    int MaxHp { get; set; }
    string Name { get; set; }
    int Power { get; set; }

    bool ApplyDamage(Action_Attack attack);
    void RefreshAP();
}