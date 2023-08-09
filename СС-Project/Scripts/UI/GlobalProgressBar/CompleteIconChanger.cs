using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteIconChanger : MonoBehaviour
{
    [SerializeField] private Image _handle;
    [SerializeField] private Image _done;
    private List<Image> _currentIcons = new List<Image>();

    public void AddIcon(Image image) 
    {
        _currentIcons.Add(image);
    }

    public void TryChangeIcons()
    {
        for(int i = 0; i < _currentIcons.Count; i++)
        {
            if (_handle.transform.position.x > _currentIcons[i].transform.position.x)
                _currentIcons[i].sprite = _done.sprite;
        }
    }
}
