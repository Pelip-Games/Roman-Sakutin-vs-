using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int _amount;

    public int Amount => _amount;
}