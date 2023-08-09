using UnityEngine;
using DG.Tweening;

public class RotateTest : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(transform.rotation.eulerAngles + transform.up * 90, 3);
    }
}
