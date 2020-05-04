using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SoldierData
{
    public string Name { get; set; }
    public int Power { get; set; }
    /// <summary>
    /// Total
    /// </summary>
    public int PowerProgress;
    public int Agility { get; set; }

    /// <summary>
    /// Total
    /// </summary>
    public int AgilityProgress;
    public int Intelect { get; set; }
    /// <summary>
    /// Total
    /// </summary>
    public int IntellectProgress;
    public int Tenacity { get; set; }
    /// <summary>
    /// Total
    /// </summary>
    public int TenacityProgress;
    public int CurrentHp { get; set; }
    public int MaxHp { get; set; }

    public int CurrentLevel { get; set; }
    public int CurrentExp { get; set; }

    public int CurrentActionPoints { get; set; }
    public int MaxActionPoints { get { return GetMaxActionPoints(); } }

    public float GetAttributeProgressPercent(AttributeTypes attribute)
    {
        int attributeCurrentLevel;
        int attributeProgressTotal;

        switch(attribute)
        {
            case AttributeTypes.Power:
                attributeCurrentLevel = Power;
                attributeProgressTotal = PowerProgress;
                break;
            case AttributeTypes.Agility:
                attributeCurrentLevel = Agility;
                attributeProgressTotal = AgilityProgress;
                break;
            case AttributeTypes.Intellect:
                attributeCurrentLevel = Intelect;
                attributeProgressTotal = IntellectProgress;
                break;
            case AttributeTypes.Tenacity:
                attributeCurrentLevel = Tenacity;
                attributeProgressTotal = TenacityProgress;
                break;
            default:
                throw new ArgumentException("Unknown attribute");
        }

        int leftBracket = Constants.Attribute_Levels[attributeCurrentLevel];
        int rightBracket = Constants.Attribute_Levels[attributeCurrentLevel + 1];
        int max = rightBracket - leftBracket;
        int current = attributeProgressTotal - leftBracket;
        return (float)current / (float)max;

    }

    public float GetHpPercent()
    {
        return (float)CurrentHp / (float)MaxHp;
    }

    public float GetLevelPercent()
    {
        return (float)CurrentExp / (float)Constants.Exp_Levels_Caps[CurrentLevel];
    }

    protected virtual int GetMaxActionPoints()
    {
        return 3 + (Agility / 3);
    }
}