using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// logic for map scene
/// </summary>
public class MapLogic : MonoBehaviour
{
    public Button StartGameButton;
    public Button NextTurnButton;

    //Player Data
    CharacterData BearData;

    //analytics
    bool analyticsVisible = false;
    public GameObject AnalyticsPanel;

    //TODO:PTRU
    List<System.Object> Effects;

    //Calendar Data
    public int TotalDays;
    int Day { get { return TotalDays > 0 && TotalDays % 7 == 0 ? 7 : TotalDays % 7; } }
    int Week { get { return TotalDays > 0 ? (((TotalDays - 1) / 7) % 4) + 1 : 0; } }
    int Month
    {
        get
        {
            return TotalDays > 0 ?
      ((((TotalDays % 28 == 0) ? TotalDays / 28 : TotalDays / 28 + 1)) % 12 == 0 ?
         12 :
       (((TotalDays % 28 == 0) ? TotalDays / 28 : TotalDays / 28 + 1)) % 12)
      : 0;
        }
    }



    //Forest Conditions
    int MaxForestHp = 10;
    int CurrentForestHp = 10;
    public AudioSource ForestDamageSound;


    //Portrait
    public Button Portrait_Button;
    public GameObject Portrait_BearHead;
    public Text Portrait_PowerText;
    public Text Portrait_AgilityText;
    public Text Portrait_IntelectText;
    public Text Portrait_HealthText;

    //Level and exp
    public Button Level_Button;
    public Image Level_ExpValue;
    public Text Level_Text;

    //ForestAndDateAndEffects
    public Button StatusPanel_Button;
    public Text StatusPanel_ForestText;
    public Text StatusPanel_DateText;
    public Text StatusPanel_UnhandledEventsText;
    public Text StatusPanel_ActiveEffectsText;


    //Mission Generator
    public const int MaxMissions = 5;
    public GameObject MissionPanelPrefab;
    public RectTransform MissionContainer;
    public RectTransform[] SpawnPoints = new RectTransform[MaxMissions];
    MissionGenerator EventGenerator = new MissionGenerator();
    /// <summary>
    /// key spawn point index, value mission
    /// </summary>
    Dictionary<int, MissionData> AvailableMissions = new Dictionary<int, MissionData>(MaxMissions)
    {
        { 0, null },
        { 1, null },
        { 2, null },
        { 3, null },
        { 4, null },
    };
    GameObject[] MissionPanels = new GameObject[MaxMissions];


    // Start is called before the first frame update
    void Start()
    {
        ResetAnalytics();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

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

    public void StartGame()
    {
        StartGameButton.gameObject.SetActive(false);
        NextTurn();
    }

    public void NextDay()
    {
        NextTurn();
    }

    void NextTurn()
    {
        //expire events
        ExpireEvents();

        //Update score before lose condition
        PlayerPrefs.SetInt(Constants.Analytics_TotalScore, CalculateScore());
        UpdateAnalyticsPanel();

        //Check is forest still alive
        CheckLoseConditions();

        //generate events
        GenerateEvents();

        UpdateCalendar();
        UpdateForestAndDateAndEffects();
        IncrementAnalytics(Constants.Analytics_TotalDays, 1);

    }

    int CalculateScore()
    {
        int total = 0;

        //days
        total += TotalDays * 2;

        //missions
        var missionsCompleted = PlayerPrefs.GetInt(Constants.Analytics_MissionsCompleted);
        total += missionsCompleted * 10;

        //level
        total += BearData.CurrentLevel * 20;

        //hp left
        total += BearData.CurrentHp * 20;

        //enemies
        var enemiesSlained = PlayerPrefs.GetInt(Constants.Analytics_EnemiesSlained);
        total += enemiesSlained * 5;

        return total;
    }

    void ExpireEvents()
    {
        bool wasForestDamaged = false;

        int currentIndex = -1;
        foreach (var kvp in AvailableMissions)
        {
            ++currentIndex;

            if (kvp.Value != null && kvp.Value.Duration > 1)
            {
                kvp.Value.Duration -= 1;
                MissionPanels[currentIndex].GetComponent<MissionPanelScript>().UpdateText(kvp.Value, BearData);
            }
            //Expire and remove
            else if (kvp.Value != null && kvp.Value.Duration == 1)
            {
                //apply effect
                if (kvp.Value.ForestDamage > 0)
                {
                    CurrentForestHp -= kvp.Value.ForestDamage;
                    wasForestDamaged = true;
                }


                //remove panel
                Destroy(MissionPanels[currentIndex]);
                MissionPanels[currentIndex] = null;

                IncrementAnalytics(Constants.Analytics_MissionsExpired, 1);
            }

        }



        if (wasForestDamaged)
            ForestDamageSound.Play();
    }


    void IncrementAnalytics(string key, int value)
    {
        var oldValue = PlayerPrefs.GetInt(key);
        PlayerPrefs.SetInt(key, oldValue + value);
        UpdateAnalyticsPanel();
    }

    void CheckLoseConditions()
    {
        if (CurrentForestHp <= 0 || BearData.CurrentHp <= 0)
        {
            gameObject.GetComponent<NavigationScript>().GoToDefeat();
        }
    }

    void GenerateEvents()
    {
        Debug.Log("GenerateEvents BEgin");
        var draftedEvent = EventGenerator.GetMission(BearData);
        if (draftedEvent != null)
        {
            Debug.Log($"Drafted {draftedEvent.Name} #{draftedEvent.Id}");
            //czy sa wolne spawn pointy
            if (AvailableMissions.Values.Any(v => v == null))
            {
                int emptyIndex = -1;
                foreach (var kvp in AvailableMissions)
                {
                    if (kvp.Value == null)
                        ++emptyIndex;
                }
                AvailableMissions[emptyIndex] = draftedEvent;

                //create panel
                var spawner = SpawnPoints[emptyIndex];
                var newMissionPanel = Instantiate(MissionPanelPrefab, spawner);
                MissionPanels[emptyIndex] = newMissionPanel;
                var missionPanelScript = newMissionPanel.GetComponent<MissionPanelScript>();
                missionPanelScript.UpdateText(draftedEvent, BearData);

                //Instantiate(MissionPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                //usun losowy najblizszy wygasnieciu i zastosuj jego obrazenia
            }
            //display event on map
        }
    }


    void UpdateCalendar()
    {
        ++TotalDays;
    }

    public void DEBUG_AddDay()
    {
        UpdateCalendar();
        UpdateForestAndDateAndEffects();
    }

    void UpdateUI()
    {
        UpdatePortraitAndLevel();
        UpdateForestAndDateAndEffects();
    }

    public void DraftBeforeStart()
    {
        BearData = new CharacterData();
        UpdateUI();

    }

    void UpdatePortraitAndLevel()
    {

        if (BearData == null)
        {
            Portrait_Button.gameObject.SetActive(false);
            Level_Button.gameObject.SetActive(false);
        }
        else
        {
            Portrait_Button.gameObject.SetActive(true);
            Level_Button.gameObject.SetActive(true);

            Portrait_PowerText.text = IntToValue(BearData.Power);
            Portrait_AgilityText.text = IntToValue(BearData.Agility);
            Portrait_IntelectText.text = IntToValue(BearData.Intelect);
            Portrait_HealthText.text = $"{BearData.CurrentHp} / {BearData.MaxHp}";

            Level_Text.text = BearData.CurrentLevel.ToString();
            Level_ExpValue.fillAmount = BearData.CurrentExp / GetExpCapForThisLevel(BearData.CurrentLevel);
        }
    }

    void UpdateForestAndDateAndEffects()
    {
        if (BearData == null)
        {
            StatusPanel_Button.gameObject.SetActive(false);
        }
        else
        {
            StatusPanel_Button.gameObject.SetActive(true);
            StatusPanel_ForestText.text = String.Format(Constants.StatusPanel_ForestConditionFormat, ForestConditionToString());
            StatusPanel_DateText.text = string.Format(Constants.StatusPanel_DateFormat, IntToValue(Day), IntToValue(Week), IntToValue(Month));
            StatusPanel_UnhandledEventsText.text = String.Format(Constants.StatusPanel_UnhandledEventsFormat, AvailableMissions.Count, MaxMissions);
            StatusPanel_ActiveEffectsText.text = EffectsToString();
        }
    }

    float GetExpCapForThisLevel(int level)
    {
        switch (level)
        {
            case 1:
                return 100;
            case 2:
                return 200;
            case 3:
                return 400;
            case 4:
                return 600;
            case 5:
                return 800;
            case 6:
                return 1000;
            case 7:
                return 1100;
            case 8:
                return 1200;
            case 9:
                return 1500;
            case 10:
                return 1700;
            default:
                return 0;
        }
    }

    string ForestConditionToString()
    {
        var result = (float)CurrentForestHp / MaxForestHp;
        if (result > 0.9f)
            return "Good";
        else if (result > 0.6f)
            return "So so";
        else if (result >= 0.3f)
            return "Poor";
        else if (result > 0)
            return "Dying";
        else
            return "Dead";
    }

    string EffectsToString()
    {
        if (Effects == null || !Effects.Any())
            return "none";
        else
        {
            //TODO:PTRU do przetestowania
            return String.Join("\r\n- ", Effects.ToArray());
        }
    }

    string IntToValue(int value)
    {
        if (value > 0)
            return value.ToString();
        return string.Empty;
    }

    public void SaveHeroData()
    {
        ///PlayerPrefs.
    }

    public void ToggleAnalytics()
    {
        if (analyticsVisible)
            AnalyticsPanel.SetActive(false);
        else
        {
            AnalyticsPanel.SetActive(true);
            UpdateAnalyticsPanel();
        }

        analyticsVisible = !analyticsVisible;
    }

    void UpdateAnalyticsPanel()
    {
        AnalyticsPanel.GetComponent<AnalyticsPanelScript>().UpdateData();
    }
}
