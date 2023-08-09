using UnityEngine;
using Agava.YandexGames;

public class AuthorizationButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        PlayerAccount.Authorize();
    }
}
