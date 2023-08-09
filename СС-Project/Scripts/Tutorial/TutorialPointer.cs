using System;
using UnityEngine;

public class TutorialPointer : MonoBehaviour
{
    public event Action StepDone;

    protected void OnStepDone()
    {
        StepDone?.Invoke();
    }

    protected void OnStepDone(Player player)
    {
        StepDone?.Invoke();
    }
}
