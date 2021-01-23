using Bubbles.Scripts.Interface;
using UnityEngine;

namespace Bubbles.Scripts.Player
{
    public class PlayerDamage : MonoBehaviour, IDamagable
    {
        [SerializeField] private PlayerStatus playerStatus;

        void Start()
        {
            playerStatus = gameObject.GetComponent<PlayerStatus>();
        }
        public void AddDamage()
        {
            playerStatus.PlayerDead();
        }
    }
}
