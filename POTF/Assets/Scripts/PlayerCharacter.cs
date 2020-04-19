using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCharacter : MonoBehaviour, ISelectable
{
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

    public bool Selected => isSelected;

    internal bool isMoving;
    internal bool isSelected;
    internal List<Vector2Int> currentPath;
    internal Vector2 currentDestination;
    internal Vector2Int gridPosition;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private Tilemap map;

    private Vector2Int mapOrigin;

    // Start is called before the first frame update
    private void Start()
    {
        mapOrigin = new Vector2Int((int)map.localBounds.min.x, (int)map.localBounds.min.y);
        gridPosition = GetPlayerGridPosition();
    }

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (isMoving)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, currentDestination, speed * Time.deltaTime);

            float distance = Vector2.Distance(currentDestination, this.transform.position);

            if (distance <= 0f)
            {
                if (currentPath.Count > 0)
                {
                    currentDestination = currentPath[0];
                    currentPath.RemoveAt(0);
                }
                else
                {
                    currentPath = null;
                    isMoving = false;
                    gridPosition = GetPlayerGridPosition();
                }
            }
        }
    }

    internal void BeginMoving()
    {
        if (currentPath != null && currentPath.Count > 0)
        {
            this.currentDestination = this.currentPath.Pop();
            this.isMoving = true;
        }
    }

    private Vector2Int GetPlayerGridPosition()
    {
        return
            new Vector2Int(
                (int)Math.Floor(this.transform.position.x) - mapOrigin.x,
                (int)Math.Floor(this.transform.position.y) - mapOrigin.y);
    }
}
