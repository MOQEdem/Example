using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private void Awake() => 
        transform.parent = null;
}
