using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapLogic : MonoBehaviour
{
    public Button StartGameButton;
    public Button NextTurnButton;

    //Player Data
    HeroData BearData;

    //TODO:PTRU
    List<System.Object> Effects;

    //Calendar Data
    //int Day { get { return TotalDays>0 && TotalDays % 7 == 0? 7 : TotalDays % 7; } }
    //int Week { get { return TotalDays > 0? (((TotalDays / 7)+1)%4) : 0; } }
    //int Month { get { return TotalDays > 0? (((TotalDays / 7) / 4) +1 % 12): 0; } }

    int Day { get { return TotalDays > 0 && TotalDays % 7 == 0 ? 7 : TotalDays % 7; } }
    int Week { get { return TotalDays > 0 ? (((TotalDays -1)/7)%4)+1 : 0; } }
    int Month { get { return TotalDays > 0 ? 
                ((((TotalDays % 28 == 0) ? TotalDays / 28 : TotalDays / 28 + 1)) % 12==0?
                   12 :
                 (((TotalDays % 28==0)?TotalDays / 28: TotalDays / 28 + 1))%12 )
                : 0; } }
    public int TotalDays;

    //MOD((INT((A1-1)/7)),4)+1
    //=MOD((INT(A1/7)+1),4)

    //Forest Conditions
    int MaxForestHp = 10;
    int CurrentFoestHp = 10;


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
    public Text StatusPanel_ActiveEffectsText;


    //Mission TODO:
    public GameObject MissionDemo_Panel;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        StartGameButton.gameObject.SetActive(false);
        MissionDemo_Panel.SetActive(true);
        NextTurn();
    }

    void NextTurn()
    {
        //generate events

        UpdateCalendar();
        UpdateForestAndDateAndEffects();
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
        BearData = new HeroData();
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
        var result = (float)CurrentFoestHp / MaxForestHp;
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
}
