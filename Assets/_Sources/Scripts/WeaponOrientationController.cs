using System;
using UnityEngine;

namespace _Sources.Scripts
{
    public class WeaponOrientationController : MonoBehaviour
    {
        [SerializeField] private Transform _aim;

        private void Update()
        {
            Vector3 newDirection = (_aim.position - transform.position).normalized;
            transform.right = newDirection;
        }
    }
}