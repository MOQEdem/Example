using UnityEngine;
using Agava.WebUtility;

public class BackgroundPause : MonoBehaviour
{
    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        Time.timeScale = inBackground ? 0 : 1;
    }
}
