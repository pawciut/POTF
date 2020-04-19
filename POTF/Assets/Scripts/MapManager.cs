using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask backgroundLayer;

    [SerializeField]
    private LayerMask characterLayer;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Tilemap overlayTilemap;

    [SerializeField]
    private Tile markTile;

    [SerializeField]
    private Tile walkableTile;

    [SerializeField]
    private Tile obstacleTile;

    [SerializeField]
    private List<PlayerCharacter> playerCharacters;

    private bool hidePath;
    private PlayerCharacter selectedCharacter;
    private Vector3Int? currentTilePosition = null;
    private Pathfinding pathfinding;

    private void Start()
    {
        pathfinding = new Pathfinding(tilemap.size.x, tilemap.size.y, 1f, tilemap.localBounds.min, false);
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        bool characterSelected = false;

        if (Input.GetMouseButtonDown(0))
        {
            characterSelected = SelectCharacter(mouseWorldPosition);
        }

        if (!characterSelected && selectedCharacter != null && !selectedCharacter.isMoving)
        {
            FindMarkPath(mouseWorldPosition);

            if (Input.GetMouseButtonDown(0))
            {
                selectedCharacter.BeginMoving();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (selectedCharacter == null || !selectedCharacter.isMoving)
            {
                pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
                pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);

                var walkable = pathfinding.GetNode(x, y).isWalkable;

                Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPosition);
                tilemap.SetTile(tilePosition, walkable ? walkableTile : obstacleTile);

                if (selectedCharacter != null)
                    selectedCharacter.currentPath = null;
            }
        }

        if (selectedCharacter && !hidePath)
            DrawPath(selectedCharacter.currentPath, selectedCharacter.isMoving ? Color.red : Color.blue);
    }

    private bool SelectCharacter(Vector3 mouseWorldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, characterLayer);

        if (hit.collider != null)
        {
            var character = hit.collider.GetComponent<PlayerCharacter>();
            if (character != null && character != selectedCharacter)
            {
                if (selectedCharacter != null)
                    selectedCharacter.isSelected = false;
                character.isSelected = true;
                selectedCharacter = character;

                return true;
            }
        }

        return false;
    }

    private void DrawPath(List<Vector2Int> currentPath, Color pathColor)
    {
        if (currentPath != null)
        {
            for (int i = 0; i < selectedCharacter.currentPath.Count - 1; i++)
            {
                Debug.DrawLine(
                    new Vector3(
                        currentPath[i].x,
                        currentPath[i].y) + 0.5f * Vector3.one,
                    new Vector3(
                        currentPath[i + 1].x,
                        currentPath[i + 1].y) + 0.5f * Vector3.one,
                    pathColor);
            }
        }
    }

    private List<Vector2Int> GetTilePath(List<PathNode> path)
    {
        if (path == null)
            return null;

        var tilePath = new List<Vector2Int>();
        var origin = new Vector2Int((int)tilemap.localBounds.min.x, (int)tilemap.localBounds.min.y);

        foreach (var item in path)
        {
            tilePath.Add(new Vector2Int(item.x, item.y) + origin);
        }

        return tilePath;
    }

    private void FindPath()
    {
        pathfinding.GetGrid().GetXY(cam.ScreenToWorldPoint(Input.mousePosition), out int x, out int y);
        List<PathNode> path = pathfinding.FindPath(selectedCharacter.gridPosition.x, selectedCharacter.gridPosition.y, x, y);
        selectedCharacter.currentPath = GetTilePath(path);
    }

    private void FindMarkPath(Vector3 mouseWorldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, backgroundLayer);

        if (hit.collider != null)
        {
            Vector3Int tilePosition = overlayTilemap.WorldToCell(mouseWorldPos);
            if (tilePosition != currentTilePosition)
            {
                FindPath();

                if (currentTilePosition.HasValue)
                    overlayTilemap.SetTile(currentTilePosition.Value, null);

                currentTilePosition = tilePosition;

                foreach (var character in playerCharacters)
                {
                    if (currentTilePosition == character.TilePosition)
                    {
                        hidePath = true;
                        return;
                    }

                    hidePath = false;
                }

                overlayTilemap.SetTile(currentTilePosition.Value, markTile);
            }
        }
    }
}
