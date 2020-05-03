using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Maintenance", menuName ="ScriptableObjects/AnimalMaintenance")]
public class AnimalMaintenance : ScriptableObject
{
    public string Name;
    public ResourceTypes Type;
    public int ValuePerWeek;
}
