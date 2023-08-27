using UnityEngine;

public class AimController : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        Cursor.visible = false;
    }

    private void Update()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = -_camera.transform.position.z;

        transform.position = _camera.ScreenToWorldPoint(screenPoint);
    }
}