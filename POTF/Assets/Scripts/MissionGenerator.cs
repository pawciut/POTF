using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// used for generating random missions/events
/// </summary>
public partial class MissionGenerator
{
    //Events & missions
    int NextMissionId = 0;

    List<Vector2> EventsProbabilty = new List<Vector2>
    {
    };

    Vector2 Event_01_Den_Chance = new Vector2(0, 0.15f);
    Vector2 Event_02_Maintenance_Chance = new Vector2(0.15f, 0.3f);
    Vector2 Event_03_Assault_Chance = new Vector2(0.3f, 0.55f);
    Vector2 Event_04_Escort_Chance = new Vector2(0.55f, 0.6f);
    Vector2 Event_05_Supply_Chance = new Vector2(0.6f, 0.67f);
    Vector2 Event_06_Scout_Chance = new Vector2(0.67f, 0.72f);
    Vector2 Event_07_Deterrence_Chance = new Vector2(0.72f, 0.97f);
    Vector2 Event_08_Emergency_Chance = new Vector2(0.97f, 1f);

    Vector2 Event_00_NoEvent_Chance = new Vector2(0.5f, 1f);

    public MissionData GetMission(CharacterData playerData)
    {
        var noEventValue = UnityEngine.Random.Range(0, 101);
        if (Event_00_NoEvent_Chance.x >= noEventValue)
        {
            //TODO no event today
        }
        else
        {
            var eventValue = UnityEngine.Random.Range(0, 101);

            MissionTypes draftedEventType = MissionTypes.None;
            if (InRange(Event_01_Den_Chance, eventValue))
                draftedEventType = MissionTypes.Den;
            if (InRange(Event_02_Maintenance_Chance, eventValue))
                draftedEventType = MissionTypes.Maintenance;
            if (InRange(Event_03_Assault_Chance, eventValue))
                draftedEventType = MissionTypes.Assault;
            if (InRange(Event_04_Escort_Chance, eventValue))
                draftedEventType = MissionTypes.Escort;
            if (InRange(Event_05_Supply_Chance, eventValue))
                draftedEventType = MissionTypes.Supply;
            if (InRange(Event_06_Scout_Chance, eventValue))
                draftedEventType = MissionTypes.Scout;
            if (InRange(Event_07_Deterrence_Chance, eventValue))
                draftedEventType = MissionTypes.Deterrence;
            if (InRange(Event_08_Emergency_Chance, eventValue))
                draftedEventType = MissionTypes.Emergency;

            var draftedMission = DraftMission(draftedEventType, playerData);
            return draftedMission;
        }
        return null;
    }


    MissionData DraftMission(MissionTypes missionType, CharacterData player)
    {
        MissionData missionData = null;
        switch (missionType)
        {
            case MissionTypes.Maintenance:
                if (Config_Maintenance_Pool.Any())
                {
                    //Pick mission draft
                    int draftedMission = UnityEngine.Random.Range(0, Config_Maintenance_Pool.Count);
                    var missionDraft = Config_Maintenance_Pool[draftedMission];

                    //Pick mission desc
                    int draftedMissionDesc = UnityEngine.Random.Range(0, Config_Maintenance_Desc_Pool.Count);
                    var missionDescription = Config_Maintenance_Desc_Pool[draftedMissionDesc];

                    //Draft Hostiles
                    var draftedHostiles = DraftHostiles(missionDraft.MaxHostiles,player.CurrentLevel);

                    //Finalize mission creation
                    missionData = new MissionData(missionDraft, Constants.Mission_Maintenance_Name, missionDescription, ++NextMissionId, draftedHostiles);
                }
                break;
            default:
                break;
        }

        return missionData;
    }

    List<HostileData> DraftHostiles(int maxHostiles, int playerLevel)
    {
        List<HostileData> hostiles = new List<HostileData>();

        int hostilesCount = UnityEngine.Random.Range(0, maxHostiles);
        var forThisLevel = Config_Hostile_Pool.Where(h => h.MinPlayerLevel <= playerLevel).ToList();

        for (int i=0;i<hostilesCount;++i)
        {
            int hostileDraftIndex = UnityEngine.Random.Range(0, forThisLevel.Count);
            var draftedHostile = forThisLevel[hostileDraftIndex];
            var hostile = new HostileData(draftedHostile);
            hostiles.Add(hostile);
        }

        return hostiles;
    }

    bool InRange(Vector2 range, float value)
    {
        return value >= range.x && value < range.y;
    }


}