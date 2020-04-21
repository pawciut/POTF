using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Character : MonoBehaviour
{
    internal bool isMoving;
    internal List<Vector2Int> currentPath;
    internal Vector2 currentDestination;
    internal Vector2Int gridPosition;
    internal ActionPoints actionPoints;

    [SerializeField]
    protected float speed = 10f;
    [SerializeField]
    protected Tilemap map;
    [SerializeField]
    protected int BaseMoveCost = 1;

    protected Vector2Int mapOrigin;
    protected CharacterData characterData;

    // Start is called before the first frame update
    public virtual void Start()
    {
        characterData = new CharacterData();
        mapOrigin = new Vector2Int((int)map.localBounds.min.x, (int)map.localBounds.min.y);
        gridPosition = GetGridPosition();
        actionPoints = GetComponent<ActionPoints>();
        actionPoints.ActionPointsChanged += ActionPoints_ActionPointsChanged;
        actionPoints.SetMaxActionPoints(characterData.MaxActionPoints);
        actionPoints.ResetActionPoints();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        MoveCharacter();
    }

    protected void ActionPoints_ActionPointsChanged(ActionPoints sender)
    {
        characterData.CurrentActionPoints = sender.Remaining;
    }

    protected virtual void MoveCharacter()
    {
        if (isMoving)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, currentDestination, speed * Time.deltaTime);

            float distance = Vector2.Distance(currentDestination, this.transform.position);

            if (distance <= 0f)
            {
                if (currentPath.Count > 0)
                {
                    currentDestination = currentPath.Pop();
                }
                else
                {
                    currentPath = null;
                    isMoving = false;
                    gridPosition = GetGridPosition();
                }
            }
        }
    }

    internal void BeginMoving()
    {
        if (currentPath != null && currentPath.Count > 0)
        {
            this.actionPoints.AddActionPoints(-GetPathCost(currentPath));
            this.currentDestination = this.currentPath.Pop();
            this.isMoving = true;
        }
    }

    protected Vector2Int GetGridPosition()
    {
        return
            new Vector2Int(
                (int)Math.Floor(this.transform.position.x) - mapOrigin.x,
                (int)Math.Floor(this.transform.position.y) - mapOrigin.y);
    }

    protected virtual int GetPathCost(List<Vector2Int> currentPath)
    {
        return (currentPath.Count - 1) * BaseMoveCost;
    }

    public Vector3Int TilePosition
    {
        get
        {
            return
                new Vector3Int(
                    (int)Math.Floor(this.transform.position.x),
                    (int)Math.Floor(this.transform.position.y),
                    (int)Math.Floor(this.transform.position.z));
        }
    }

    public bool CanMove(List<Vector2Int> path)
    {
        if (path != null)
        {
            return this.actionPoints.HasEnoughActionPoints(GetPathCost(path));
        }
        else
            return false;
    }

    public bool CanMove()
    {
        return CanMove(currentPath);
    }
}
