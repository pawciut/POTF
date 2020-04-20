using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticsPanelScript : MonoBehaviour
{
    public Text Description;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateData()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Total score: {PlayerPrefs.GetInt(Constants.Analytics_TotalScore)}");
        sb.AppendLine($"Total days: {PlayerPrefs.GetInt(Constants.Analytics_TotalDays)}");
        sb.AppendLine($"Max level: {PlayerPrefs.GetInt(Constants.Analytics_ReachedMaxLevel)}");
        sb.AppendLine($"Missions completed: {PlayerPrefs.GetInt(Constants.Analytics_MissionsCompleted)}");
        sb.AppendLine($"Missions failed: {PlayerPrefs.GetInt(Constants.Analytics_MissionsFailed)}");
        sb.AppendLine($"Missions expired: {PlayerPrefs.GetInt(Constants.Analytics_MissionsExpired)}");
        sb.AppendLine($"Enemies slained: {PlayerPrefs.GetInt(Constants.Analytics_EnemiesSlained)}");
        Description.text = sb.ToString();
    }


    void ResetAnalytics()
    {
        PlayerPrefs.SetInt(Constants.Analytics_TotalScore, 0);
        PlayerPrefs.SetInt(Constants.Analytics_TotalDays, 0);
        PlayerPrefs.SetInt(Constants.Analytics_ReachedMaxLevel, 0);
        PlayerPrefs.SetInt(Constants.Analytics_MissionsCompleted, 0);
        PlayerPrefs.SetInt(Constants.Analytics_MissionsFailed, 0);
        PlayerPrefs.SetInt(Constants.Analytics_MissionsExpired, 0);
        PlayerPrefs.SetInt(Constants.Analytics_EnemiesSlained, 0);
    }
}
