using System;
using UnityEngine;

namespace _Sources.Scripts
{
    public class WeaponOrientationController : MonoBehaviour
    {
        [SerializeField] private Transform _aim;

        private void Update()
        {
            var angle = Mathf.Atan2(_aim.position.y, _aim.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}