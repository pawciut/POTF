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
    private Tilemap background;

    [SerializeField]
    private Tilemap obstacles;

    [SerializeField]
    private Tilemap overlay;

    [SerializeField]
    private Tile markTile;

    private bool hidePath;
    private Vector3Int? currentTilePosition = null;
    private Pathfinding pathfinding;
    private CharacterManager characterManager;
    private TurnManager turnManager;

    private PlayerCharacter selectedCharacter
    {
        get
        {
            return characterManager.activePlayerCharacter;
        }
        set
        {
            characterManager.activePlayerCharacter = value;
        }
    }

    private void Start()
    {
        characterManager = GetComponent<CharacterManager>();
        turnManager = GetComponent<TurnManager>();
        turnManager.TurnChanged += TurnManager_TurnChanged;
        pathfinding = new Pathfinding(background.size.x, background.size.y, 1f, background.localBounds.min, false);
        InitializeObstacles();
    }

    private void TurnManager_TurnChanged(TurnManager.TurnPhase phase)
    {
        if(phase == TurnManager.TurnPhase.Player)
        {
            if (selectedCharacter != null)
                selectedCharacter.isSelected = false;
            selectedCharacter = null;

            foreach (var playerChar in characterManager.PlayerCharacters)
                playerChar.actionPoints.ResetActionPoints();
        }
    }

    private void Update()
    {
        if (turnManager.IsPlayerTurn)
        {

            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            var newTilePosition = GetTileFromWorldPosition(mouseWorldPosition);
            var characterSelected = false;

            if (Input.GetMouseButtonDown(0))
            {
                characterSelected = SelectCharacter(mouseWorldPosition);
                if (characterSelected)
                    Debug.Log($"Action Points: {selectedCharacter.actionPoints.Remaining}");
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (selectedCharacter != null)
                {
                    selectedCharacter.isSelected = false;
                    selectedCharacter = null;
                    ClearMarker();
                }
            }

            if (!characterSelected && selectedCharacter != null && !selectedCharacter.isMoving)
            {
                if (newTilePosition.HasValue && newTilePosition != currentTilePosition)
                {
                    selectedCharacter.currentPath =
                        pathfinding.FindTilePath(selectedCharacter.gridPosition.x, selectedCharacter.gridPosition.y, mouseWorldPosition);

                    ClearMarker();

                    currentTilePosition = newTilePosition;

                    if (IsAnyCharacterInPosition(currentTilePosition))
                    {
                        hidePath = true;
                    }
                    else
                    {
                        hidePath = false;
                        SetMarker(currentTilePosition.Value);
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (selectedCharacter.CanMove())
                        selectedCharacter.BeginMoving();
                }
            }

            if (selectedCharacter && !hidePath)
            {
                var pathColor = Color.blue;
                if (!selectedCharacter.CanMove())
                    pathColor = Color.red;
                DrawPath(selectedCharacter.currentPath, pathColor);
            }
        }
    }

    private void InitializeObstacles()
    {
        foreach (var pos in obstacles.cellBounds.allPositionsWithin)
        {
            Vector3Int tilePosition = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 worldPosition = obstacles.CellToWorld(tilePosition);

            if (obstacles.HasTile(tilePosition))
            {
                pathfinding.GetGrid().GetXY(worldPosition, out int x, out int y);
                pathfinding.GetNode(x, y).SetIsWalkable(false);
            }
        }
    }

    private void SetMarker(Vector3Int tilePosition)
    {
        overlay.SetTile(tilePosition, markTile);
    }

    private bool IsAnyCharacterInPosition(Vector3Int? tilePosition)
    {
        if (tilePosition.HasValue)
        {
            foreach (var character in characterManager.PlayerCharacters)
            {
                if (currentTilePosition == character.TilePosition)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void ClearMarker()
    {
        if (currentTilePosition.HasValue)
            overlay.SetTile(currentTilePosition.Value, null);
    }

    private Vector3Int? GetTileFromWorldPosition(Vector3 worldPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, backgroundLayer);

        if (hit.collider != null)
        {
            Vector3Int tilePosition = background.WorldToCell(worldPosition);
            return tilePosition;
        }

        return null;
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
                {
                    selectedCharacter.isSelected = false;
                    selectedCharacter.currentPath = null;
                }

                character.isSelected = true;
                selectedCharacter = character;
                selectedCharacter.currentPath = null;

                return true;
            }
        }

        return false;
    }

    private void DrawPath(List<Vector2Int> currentPath, Color pathColor)
    {
        if (currentPath != null)
        {
            for (int i = 0; i < currentPath.Count - 1; i++)
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

    //private List<Vector2Int> GetTilePath(List<PathNode> path)
    //{
    //    if (path == null)
    //        return null;

    //    var tilePath = new List<Vector2Int>();
    //    var origin = new Vector2Int((int)background.localBounds.min.x, (int)background.localBounds.min.y);

    //    foreach (var item in path)
    //    {
    //        tilePath.Add(new Vector2Int(item.x, item.y) + origin);
    //    }

    //    return tilePath;
    //}

    //private List<Vector2Int> FindTilePath(int xFrom, int yFrom, int xTo, int yTo)
    //{
    //    List<PathNode> path = pathfinding.FindPath(xFrom, yFrom, xTo, yTo);
    //    return GetTilePath(path);
    //}
}
