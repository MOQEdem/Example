using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardPlayerInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;

    public void SetInfo(string name, string score)
    {
        _name.text = name;
        _score.text = score;
    }
}
