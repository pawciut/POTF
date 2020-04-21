public class PlayerCharacter : Character, ISelectable
{
    public bool Selected => isSelected;
    internal bool isSelected;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
}
