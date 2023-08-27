using UnityEngine;
using UnityEngine.UI;

public class VisibilityRange : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 5;
    [SerializeField] private float _viewAngle = 45;
    [SerializeField] private Image _circleImage;

    private Vector2 _direction = Vector2.up;
    private bool _isInitialized = false;

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

        RectTransform canvasRectTransform = gameObject.GetComponent<RectTransform>();
        canvasRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxDistance * 2f);
        canvasRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _maxDistance * 2f);

        _isInitialized = true;
        UpdateState();
    }

    public void SetDirection(Vector2 newDirection)
    {
        if (newDirection.magnitude > 0)
        {
            _direction = newDirection;
        }
        
        UpdateState();
    }

    private void UpdateState()
    {
        _circleImage.transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction) * Quaternion.AngleAxis(-_viewAngle / 2, Vector3.back);
    }
}
