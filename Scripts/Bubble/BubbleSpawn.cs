using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bubbles.Scripts.Bubble
{
    public class BubbleSpawn : MonoBehaviour
    {
        [SerializeField] private int spawnTime;
        [SerializeField] private GameObject bubbleObject;
    
        void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(spawnTime)).Subscribe(_ => { Spawn(); }).AddTo(this);
        }

        private void Spawn()
        {
            var _position = this.transform.position;
            var bubble = Instantiate(bubbleObject, _position, new Quaternion(0, 0, 0,0));
        }
    }
}

