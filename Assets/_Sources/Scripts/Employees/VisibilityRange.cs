using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VisibilityRange : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 5;
    [SerializeField] private float _viewAngle = 45;
    [SerializeField] private Image _circleImage;

    private Vector2 _direction = Vector2.up;
    private bool _isInitialized = false;

    public void SetDirection(Vector2 newDirection)
    {
        if (newDirection.magnitude > 0)
        {
            _direction = newDirection;
        }
        UpdateState();
    }

    public void Initialize(float maxDistance, float viewAngle, Vector2 initialDirection)
    {
        if (maxDistance > 0)
        {
            _maxDistance = maxDistance;
        }
        if (viewAngle > 0)
        {
            _viewAngle = viewAngle;
        }
        if (initialDirection.magnitude > 0)
        {
            _direction = initialDirection;
        }

        _circleImage.fillAmount = _viewAngle / 360;

        var canvasRectTransform = gameObject.GetComponent<RectTransform>();
        canvasRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxDistance);
        canvasRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _maxDistance);

        _isInitialized = true;
        UpdateState();
    }

    private void Start()
    {
        Initialize(_maxDistance, _viewAngle, _direction);
    }

    private void UpdateState()
    {
        if (_circleImage != null && _isInitialized)
        {
            float rotationAngle = Quaternion.FromToRotation(Vector3.up, _direction).eulerAngles.y - _viewAngle / 2;
            _circleImage.transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction) * Quaternion.AngleAxis(-_viewAngle / 2, Vector3.back);
        }
    }

    private void Update()
    {
        //UpdateState();
    }
}
