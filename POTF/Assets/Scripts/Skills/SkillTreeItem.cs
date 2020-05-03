using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Tree", menuName = "ScriptableObjects/Skills/SkillTreeItem")]
public class SkillTreeItem : ScriptableObject
{
    public string Name;
    public string Description;
    public SkillTypes Type;
    public SkillUsageTypes Usage;
    public ScriptableObject Action;

}
