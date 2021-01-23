using Bubbles.Scripts.Interface;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bubbles.Scripts.Player
{
    public class PlayerCatch : MonoBehaviour, ICatchable
    {
        [SerializeField] private PlayerStatus playerStatus;
        
        [SerializeField] private IPickable targetTresurebox;
        public ReactiveProperty<bool> isCatch = new ReactiveProperty<bool>(false);
        
        void Start()
        {
            playerStatus = gameObject.GetComponent<PlayerStatus>();
            
            // 宝箱キャッチ
            this.UpdateAsObservable()
                .Where(_ =>
                    (
                        (Input.GetKeyUp("space")) 
                    ) &&
                    playerStatus.playerState.Value == PlayerState.Idle &&
                    isCatch.Value == false &&
                    targetTresurebox != null
                )
                .Subscribe(_ => Catch());
            
            // 宝箱リリース
            this.UpdateAsObservable()
                .Where(_ =>
                    (
                        (Input.GetKeyDown("space")) 
                    ) &&
                    playerStatus.playerState.Value == PlayerState.Carry &&
                    isCatch.Value == true &&
                    targetTresurebox != null
                )
                .Subscribe(_ => Release());
        }
        
        
        void OnTriggerEnter2D(Collider2D other)
        {
            // 宝箱キャッチ
            if (other.gameObject.GetComponent<IPickable>() is var tresurebox && tresurebox != null)
            {
                if (isCatch.Value == true)
                {
                    return;
                }
                
                targetTresurebox = tresurebox;
            }
            
            // 宝箱回収
            if (other.gameObject.GetComponent<IRecoverable>() is var recoverArea && recoverArea != null)
            {
                if (isCatch.Value == false && tresurebox == null)
                {
                    return;
                }
                
//                targetTresurebox.Recovery();
                targetTresurebox = null;
                isCatch.Value = false;
            }
            
        }

        public void Catch()
        {
            targetTresurebox.Behold();
            isCatch.Value = true;
        }

        public void Release()
        {
            targetTresurebox.Normal();
            targetTresurebox = null;
            
            isCatch.Value = false;
        }
    }
}
