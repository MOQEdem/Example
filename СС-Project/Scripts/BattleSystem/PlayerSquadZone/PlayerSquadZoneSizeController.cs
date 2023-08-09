using UnityEngine;

public class PlayerSquadZoneSizeController : MonoBehaviour
{
    [SerializeField] private float _stepChangeSize;
    [SerializeField] private int _countPointOneLine = 9;
    [SerializeField] private float _offSet = 1f;

    private RectTransform _rectTransform;
    private float _baseSizeX;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _baseSizeX = _rectTransform.rect.width;
    }

    public void SetSize(int capacity)
    {
        int countRows;

        if ((float)capacity / _countPointOneLine > capacity / _countPointOneLine)
            countRows = (capacity / _countPointOneLine) + 1;
        else
            countRows = capacity / _countPointOneLine;

        if (countRows == 1)
        {
            float countColumn = (capacity % _countPointOneLine);
            float sizeX;

            if (countColumn != 0)
            {
                sizeX = countColumn * _stepChangeSize + _offSet;
            }
            else
            {
                sizeX = _baseSizeX;
            }

            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeX);
        }
        else
        {
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _baseSizeX);
        }

        if (countRows > 0)
        {
            float sizeY;

            sizeY = (countRows * _stepChangeSize);

            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeY);
        }
        else
        {
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        }
    }
}
