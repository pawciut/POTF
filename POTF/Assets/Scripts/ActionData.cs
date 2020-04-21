using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// bazowa klasa do akcji dla npc i gracza
/// </summary>
public abstract class ActionData
{
    public CharacterData Actor;
    public abstract ActionTypes ActionType { get; }
    public virtual int APCost { get; set; } = 0;
    public virtual float BaseSuccessChance { get; } = 0.25f;

    public virtual float ChanceOfSuccess(CharacterData character, ActionData reactionForThisAction = null)
    {
        Actor = character;
        return BaseSuccessChance;
    }
}

/// <summary>
/// akcja aktaku i jej parametry
/// </summary>
public class Action_Attack : ActionData
{
    public int BaseDamage { get; set; } = 1;

    public AttackTypes AttackType = AttackTypes.Melee;

    public override ActionTypes ActionType { get => ActionTypes.Attack; }

    public Action_Attack()
    {
        APCost = 1;
    }
    public Action_Attack(int damage = 1, int apCost = 1, AttackTypes attackType = AttackTypes.Melee)
    {
        this.BaseDamage = damage;
        this.APCost = apCost;
        this.AttackType = attackType;
    }

    public int GetDamage()
    {
        if (AttackType == AttackTypes.Melee)
            return BaseDamage + (int)(Actor.Power * Constants.Mod_MeleeDamageBonusPerPower);
        return BaseDamage;
    }

    public override float ChanceOfSuccess(CharacterData character, ActionData reactionForThisAction = null)
    {
        base.ChanceOfSuccess(character, reactionForThisAction);
        return BaseSuccessChance + character.Agility * Constants.Mod_HitChancePerAgility + character.Intelect * Constants.Mod_HitChancePerIntelect;
    }

    public override string ToString()
    {
        return $"Attack {GetDamage()} {AttackType}";
    }

}

/// <summary>
/// akcja uniku i jej parametry
/// </summary>
public class Action_Dodge : ActionData
{
    public override ActionTypes ActionType => ActionTypes.Dodge;

    public Action_Dodge()
    {
    }

    public override float ChanceOfSuccess(CharacterData character, ActionData reactionForThisAction = null)
    {
        base.ChanceOfSuccess(character, reactionForThisAction);

        var successChance = BaseSuccessChance + character.Agility * Constants.Mod_DodgeChancePerAgility + character.Intelect * Constants.Mod_DodgeChancePerIntelect;
        if (reactionForThisAction is Action_Attack && (reactionForThisAction as Action_Attack).AttackType == AttackTypes.Ranged)
        {

            successChance -= (reactionForThisAction as Action_Attack).Actor.Agility * Constants.Mod_ReduceDodgeChancePerAttackerAgilityWhenRangedAttack;
        }
        return successChance;
    }
}

/// <summary>
/// akcja naprawiania i jej parametry
/// </summary>
public class Action_Fix : ActionData
{
    public override ActionTypes ActionType => ActionTypes.Fix;

    public override float ChanceOfSuccess(CharacterData character, ActionData reactionForThisAction = null)
    {
        base.ChanceOfSuccess(character, reactionForThisAction);
        return BaseSuccessChance 
            //+ character.Agility * Constants.Mod_FixChancePerAgility 
            + character.Intelect * Constants.Mod_FixChancePerIntelect;
    }
}


public class Action_Scout : ActionData
{
    public override float BaseSuccessChance => 1f;
    public override ActionTypes ActionType => ActionTypes.Scout;
}


