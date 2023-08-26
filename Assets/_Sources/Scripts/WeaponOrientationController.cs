using System;
using UnityEngine;

namespace _Sources.Scripts
{
    public class WeaponOrientationController : MonoBehaviour
    {
        [SerializeField] private Transform _aim;

        private void Update()
        {
            transform.LookAt(_aim);
        }
    }
}