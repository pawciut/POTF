using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public delegate void TurnChangedEventHandler(TurnPhase phase);
    public event TurnChangedEventHandler TurnChanged;

    public enum TurnPhase
    {
        Player,
        Enemy
    }

    private TurnPhase currentPhase;
    private int currentTurn;

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = TurnPhase.Player;
        currentTurn = 1;

        Debug.Log($"Current turn: {currentPhase.ToString()}");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EndTurn()
    {
        switch (currentPhase)
        {
            case TurnPhase.Player:
                currentPhase = TurnPhase.Enemy;
                break;
            case TurnPhase.Enemy:
                currentPhase = TurnPhase.Player;
                break;
            default:
                break;
        }

        Debug.Log($"Current turn: {currentPhase.ToString()}");

        this.currentTurn++;

        TurnChanged?.Invoke(currentPhase);
    }

    public bool IsPlayerTurn
    { 
        get 
        { 
            return this.currentPhase == TurnPhase.Player;
        } 
    }
    public bool IsEnemyTurn
    {
        get
        {
            return this.currentPhase == TurnPhase.Enemy;
        }
    }
}
