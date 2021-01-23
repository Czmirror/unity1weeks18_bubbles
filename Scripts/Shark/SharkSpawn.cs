using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UniRx;

namespace Bubbles.Scripts.Shark
{
    public class SharkSpawn : MonoBehaviour
    {
        [SerializeField] private int spawnTime;
        [SerializeField] private GameObject sharkObject;

        private void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(spawnTime)).Subscribe(_ => { Spawn(); }).AddTo(this);
        }
        
        private void Spawn()
        {
            var _position = new Vector3(12, Random.Range(-1.0f, 3.0f), 0);
            var shark = Instantiate(sharkObject, _position, new Quaternion(0, 0, 0,0));
        }
    }
}
