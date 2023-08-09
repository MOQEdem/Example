using UnityEngine;
using UnityEngine.UI;

public class StartCameraProgressPoint : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _doneSprite;
    [SerializeField] private Sprite _readySprite;

    public Image Image => _image;

    public void SetSprite(bool isDone)
    {
        _image.sprite = isDone ? _doneSprite : _readySprite;
    }
}
