using UnityEngine;

public class CameraCoordinatesGrid : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void UpdateGrid()
    {
        transform.rotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
    }
}
