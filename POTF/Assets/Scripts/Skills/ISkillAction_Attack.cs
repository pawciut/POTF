using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillAction_Attack : ISkillAction
{
    int BaseDamage { get; set; }
    AttackTypes AttackType { get; }
}
