using System;
using TMPro;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _lifeTime;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private TMP_Text _text;

    public event Action Destroyed;

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    public void Init(string phrase)
    {
        _text.text = phrase;
        transform.localPosition = _offset;
        
        Destroy(gameObject, _lifeTime);
    }
}