public class Constants
{
    //scenes
    public const string MapSceneName = "MapScene";
    public const string Mission1SceneName = "MissionAScene";
    public const string MenuSceneName = "MenuScene";
    public const string OptionSceneName = "optionsScene";
    public const string DefeatSceneName = "DefeatScene";
    public const string CreditsSceneName = "CreditsScene";
    public const string HowToPlaySceneName = "HowToPlayScene";

    //formats
    public const string StatusPanel_ForestConditionFormat = "Forest condition:\r\n {0}";
    public const string StatusPanel_DateFormat = "Day {0}, Week {1}, Month {2}";
    public const string StatusPanel_UnhandledEventsFormat = "Events {0}/{1}";

    //Modifiers
    public const float Mod_HitChancePerAgility = 0.05f;
    public const float Mod_HitChancePerIntelect = 0.05f;
    public const float Mod_DodgeChancePerAgility = 0.05f;
    public const float Mod_DodgeChancePerIntelect = 0.05f;
    public const float Mod_ReduceDodgeChancePerAttackerAgilityWhenRangedAttack = 0.05f;
    //public const float Mod_FixChancePerAgility = 0.1f;
    public const float Mod_FixChancePerIntelect = 0.1f;
    public const float Mod_MeleeDamageBonusPerPower = 0.5f;

    //Map BaseExp

    //Mission_Names
    public const string Mission_Den_Name = "Den";
    public const string Mission_Maintenance_Name = "Maintenance";
    public const string Mission_Assault_Name = "Assault";
    public const string Mission_Escort_Name = "Escort";
    public const string Mission_Supply_Name = "Supply";
    public const string Mission_Scout_Name = "Scout";
    public const string Mission_Deterrence_Name = "Deterrence";
    public const string Mission_Emergency_Name = "Emergency";

    public const string Mission_Maintenance_Success = "Bear fixed the thing";


    //Mission_Maintenance Desc
    public const string Mission_Maintenance_Desc_01 = "Animals reported that someone blocked the stream with a rock. You need to remove this rock in order to restore the balance of the forest.";
    public const string Mission_Maintenance_Desc_02 = "Someone broke the feeding rack. You need to go and fix it.";
    public const string Mission_Den_Desc_01 = "There is some free time to go rest in your den";

    //Analytics
    public const string Analytics_TotalScore = "Analytics_TotalScore";
    public const string Analytics_TotalDays = "Analytics_TotalDays";
    public const string Analytics_ReachedMaxLevel = "Analytics_ReachedMaxLevel";
    public const string Analytics_MissionsCompleted = "Analytics_MissionsCompleted";
    public const string Analytics_MissionsFailed = "Analytics_MissionsFailed";
    public const string Analytics_MissionsExpired = "Analytics_MissionsExpired";
    public const string Analytics_EnemiesSlained = "Analytics_EnemiesSlained";

    //Attribute_Progress_Levels
    public static readonly int[] Attribute_Levels = new int[11]{
        0, //level 0
        100,//level 1, 0-100
        220,//level 2, 100-220
        400,//level 3, 220-400
        650,//level 4, 400-650
        950,//level 5, 650-950
        1270,//level 6, 950-1270
        1650,//level 7, 1270-1650
        2050,//level 8, 1650-2050
        2470,//level 9, 2050-2470
        2910//level 10, 2470-2910
    };

    //Attribute_Progress_Levels
    public static readonly int[] Exp_Levels_Caps = new int[11]{
        0, //level 0
        100,//level 1, 0-100
        200,//level 2, 100-220
        400,//level 3, 220-400
        600,//level 4, 400-650
        800,//level 5, 650-950
        1000,//level 6, 950-1270
        1200,//level 7, 1270-1650
        1500,//level 8, 1650-2050
        1700,//level 9, 2050-2470
        2910//level 10, 2470-2910
    };
}