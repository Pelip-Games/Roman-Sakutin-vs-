using TMPro;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _lifeTime;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private TMP_Text _text;

    private Transform _owner;

    private void Update()
    {
        if (transform == null)
            return;
        
        transform.position = _owner.position + (Vector3)_offset;
    }
    
    public void Init(string phrase, Transform owner)
    {
        _text.text = phrase;
        _owner = owner;
        
        Destroy(gameObject, _lifeTime);
    }
}