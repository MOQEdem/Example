using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class AnalyticManager : MonoBehaviour
{
    private GameAnalyticsObject _gameAnalyticsObject;
    private int _levelNumber;

    private void Awake()
    {
        GameAnalytics.Initialize();

        _gameAnalyticsObject = new GameAnalyticsObject();
        _levelNumber = SceneManager.GetActiveScene().buildIndex;
    }

    public void SendEventOnLevelStart()
    {
        _gameAnalyticsObject.OnLevelStart(_levelNumber);
    }

    public void SendEventOnLevelComplete()
    {
        _gameAnalyticsObject?.OnLevelComplete(_levelNumber);
    }

    public void SendEventOnFail()
    {
        _gameAnalyticsObject?.OnFail(_levelNumber);
    }

    public void SendEventOnLevelRestart()
    {
        _gameAnalyticsObject?.OnLevelRestart(_levelNumber);
    }

    public void SendEventOnReviveUsed()
    {
        _gameAnalyticsObject.OnReviveUsed(_levelNumber);
    }

    public void SendEventOnCatapultUsed()
    {
        _gameAnalyticsObject.OnCatapultUsed(_levelNumber);
    }

    public void SendEventOnDragonUsed()
    {
        _gameAnalyticsObject.OnDragonUsed(_levelNumber);
    }

    public void SendEventOnGoldRewardUsed()
    {
        _gameAnalyticsObject.OnGoldRewardUsed(_levelNumber);
    }

    public void SendEventOnMountUsed()
    {
        _gameAnalyticsObject.OnMountUsed(_levelNumber);
    }
}
