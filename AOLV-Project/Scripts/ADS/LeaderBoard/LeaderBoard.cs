using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    private const string ResourceLeaderBoard = "ResourceLeaderBoard";

    [SerializeField] private ResourceCounter _resourceCounter;
    [SerializeField] private LeaderBoardPlayerInfo _playerInfoPrefab;
    [SerializeField] private Transform _contend;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _authorizeButton;
    [SerializeField] private TMP_Text _text;

    private Resizer _resizer;
    private Vector3 _normalScale;

    private void OnEnable()
    {
        _resizer = GetComponent<Resizer>();

        _authorizeButton.onClick.AddListener(OnAuthorizeButtonClick);
    }

    private void OnDisable()
    {
        _authorizeButton.onClick.RemoveListener(OnAuthorizeButtonClick);
    }

    private void Start()
    {
        _normalScale = new Vector3(1, 1, 1);
        _resizer.Resize(0.1f, 0f);
    }

    public void OnLeaderBoardButtonClick()
    {
        _canvasGroup.interactable = false;

        Show();

        if (PlayerAccount.IsAuthorized)
        {
            _authorizeButton.gameObject.SetActive(false);
            _text.gameObject.SetActive(false);
            _authorizeButton.interactable = false;
        }
        else
        {
            _authorizeButton.gameObject.SetActive(true);
            _text.gameObject.SetActive(true);
            _authorizeButton.interactable = true;
        }
    }

    private void Show()
    {
        ClearContend();
        _resizer.Resize(0.5f, _normalScale);

        if (PlayerAccount.IsAuthorized)
        {
            if (PlayerAccount.HasPersonalProfileDataPermission)
            {
                Leaderboard.SetScore(ResourceLeaderBoard, _resourceCounter.Value);
            }

            Leaderboard.GetEntries(ResourceLeaderBoard, (result) =>
            {
                foreach (var entrie in result.entries)
                {
                    LeaderBoardPlayerInfo playerInfo = Instantiate(_playerInfoPrefab, _contend);
                    string name = entrie.player.publicName;
                    if (string.IsNullOrEmpty(name))
                        name = "Anonym";
                    playerInfo.SetInfo(name, entrie.score.ToString());
                }
            });
        }
        else
        {
            ClearContend();
            LeaderBoardPlayerInfo playerInfo = Instantiate(_playerInfoPrefab, _contend);
            // playerInfo.SetInfo("Вы", "не авторизованы");
        }
    }

    public void OnAuthorizeButtonClick()
    {
        PlayerAccount.Authorize();
    }

    public void OnClosetButtonClick()
    {
        _canvasGroup.interactable = true;
        _resizer.Resize(0.5f, 0f);
    }

    private void ClearContend()
    {
        if (_contend.childCount > 0)
        {
            foreach (Transform child in _contend)
            {
                Destroy(child.gameObject);
            }
        }
    }
}