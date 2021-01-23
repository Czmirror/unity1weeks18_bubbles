using System.Collections;
using System.Collections.Generic;
using Bubbles.Scripts.Interface;
using UnityEngine;

namespace Bubbles.Scripts.Shark
{
    public class SharkAttack : MonoBehaviour, IAttackable
    {   
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<IDamagable>() is var damage && damage != null)
            {
                Attack(damage);
            }
        }

        public void Attack(IDamagable damage)
        {
            damage.AddDamage();
        }
    }
}
