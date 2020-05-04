using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierPanelScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Name_TMP;
    public TMPro.TextMeshProUGUI CurrentHP_TMP;
    public Image HP_Progress;
    public TMPro.TextMeshProUGUI AP_TMP;
    public TMPro.TextMeshProUGUI MP_TMP;
    public TMPro.TextMeshProUGUI Att_Power_TMP;
    public Image Att_Power_Progress;
    public TMPro.TextMeshProUGUI Att_Agility_TMP;
    public Image Att_Agility_Progress;
    public TMPro.TextMeshProUGUI Att_Intellect_TMP;
    public Image Att_Intellect_Progress;
    public TMPro.TextMeshProUGUI Att_Tenacity_TMP;
    public Image Att_Tenacity_Progress;
    public Text Level_Text;
    public Image Level_Progress;
    //TODO: czy tu powienien byc skrypt przycisku skilli/ skryptu skilli
    public GameObject Skill1;
    public GameObject Skill2;
    public GameObject Skill3;
    public GameObject Skill4;
    public GameObject Skill5;

    


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Set(SoldierData data)
    {
        this.Name_TMP.text = data.Name;
        this.CurrentHP_TMP.text = $"{data.CurrentHp}/{data.MaxHp}";
        this.HP_Progress.fillAmount = data.GetHpPercent();
        this.AP_TMP.text = data.CurrentActionPoints.ToString();
        this.MP_TMP.text = string.Empty;//TODO
        this.Att_Power_TMP.text = data.Power.ToString();
        this.Att_Power_Progress.fillAmount = data.GetAttributeProgressPercent(AttributeTypes.Power);
        this.Att_Agility_TMP.text = data.Agility.ToString();
        this.Att_Agility_Progress.fillAmount = data.GetAttributeProgressPercent(AttributeTypes.Agility);
        this.Att_Intellect_TMP.text = data.Intelect.ToString();
        this.Att_Intellect_Progress.fillAmount = data.GetAttributeProgressPercent(AttributeTypes.Intellect);
        this.Att_Tenacity_TMP.text = data.Tenacity.ToString();
        this.Att_Tenacity_Progress.fillAmount = data.GetAttributeProgressPercent(AttributeTypes.Tenacity);
        this.Level_Text.text = data.CurrentLevel.ToString();
        this.Level_Progress.fillAmount = data.GetLevelPercent();
        //TODO: czy tu powienien byc skrypt przycisku skilli/ skryptu skilli
        //this.Skill1 = null;
        //this.Skill2 = null;
        //this.Skill3 = null;
        //this.Skill4 = null;
        //this.Skill5 = null;
    }
}
