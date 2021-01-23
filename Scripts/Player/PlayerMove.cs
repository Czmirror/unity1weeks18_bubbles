using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bubbles.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        public float moveSpeed; // 現在のスピード

        [SerializeField] private float[] clamp;

        private void Start()
        {
            var playerStatus = this.GetComponent<PlayerStatus>();

            // 通常
            this.UpdateAsObservable()
                .Where(_ =>
                    (
                        (Input.GetAxis("Horizontal") != 0) ||
                        (Input.GetAxis("Vertical") != 0)
                    ) &&
                    playerStatus.playerState.Value == PlayerState.Idle
                )
                .Subscribe(_ => Move());

            // 宝箱運搬
            this.UpdateAsObservable()
                .Where(_ =>
                    (
                        (Input.GetAxis("Horizontal") != 0)
                    ) &&
                    playerStatus.playerState.Value == PlayerState.Carry
                )
                .Subscribe(_ => Carry());

            // 宝箱運搬（自然落下）
            this.UpdateAsObservable()
                .Where(_ =>
                    playerStatus.playerState.Value == PlayerState.Carry
                )
                .Subscribe(_ => Fall());

            // 泡接触＆動き
            this.UpdateAsObservable()
                .Where(_ =>
                    (
                        (Input.GetAxis("Horizontal") != 0) ||
                        (Input.GetAxis("Vertical") != 0)
                    ) &&
                    playerStatus.playerState.Value == PlayerState.Floating
                )
                .Subscribe(_ => FloatingMove());
            
            // 泡接触
            this.UpdateAsObservable()
                .Where(_ =>
                    playerStatus.playerState.Value == PlayerState.Floating
                )
                .Subscribe(_ => Floating());

            // やられ
            this.UpdateAsObservable()
                .Where(_ =>
                    playerStatus.playerState.Value == PlayerState.Dead
                )
                .Subscribe(_ => Dead());
        }

        // 通常時の動き
        void Move()
        {
            var x = Input.GetAxis("Horizontal") * moveSpeed;
            var y = Input.GetAxis("Vertical") * moveSpeed;

            if (x != 0 || y != 0)
            {
                var direction = new Vector3(x, y, 0);
                transform.position += new Vector3(x * Time.deltaTime, y * Time.deltaTime, 0);
                Clamp();
            }
        }

        // 運搬時の動き
        void Carry()
        {
            var x = Input.GetAxis("Horizontal") * moveSpeed;

            if (x != 0)
            {
                var direction = new Vector3(x, 0, 0);
                transform.position += new Vector3(x * Time.deltaTime, 0, 0);
                Clamp();
            }
        }

        // 運搬時の動き（自然落下）
        void Fall()
        {
            var y = -1;
            var direction = new Vector3(0, y, 0);
            transform.position += new Vector3(0, y * Time.deltaTime, 0);
            Clamp();
        }

        // 泡接触＆動き
        void FloatingMove()
        {
            var x = Input.GetAxis("Horizontal") * moveSpeed;

            if (x != 0)
            {
                var direction = new Vector3(x, 0, 0);
                transform.position += new Vector3(x * Time.deltaTime, 0, 0);
                Clamp();
            }
        }

        // 泡接触
        void Floating()
        {
            var y = +1;

            var direction = new Vector3(0, y, 0);
            transform.position += new Vector3(0, y * Time.deltaTime, 0);
            Clamp();
        }

        // 主人公やられ時の動き
        void Dead()
        {
            var y = -1;
            var direction = new Vector3(0, y, 0);
            transform.position += new Vector3(0, y * Time.deltaTime, 0);
            Clamp();
        }

        // 主人公移動可能範囲への維持
        void Clamp()
        {
            var player_pos_x = Mathf.Clamp(transform.position.x, clamp[0], clamp[1]);
            var player_pos_y = Mathf.Clamp(transform.position.y, clamp[2], clamp[3]);
            transform.position = new Vector3(player_pos_x, player_pos_y, transform.position.z);
        }
    }
}
