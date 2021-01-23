using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bubbles.Scripts.Bubble
{
    public class BubbleDelete : MonoBehaviour
    {
        [SerializeField]
        private int delete_position_y;
    
        void Start()
        {
            this.UpdateAsObservable()
                .Where(_ =>
                    this.gameObject.transform.position.y >= delete_position_y
                )
                .Subscribe(_ => Delete());
        }

        // Update is called once per frame
        private void Delete()
        {
            Destroy(gameObject);
        }
    }

}

