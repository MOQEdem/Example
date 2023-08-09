using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private bool _isTaken = false;

    public bool IsTaken => _isTaken;

    public void Take()
    {
        _isTaken = true;
    }
}
