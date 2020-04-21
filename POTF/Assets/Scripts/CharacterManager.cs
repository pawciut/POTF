using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public PlayerCharacter[] PlayerCharacters => playerCharacters;
    public EnemyCharacter[] EnemyCharacters => enemyCharacters;

    public PlayerCharacter activePlayerCharacter;
    public EnemyCharacter activeEnemyCharacter;

    private PlayerCharacter[] playerCharacters;
    private EnemyCharacter[] enemyCharacters;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacters = FindObjectsOfType<PlayerCharacter>();
        enemyCharacters = FindObjectsOfType<EnemyCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
