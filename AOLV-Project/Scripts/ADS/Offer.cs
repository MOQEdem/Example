using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core;
using UnityEngine;

public class Offer : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    private void OnEnable()
    {
        _timer.TimerStoped += Off;
    }

    private void OnDisable()
    {
        _timer.TimerStoped -= Off;
    }

    private void Off()
    {
        gameObject.SetActive(false);
    }
}