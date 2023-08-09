using UnityEngine;
using GameAnalyticsSDK;
using System.Collections.Generic;

public class GameAnalyticsObject
{
    public void OnLevelStart(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start", new Dictionary<string, object>()
        {
            {"level", levelNumber }
        });
    }

    public void OnLevelComplete(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_complete", new Dictionary<string, object>()
        {
            {"level", levelNumber },
            {"time_spent",(int)Time.timeSinceLevelLoad }
        });
    }

    public void OnFail(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "fail", new Dictionary<string, object>()
        {
            {"level", levelNumber },
            {"time_spent",(int)Time.timeSinceLevelLoad }
        });
    }

    public void OnLevelRestart(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "restart", new Dictionary<string, object>()
        {
            {"level",levelNumber }
        });
    }

    public void OnReviveUsed(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "revive", new Dictionary<string, object>()
        {
            {"level",levelNumber }
        });
    }

    public void OnCatapultUsed(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "catapult_used", new Dictionary<string, object>()
        {
            {"level",levelNumber }
        });
    }

    public void OnDragonUsed(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "dragon_used", new Dictionary<string, object>()
        {
            {"level",levelNumber }
        });
    }

    public void OnGoldRewardUsed(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "goldreward_used", new Dictionary<string, object>()
        {
            {"level",levelNumber }
        });
    }

    public void OnMountUsed(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "mount_used", new Dictionary<string, object>()
        {
            {"level",levelNumber }
        });
    }
}
