using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    private const string LevelsLeaderBoard = "LevelsLeaderBoard";

    [SerializeField] private Transform _content;
    [SerializeField] private LeaderBoardPlayerInfo _playerInfoPrefab;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Button _leaderBoardButton;
    [SerializeField] private GameObject _authorizationButton;

    private void OnEnable()
    {
        _authorizationButton.gameObject.SetActive(false);
        _levelLoader.LevelFinised += SetScore;
    }

    private void OnDisable()
    {
        _levelLoader.LevelFinised -= SetScore;
    }

    public void Show()
    {

        ClearContent();

        _leaderBoardButton.interactable = false;
#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            if (PlayerAccount.IsAuthorized && !PlayerAccount.HasPersonalProfileDataPermission)
            {
                Leaderboard.SetScore(LevelsLeaderBoard, PlayerPrefs.GetInt("ComplitedLevels"));
            }

            Leaderboard.GetEntries(LevelsLeaderBoard, (result) =>
            {
                foreach (var entrie in result.entries)
                {
                    LeaderBoardPlayerInfo playerInfo = Instantiate(_playerInfoPrefab, _content);
                    string name = entrie.player.publicName;
                    if (string.IsNullOrEmpty(name))
                        name = "Anonym";
                    playerInfo.SetInfo(name, entrie.score.ToString());
                }
            });
        }
        else
        {
            ClearContent();
            _authorizationButton.SetActive(true);
        }
#endif
    }

    private void SetScore()
    {
#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            if (PlayerAccount.HasPersonalProfileDataPermission)
            {
                if (PlayerAccount.HasPersonalProfileDataPermission)
                    Leaderboard.SetScore(LevelsLeaderBoard, PlayerPrefs.GetInt("ComplitedLevels"));
            }
        }
#endif
    }

    public void OnClosetButtonClick()
    {
        _leaderBoardButton.interactable = true;
        gameObject.SetActive(false);
    }

    private void ClearContent()
    {
        if (_content.childCount > 0)
        {
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
