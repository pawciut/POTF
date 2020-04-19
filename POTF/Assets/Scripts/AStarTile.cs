using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New AStarTile", menuName = "Tiles/AStarTile")]
public class AStarTile : Tile
{
    public bool Walkable;
}
