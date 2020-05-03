using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Soldier", menuName ="ScriptableObjects/AnimalSoldier")]
public class AnimalSoldier : ScriptableObject
{
    public string Name;
    public string Description;
    public AnimalTypes Type;
    public AnimalSizeTypes Size;

    public int Power;
    public int Agility;
    public int Intelect;
    public int Tenacity;

    public int BaseHp;
    public float BonusHpPerTenacity;

    public int Level;
    public SkillTree Skill_1;
    public SkillTree Skill_2;
    public SkillTree Skill_3;
    public SkillTree Skill_4;
    public SkillTree Skill_5;

    public AnimalMaintenance[] DietOptions;

    public int Cost;



}
