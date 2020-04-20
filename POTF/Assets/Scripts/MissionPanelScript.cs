using System.Collections;
using System.Collections.Generic;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(MissionData missionData)
    {
        //Tylko dla maintenance ma byc dostepny?
        GoButton.interactable = false;
        MissionNameText.text = missionData.Name;
        //MissionIdText.text
    }
}
