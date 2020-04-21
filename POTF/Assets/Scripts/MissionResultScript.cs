using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionResultScript : MonoBehaviour
{
    public Text HeaderText;
    public Text LogText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSuccessData(MissionData missionData, string log)
    {
        HeaderText.text = $"{missionData.Name} #{missionData.Id} completed";
        LogText.text = log;
    }
    public void UpdateFailedData(MissionData missionData, string log)
    {
        HeaderText.text = $"{missionData.Name} #{missionData.Id} failed";
        LogText.text = log;
    }
}
