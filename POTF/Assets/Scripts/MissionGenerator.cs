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
    IDraftingPools draftingPools;

    public MissionGenerator(IDraftingPools draftingPools)
    {
        this.draftingPools = draftingPools;
    }
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
        Debug.Log("GetMission");
        var noEventValue = UnityEngine.Random.Range(0f, 1f);
        Debug.Log($"GetMission noEventChance:{noEventValue}");
        if (Event_00_NoEvent_Chance.x >= noEventValue)
        {
            //TODO no event today
        }
        else
        {
            var eventValue = UnityEngine.Random.Range(0f, 1f);
            Debug.Log($"GetMission eventValue:{eventValue}");
            MissionTypes draftedEventType = MissionTypes.None;
            if (InRange(Event_01_Den_Chance, eventValue))
                draftedEventType = MissionTypes.Den;
            if (InRange(Event_02_Maintenance_Chance, eventValue))
                draftedEventType = MissionTypes.Maintenance;
            if (InRange(Event_03_Assault_Chance, eventValue))
                draftedEventType = MissionTypes.Assault;
            if (InRange(Event_04_Escort_Chance, eventValue))
                draftedEventType = MissionTypes.Maintenance;//MissionTypes.Escort;
            if (InRange(Event_05_Supply_Chance, eventValue))
                draftedEventType = MissionTypes.Supply;
            if (InRange(Event_06_Scout_Chance, eventValue))
                draftedEventType = MissionTypes.Maintenance;//MissionTypes.Scout;
            if (InRange(Event_07_Deterrence_Chance, eventValue))
                draftedEventType = MissionTypes.Maintenance;//MissionTypes.Deterrence;
            if (InRange(Event_08_Emergency_Chance, eventValue))
                draftedEventType = MissionTypes.Maintenance;//MissionTypes.Emergency;

            var draftedMission = DraftMission(draftedEventType, playerData);
            //var draftedMission = DraftMission(MissionTypes.Maintenance, playerData);
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
                    missionData =GetM(missionType, Config_Maintenance_Pool, Config_Maintenance_Desc_Pool, player, Constants.Mission_Maintenance_Name);
                    
                    ////Pick mission draft
                    //int draftedMission = UnityEngine.Random.Range(0, Config_Maintenance_Pool.Count);
                    //var missionDraft = Config_Maintenance_Pool[draftedMission];

                    ////Pick mission desc
                    //int draftedMissionDesc = UnityEngine.Random.Range(0, Config_Maintenance_Desc_Pool.Count);
                    //var missionDescription = Config_Maintenance_Desc_Pool[draftedMissionDesc];

                    ////Draft Hostiles
                    //var draftedHostiles = DraftHostiles(missionDraft.MaxHostiles, player.CurrentLevel);

                    ////Finalize mission creation
                    //missionData = new MissionData(missionType, missionDraft, Constants.Mission_Maintenance_Name, missionDescription, ++NextMissionId, draftedHostiles);
                }
                break;
            case MissionTypes.Den:
                missionData =GetM(missionType, Config_Den_Pool, Config_Den_Desc_Pool, player, Constants.Mission_Den_Name);
                break;

            case MissionTypes.Assault:
                missionData =GetM(missionType, Config_Assault_Pool, Config_Assault_Desc_Pool, player, Constants.Mission_Assault_Name);
                break;
            case MissionTypes.Supply:
                missionData =GetM(missionType, Config_Supply_Pool, Config_Supply_Desc_Pool, player, Constants.Mission_Assault_Name);
                break;
            default:
                break;
        }

        return missionData;
    }

    MissionData GetM(MissionTypes missionType,List<MissionDraftConfiguration> draftPool, List<string> descPool, CharacterData player, string missionName)
    {
        //Pick mission draft
        int draftedMission = UnityEngine.Random.Range(0, draftPool.Count);
        var missionDraft = draftPool[draftedMission];

        //Pick mission desc
        int draftedMissionDesc = UnityEngine.Random.Range(0, descPool.Count);
        var missionDescription = descPool[draftedMissionDesc];

        //Draft Hostiles
        var draftedHostiles = DraftHostiles(missionDraft, player.CurrentLevel);

        //Finalize mission creation
        return new MissionData(missionType, missionDraft, missionName, missionDescription, ++NextMissionId, draftedHostiles);
    }

    public List<HostileData> DraftHostiles(MissionDraftConfiguration missionDraftConfiguration, int playerLevel)
    {
        List<HostileData> hostiles = new List<HostileData>();

        int hostilesCount = UnityEngine.Random.Range(0, missionDraftConfiguration.MaxHostiles + 1);
        var forThisLevel = draftingPools.Config_Hostile_Pool.Where(h => h.MinPlayerLevel <= playerLevel).ToList();

        for (int i = 0; i < hostilesCount; ++i)
        {
            //dog elite
            //int hostileDraftIndex = 0;
            int hostileDraftIndex = UnityEngine.Random.Range(0, forThisLevel.Count);
            if (hostileDraftIndex < forThisLevel.Count)
            {
                var draftedHostile = forThisLevel[hostileDraftIndex];
                var hostile = new HostileData(draftedHostile);
                hostiles.Add(hostile);
            }
        }

        return hostiles;
    }

    bool InRange(Vector2 range, float value)
    {
        return value >= range.x && value < range.y;
    }


}