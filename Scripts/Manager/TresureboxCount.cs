using Bubbles.Scripts.Tresurebox;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace Bubbles.Scripts.Manager
{
    public class TresureboxCount : MonoBehaviour
    {
        public IntReactiveProperty TotalTresurebox = new IntReactiveProperty(0);
        public IntReactiveProperty CurrentTresurebox = new IntReactiveProperty(0);
        
        private void Start()
        {
            var tresureboxObjects = GameObject.FindGameObjectsWithTag ("Tresurebox");
            TotalTresurebox.Value = tresureboxObjects.Length;

            foreach (var tresurebox in tresureboxObjects)
            {
                tresurebox.GetComponent<TresureboxStatus>().tresureboxState
                    .Where(
                        x => x == TresureboxState.Recovery
                    )
                    .Subscribe(
                        _ => Countup()
                    );
            }
        }

        private void Countup()
        {
            CurrentTresurebox.Value++;
        }
    }
}
