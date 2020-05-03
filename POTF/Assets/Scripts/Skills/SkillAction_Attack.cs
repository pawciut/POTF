using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Tree", menuName = "ScriptableObjects/Skills/SkillAction_Attack")]
public class SkillAction_Attack : ScriptableObject, ISkillAction
{
    public ActionTypes actionType = ActionTypes.Attack;
    public int baseAPCost = 2;
    public float baseSuccessChance = 0.25f;
    public int baseDamage= 1;
    public AttackTypes attackType = AttackTypes.Melee;

    public ActionTypes ActionType { get { return this.actionType; } }
    public int BaseAPCost { get { return this.baseAPCost; } set { this.baseAPCost = value; } }
    public float BaseSuccessChance { get { return this.baseSuccessChance; } } 

    public int BaseDamage { get { return this.baseDamage; } set { this.baseDamage = value; } } 
    public AttackTypes AttackType { get { return this.attackType; } }
}
