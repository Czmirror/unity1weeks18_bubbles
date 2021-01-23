using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bubbles.Scripts.Shark
{
    public class SharkMove : MonoBehaviour
    {
        public float moveSpeed; // 現在のスピード
        private void Start()
        {
            // UniRX 移動処理を実施
            this.UpdateAsObservable()
                .Subscribe(_ => Move());
        }

        void Move()
        {
            var x = -1 * moveSpeed;
            var direction = new Vector3(x, 0, 0);
            transform.position += new Vector3(x * Time.deltaTime, 0, 0);
        }
    }
}
