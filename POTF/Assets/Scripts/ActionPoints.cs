using UnityEngine;

public class ActionPoints : MonoBehaviour
{
    public delegate void ActionPointsChangedEventHandler(ActionPoints sender);
    public event ActionPointsChangedEventHandler ActionPointsChanged;

    public int Remaining => currentActionPoints;

    private int maxActionPoints;
    private int currentActionPoints;

    private void Start()
    {
        ResetActionPoints();
    }

    public void ResetActionPoints()
    {
        this.currentActionPoints = this.maxActionPoints;
        OnActionPointChanged();
    }

    public bool HasEnoughActionPoints(int pointCost)
    {
        return this.currentActionPoints >= pointCost;
    }

    public int AddActionPoints(int points)
    {
        this.currentActionPoints += points;
        OnActionPointChanged();

        return this.currentActionPoints;
    }

    public void SetMaxActionPoints(int points)
    {
        this.maxActionPoints = points;
        OnActionPointChanged();
    }

    private void OnActionPointChanged()
    {
        ActionPointsChanged?.Invoke(this);
    }

}
