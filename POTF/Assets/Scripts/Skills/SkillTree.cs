using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Tree", menuName = "ScriptableObjects/Skills/SkillTree")]
public class SkillTree : ScriptableObject
{
    public string Name;

    public SkillTreeItem Level1_1;
    public SkillTreeItem Level2_1;
    public SkillTreeItem Level2_2;
    public SkillTreeItem Level3_1;
    public SkillTreeItem Level3_2;
    public SkillTreeItem Level4_1;

    
}
