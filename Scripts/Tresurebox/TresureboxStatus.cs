using UnityEngine;
using UniRx;

namespace Bubbles.Scripts.Tresurebox
{
    // 状態の種類のenum
    public enum TresureboxState
    {
        Normal,
        Behold,
        Recovery
    }
    
    // 独自UniRxステータス
    [SerializeField]
    public class TresureboxStateReactiveProperty : ReactiveProperty<TresureboxState>
    {
        public TresureboxStateReactiveProperty(){}
        public TresureboxStateReactiveProperty(TresureboxState init) : base(init){}
    }
    
    // インスペクタウィンドウ表示のためのエディタ拡張を定義
//    [UnityEditor.CustomPropertyDrawer(typeof(TresureboxStateReactiveProperty))]
//    public class ExtendInspectorDisplayDrawer : InspectorDisplayDrawer{}
    
    // プレイヤー状態
    public class TresureboxStatus : MonoBehaviour
    {
        public TresureboxState currentState
        {
            get { return tresureboxState.Value; }
        }
        
        public TresureboxStateReactiveProperty tresureboxState = new TresureboxStateReactiveProperty();

        public void TresureboxNormal()
        {
            tresureboxState.Value = TresureboxState.Normal;
        }

        public void TresureboxBehold()
        {
            tresureboxState.Value = TresureboxState.Behold;
        }
        
        public void TresureboxRecoverly()
        {
            tresureboxState.Value = TresureboxState.Recovery;
        }
    }
}
