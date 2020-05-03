using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillAction
{
    ActionTypes ActionType { get; }
    int BaseAPCost { get; set; }
    float BaseSuccessChance { get; }


}
