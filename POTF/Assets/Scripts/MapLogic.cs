using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// logic for map scene
/// </summary>
public class MapLogic : MonoBehaviour
{
    //Player Data
    CharacterData BearData;


    //analytics
    bool analyticsVisible = false;
    public GameObject AnalyticsPanel;
    

    //Forest Conditions
    int MaxForestHp = 10;
    int CurrentForestHp = 10;



    //Mission Generator
    const int MaxMissions = 5;
    MissionGenerator EventGenerator = new MissionGenerator(new DraftingPools1(), new UnityRandomGenerator());
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



    //-----------------------Properties for inspector--------------------------

    [Header("UI")]
    public Button StartGameButton;
    public Button NextTurnButton;

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

    //Missions
    public GameObject MissionPanelPrefab;
    public RectTransform MissionContainer;
    public RectTransform[] SpawnPoints = new RectTransform[MaxMissions];
    public MissionResultScript MissionResultPanel;

    //welcome panel
    public GameObject WelcomePanel;
    
    
    //TODO:PTRU
    List<System.Object> Effects;

    //Calendar Data
    int TotalDays;
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



    [Header("Sound & Music")]
    public AudioSource ForestDamageSound;
    public AudioSource MissionSuccessSound;
    public AudioSource MissionFailedSound;

    //--------------------Code---------------------------------

    // Start is called before the first frame update
    void Start()
    {
        ResetAnalytics();
        UpdateUI();

        WelcomePanel?.SetActive(true);
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

    public void AutoresolveEvent(int id)
    {
        Debug.Log("Autoresolve");
        int eventIndex = -1;

        foreach (var kvp in AvailableMissions)
        {
            ++eventIndex;
            if (kvp.Value != null && kvp.Value.Id == id)
            {
                StringBuilder resolveLog = new StringBuilder();
                //rozpatrujemy

                bool isSuccess = true;

                //najpierw czy sa wrogowie
                if (kvp.Value.Hostiles.Any())
                {
                    PerformCombat(kvp.Value.Hostiles);

                    //check result
                    if (BearData.CurrentHp <= 0)
                    {
                        isSuccess = false;
                    }

                    //UpdateMissionResults

                    foreach (var hostile in kvp.Value.Hostiles)
                    {
                        if (hostile.CurrentHp <= 0)
                        {
                            IncrementAnalytics(Constants.Analytics_EnemiesSlained, 1);
                            AddExp(hostile.BaseExp);
                            resolveLog.AppendLine($"Enemy {hostile.Name} slained");
                        }
                        else
                        {
                            resolveLog.AppendLine($"Enemy {hostile.Name} alive");
                        }
                    }

                    if(!kvp.Value.Hostiles.Any())
                        resolveLog.AppendLine($"Hostiles: 0");
                }

                if (isSuccess)
                {
                    AddExp(kvp.Value.Exp);
                    IncrementAnalytics(Constants.Analytics_MissionsCompleted, 1);

                    if (ApplyBearRegen(kvp.Value))
                        resolveLog.AppendLine($"{BearData.Name} regenerates");
                    if (ApplyForestRegen(kvp.Value))
                        resolveLog.AppendLine($"Forest conditiona improved");

                    if(!String.IsNullOrEmpty(kvp.Value.MissionSuccessInfo))
                        resolveLog.AppendLine(kvp.Value.MissionSuccessInfo);
                }
                else
                {
                    IncrementAnalytics(Constants.Analytics_MissionsFailed, 1);
                    ApplyForestDamage(kvp.Value);
                }


                //wysietlanie wyniku i usuwanie misji
                UpdateForestAndDateAndEffects();
                UpdatePortraitAndLevel();
                ShowMissionResult(isSuccess, kvp.Value, resolveLog.ToString());
                CleanUpAfterMission(kvp.Value, eventIndex);
                NextTurnButton.interactable = false;

                //koniec rozpatrywanej misji
                break;
            }
        }
    }

    void CleanUpAfterMission(MissionData mission, int index)
    {
        Destroy(MissionPanels[index]);
        MissionPanels[index] = null;
        AvailableMissions[index] = null;
    }

    void ShowMissionResult(bool isSuccess, MissionData missionData, string log)
    {
        if (isSuccess)
        {
            MissionResultPanel.UpdateSuccessData(missionData, log);
            MissionSuccessSound.Play();
        }
        else
        {
            MissionResultPanel.UpdateFailedData(missionData, log);
            MissionFailedSound.Play();
        }
        MissionResultPanel.gameObject.SetActive(true);
    }

    void AddExp(int exp)
    {
        var expCap = GetExpCapForThisLevel(BearData.CurrentLevel);
        BearData.CurrentExp += exp;

        if (BearData.CurrentExp >= expCap)
        {
            //level up
            BearData.CurrentLevel++;
        }
        UpdatePortraitAndLevel();
    }

    void PerformCombat(List<HostileData> hostiles)
    {
        //dopuki niedziedz zyje i sa przeciwnicy
        while (BearData.CurrentHp > 0 && hostiles.Any(h => h.CurrentHp > 0))
        {

            var livingHostiles = hostiles.Where(h => h.CurrentHp > 0);
            bool multipleEnemies = livingHostiles.Count() > 1;
            //round
            BearData.RefreshAP();

            //dopuki ma ruchy a przeciwnicy zyja to bij
            while (BearData.CurrentActionPoints > 0 && hostiles.Any(h => h.CurrentHp > 0))
            {
                //czy oplaca sie cleave
                if (multipleEnemies && BearData.CurrentActionPoints >= GetBearCleave().APCost)
                {
                    PerformCleaveAttack(BearData, hostiles.Where(h => h.CurrentHp > 0), GetBearCleave());
                }
                else if (BearData.CurrentActionPoints >= GetBearSingleAttack().APCost)
                {
                    //single attack
                    PerformSingleAttack(BearData, hostiles.FirstOrDefault(h => h.CurrentHp > 0), GetBearSingleAttack());
                }
            }

            //ruch przeciwnikow
            foreach (var aliveHostile in hostiles.Where(h => h.CurrentHp > 0))
            {
                aliveHostile.RefreshAP();
                //dopoki ma ruchy i niedzwiedz zyje
                while (aliveHostile.CurrentActionPoints > 0 && BearData.CurrentHp > 0)
                {
                    //najpierw ranged
                    var rangedAttack = (Action_Attack)aliveHostile.Actions.FirstOrDefault(a => a is Action_Attack && (a as Action_Attack).AttackType == AttackTypes.Ranged);
                    var meleeAttack = (Action_Attack)aliveHostile.Actions.FirstOrDefault(a => a is Action_Attack && (a as Action_Attack).AttackType == AttackTypes.Melee);
                    if (rangedAttack != null && aliveHostile.CurrentActionPoints >= rangedAttack.APCost)
                    {
                        PerformSingleAttack(aliveHostile, BearData, rangedAttack);
                    }
                    else if (meleeAttack != null && aliveHostile.CurrentActionPoints >= meleeAttack.APCost)
                    {
                        //melee
                        PerformSingleAttack(aliveHostile, BearData, meleeAttack);
                    }
                }
            }
            //next round
        }
    }


    /// <summary>
    /// removes AP and checks if hit or not, does not apply effect
    /// </summary>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    bool TryHit(CharacterData source, ActionData action)
    {
        source.CurrentActionPoints -= action.APCost;

        var hitRoll = UnityEngine.Random.Range(0f, 1f);
        var hitChance = action.ChanceOfSuccess(source, null);
        if (hitRoll > hitChance)
        {
            Debug.Log($"{source.Name} failed {action.ToString()}");
            //missed
            return false;
        }
        return true;
    }

    void PerformSingleAttack(CharacterData source, CharacterData target, Action_Attack attack)
    {
        if (TryHit(source, attack))
        {
            if (target.ApplyDamage(attack))
                Debug.Log($"{source.Name} apply {attack.ToString()} to {target.Name}");
        }
    }
    void PerformCleaveAttack(CharacterData source, IEnumerable<CharacterData> target, Action_Attack attack)
    {
        if (TryHit(source, attack))
        {
            //hit not missed
            foreach (var tar in target)
            {
                if (tar.ApplyDamage(attack))
                    Debug.Log($"{source.Name} apply {attack.ToString()} to {tar.Name}");
            }
        }
    }

    Action_Attack GetBearCleave()
    {
        return (Action_Attack)BearData.Actions.FirstOrDefault(a => a is Action_Attack && (a as Action_Attack).AttackType == AttackTypes.Melee_Cleave);
    }
    Action_Attack GetBearSingleAttack()
    {
        return (Action_Attack)BearData.Actions.FirstOrDefault(a => a is Action_Attack && (a as Action_Attack).AttackType == AttackTypes.Melee);
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
                if (ApplyForestDamage(kvp.Value))
                    wasForestDamaged = true;


                //remove panel
                Destroy(MissionPanels[currentIndex]);
                MissionPanels[currentIndex] = null;

                IncrementAnalytics(Constants.Analytics_MissionsExpired, 1);
            }

        }



        if (wasForestDamaged)
            ForestDamageSound.Play();
    }
    bool ApplyBearRegen(MissionData missionData)
    {
        if (BearData.CurrentHp < BearData.MaxHp)
        {
            if (BearData.CurrentHp + missionData.RegenPlayer > BearData.MaxHp)
                BearData.CurrentHp = BearData.MaxHp;
            else
                BearData.CurrentHp += missionData.RegenPlayer;
            return true;
        }
        return false;
    }

    bool ApplyForestRegen(MissionData missionData)
    {
        if (CurrentForestHp < MaxForestHp)
        {
            if (CurrentForestHp + missionData.RegenForest > MaxForestHp)
                CurrentForestHp = MaxForestHp;
            else
                CurrentForestHp += missionData.RegenForest;
            return true;
        }
        return false;
    }

    bool ApplyForestDamage(MissionData missionData)
    {
        if (missionData.ForestDamage > 0)
        {
            CurrentForestHp -= missionData.ForestDamage;
            return true;
        }
        return false;
    }


    void IncrementAnalytics(string key, int value)
    {
        var oldValue = PlayerPrefs.GetInt(key);
        PlayerPrefs.SetInt(key, oldValue + value);
        UpdateAnalyticsPanel();
    }

    public void CheckLoseConditions()
    {
        if (CurrentForestHp <= 0 || BearData.CurrentHp <= 0)
        {
            gameObject.GetComponent<NavigationScript>().GoToDefeat();
        }
        NextTurnButton.interactable = true;
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

                //attach autoresolve handler
                missionPanelScript.AutoButton.onClick.AddListener(delegate { AutoresolveEvent(draftedEvent.Id); });

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
    public void DEBUG_AddExp()
    {
        AddExp(50);
        UpdatePortraitAndLevel();
    }

    void UpdateUI()
    {
        UpdatePortraitAndLevel();
        UpdateForestAndDateAndEffects();
    }

    public void DraftBeforeStart()
    {
        BearData = new CharacterData()
        {
            Name = "Bear",
            Actions = new List<ActionData>
            {
                new Action_Attack(),
                new Action_Attack(1,2, AttackTypes.Melee_Cleave),
                new Action_Dodge(),
                new Action_Fix(),
            }
        };

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
