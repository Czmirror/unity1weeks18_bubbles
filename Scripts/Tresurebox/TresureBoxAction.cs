using Bubbles.Scripts.Interface;
using UnityEngine;

namespace Bubbles.Scripts.Tresurebox
{
    public class TresureBoxAction : MonoBehaviour, IPickable
    {
        [SerializeField] private TresureboxStatus tresureboxStatus;

        private void Start()
        {
            tresureboxStatus = gameObject.GetComponent<TresureboxStatus>();
        }
        
        // 通常時（宝箱を手放すなど）
        public void Normal()
        {
            tresureboxStatus.TresureboxNormal();
        }
        
        // 宝箱を持っている状態
        public void Behold()
        {
            tresureboxStatus.TresureboxBehold();
        }
        
        // 宝箱回収
        public void Recovery()
        {
            tresureboxStatus.TresureboxRecoverly();
        }
        
    }
}
