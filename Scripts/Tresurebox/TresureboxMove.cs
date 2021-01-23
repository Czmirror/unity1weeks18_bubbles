using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bubbles.Scripts.Tresurebox
{
    public class TresureboxMove : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float[] clamp;
        
        private void Start()
        {
            player = GameObject.Find("Player");
            
            var tresureboxStatus = this.GetComponent<TresureboxStatus>();
            
            this.UpdateAsObservable()
                .Where(_ =>
                    tresureboxStatus.tresureboxState.Value == TresureboxState.Normal
                )
                .Subscribe(_ => Normal());
            
            this.UpdateAsObservable()
                .Where(_ =>
                    tresureboxStatus.tresureboxState.Value == TresureboxState.Behold
                )
                .Subscribe(_ => Behold());
            
            this.UpdateAsObservable()
                .Where(_ =>
                    tresureboxStatus.tresureboxState.Value == TresureboxState.Recovery
                )
                .Subscribe(_ => Recovery());
        }
        
        // 移動可能範囲
        void Clamp()
        {
            var player_pos_x = Mathf.Clamp(transform.position.x, clamp[0], clamp[1]);
            var player_pos_y = Mathf.Clamp(transform.position.y, clamp[2], clamp[3]);
            transform.position = new Vector3(player_pos_x, player_pos_y, transform.position.z);
        }

        void Normal()
        {
            var y = -1;
            var direction = new Vector3(0, y, 0);
            transform.position += new Vector3(0, y * Time.deltaTime, 0);
            Clamp();
        }
        void Behold()
        {
            gameObject.transform.position = player.transform.position;
        }
        
        void Recovery()
        {
            // 宝箱をキャプチャーポイントまで移動させる
            this.transform.DOMove(new Vector3(0,10,0), 3f);
        }
    }
}
