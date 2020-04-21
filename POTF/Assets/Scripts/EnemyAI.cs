using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private CharacterManager characterManager;
    private TurnManager turnManager;
    private List<EnemyCharacter> enemiesToMove;

    // Start is called before the first frame update
    void Start()
    {
        characterManager = GetComponent<CharacterManager>();
        turnManager = GetComponent<TurnManager>();
        turnManager.TurnChanged += TurnManager_TurnChanged;
    }

    private void TurnManager_TurnChanged(TurnManager.TurnPhase phase)
    {
        if(phase == TurnManager.TurnPhase.Enemy)
        {
            characterManager.activeEnemyCharacter = null;
            enemiesToMove = new List<EnemyCharacter>(characterManager.EnemyCharacters);

            foreach (var enemyChar in characterManager.EnemyCharacters)
                enemyChar.actionPoints.ResetActionPoints();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(turnManager.IsEnemyTurn)
        {
            if(characterManager.activeEnemyCharacter == null && enemiesToMove.Count == 0)
            {
                turnManager.EndTurn();
                return;
            }
            if(characterManager.activeEnemyCharacter != null && !characterManager.activeEnemyCharacter.isMoving)
            {
                characterManager.activeEnemyCharacter = null;
            }
            else if (characterManager.activeEnemyCharacter == null && enemiesToMove.Count > 0)
            {
                characterManager.activeEnemyCharacter = enemiesToMove.Pop();
                MakeAIMove();
            }
        }
    }

    private void MakeAIMove()
    {
        //TODO
        Debug.Log("AI Moving...");
        if (characterManager.PlayerCharacters.Length > 0)
        {
            var chosenPlayer = characterManager.PlayerCharacters[0];
            var path = Pathfinding.Instance.FindTilePath(characterManager.activeEnemyCharacter.gridPosition.x, characterManager.activeEnemyCharacter.gridPosition.y, chosenPlayer.transform.position);
            if(characterManager.activeEnemyCharacter.CanMove(path))
            {
                characterManager.activeEnemyCharacter.currentPath = path;
                characterManager.activeEnemyCharacter.BeginMoving();
            }
            else
            {
                var truncatedPath = TruncatePath(path);
                characterManager.activeEnemyCharacter.currentPath = truncatedPath;
                characterManager.activeEnemyCharacter.BeginMoving();
            }
        }
    }

    private List<Vector2Int> TruncatePath(List<Vector2Int> path)
    {
        var truncatedPath = new List<Vector2Int>();
        truncatedPath.Add(path.Pop());
        while(characterManager.activeEnemyCharacter.CanMove(truncatedPath))
        {
            truncatedPath.Add(path.Pop());
        }

        return truncatedPath.GetRange(0, truncatedPath.Count - 1);
    }
}
