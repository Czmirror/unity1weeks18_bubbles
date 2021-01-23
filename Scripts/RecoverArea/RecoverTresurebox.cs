using Bubbles.Scripts.Interface;
using UnityEngine;

namespace Bubbles.Scripts.RecoverArea
{
    public class RecoverTresurebox : MonoBehaviour, IRecoverable
    {
        [SerializeField] private IPickable targetTresurebox;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<IPickable>() is var tresurebox && tresurebox != null)
            {
                targetTresurebox = tresurebox;
                Recover();
            }
        }

        public void Recover()
        {
            targetTresurebox.Recovery();
        }
    }
}
