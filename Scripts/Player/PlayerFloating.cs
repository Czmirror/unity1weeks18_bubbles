using Bubbles.Scripts.Interface;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bubbles.Scripts.Player
{
    public class PlayerFloating : MonoBehaviour
    {
        [SerializeField] private PlayerStatus playerStatus;

        private IPickable targetTresurebox;
        public ReactiveProperty<bool> isFloating = new ReactiveProperty<bool>(false);

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<IFloatable>() is var bubble && bubble != null)
            {
                isFloating.Value = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<IFloatable>() is var bubble && bubble != null)
            {
                isFloating.Value = false;
            }
        }
    }
}
