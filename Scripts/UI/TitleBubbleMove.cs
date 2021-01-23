using System;
using UnityEngine;
using DG.Tweening;
using UniRx;
using Random = UnityEngine.Random;

namespace Bubbles.Scripts.UI
{
    public class TitleBubbleMove : MonoBehaviour
    {
        [SerializeField] private int randomTime;
        
        private void Start()
        {
//            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
//            gameObject.transform.DOMove(new Vector3(-12, 0, 0), 1).SetRelative().SetLoops(-1, LoopType.Yoyo);
            
            Observable.Interval(TimeSpan.FromSeconds(randomTime)).Subscribe(_ => { RandomMove(); }).AddTo(this);
        }

        // 泡の発生源をランダムで移動させる
        private void RandomMove()
        {
            var x = gameObject.transform.position.x;
            var y = gameObject.transform.position.y;
            var z = gameObject.transform.position.z;

            x = Random.Range(-10, 10);
            y = Random.Range(-5, -10);
            z = Random.Range(1, 15);
            
            gameObject.transform.position = new Vector3(x,y,z);
        }
    }
}
