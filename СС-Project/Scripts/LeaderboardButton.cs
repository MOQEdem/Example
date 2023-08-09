using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
    private void Awake()
    {
#if !YANDEX_GAMES && !TEST
        this.gameObject.SetActive(false);
#endif
    }
}
