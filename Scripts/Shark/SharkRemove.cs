using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bubbles.Scripts.Shark
{
    public class SharkRemove : MonoBehaviour
    {
        [SerializeField]
        private int delete_position_x;

        void Start()
        {
            this.UpdateAsObservable()
                .Where(_ =>
                    this.gameObject.transform.position.x <= delete_position_x
                )
                .Subscribe(_ => Remove());
        }

        private void Remove()
        {
            Destroy(gameObject);
        }
    }

}
