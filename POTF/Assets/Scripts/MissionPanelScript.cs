using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// script used for presenting information about mission on UI in mission panel
/// </summary>
public class MissionPanelScript : MonoBehaviour
{
    public Button AutoButton;
    public Button GoButton;
    public Text MissionNameText;
    public Text MissionIdText;
    public Text MissionDurationText;
    public Text MissionScoutInfoText;
    public Text MissionDescText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(MissionData missionData, CharacterData playerData)
    {
        //Tylko dla maintenance ma byc dostepny?
        GoButton.interactable = false;
        MissionNameText.text = missionData.Name;
        MissionIdText.text = $"#{missionData.Id}";
        var dayDays = missionData.Duration > 1 ? " day" : " days";
        MissionDurationText.text = $"Expires in {missionData.Duration} {dayDays}";
        MissionScoutInfoText.text = $"Threat: {GetThreat(missionData, playerData)}\r\nHostiles: {GetHostiles(missionData, playerData)}";
        MissionDescText.text = missionData.Desc;
    }

    string GetThreat(MissionData missionData, CharacterData playerData)
    {
        string output = string.Empty;
        if (missionData.ForestDamage > 6)
            output = "Very high";
        else if (missionData.ForestDamage > 4)
            output = "High";
        else if (missionData.ForestDamage > 2)
            output = "Medium";
        else
            output = "Low";

        var scouting = playerData.Actions.FirstOrDefault();
        if (scouting != null)
            output = output + $" ({missionData.ForestDamage})";

            return output;
    }
    string GetHostiles(MissionData missionData, CharacterData playerData)
    {
        var scouting = playerData.Actions.FirstOrDefault();
        if (scouting != null)
        {
            return missionData.Hostiles.Count.ToString();
        }
        else
            return "Unknown";
    }
}
