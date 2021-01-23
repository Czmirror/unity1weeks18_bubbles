using System;
using Bubbles.Scripts.Manager;
using UnityEngine;
using UniRx;

namespace Bubbles.Scripts.Player
{
    // 状態の種類のenum
    public enum PlayerState
    {
        Idle,
        Dead,
        Carry,
        Floating
    }

    // 独自UniRxステータス
    [SerializeField]
    public class PlayerStateReactiveProperty : ReactiveProperty<PlayerState>
    {
        public PlayerStateReactiveProperty(){}
        public PlayerStateReactiveProperty(PlayerState init) : base(init){}
    }
    
    // インスペクタウィンドウ表示のためのエディタ拡張を定義
//    [UnityEditor.CustomPropertyDrawer(typeof(PlayerStateReactiveProperty))]
//    public class ExtendInspectorDisplayDrawer : InspectorDisplayDrawer{}
    
    // プレイヤー状態
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private PlayerCatch playerCatch;
        [SerializeField] private PlayerFloating playerFloating;
        [SerializeField] private GameObject gameStatusObject;
        public PlayerState currentState
        {
            get { return playerState.Value; }
        }
        
        public PlayerStateReactiveProperty playerState = new PlayerStateReactiveProperty();
        
        private void Start()
        {
            playerCatch = gameObject.GetComponent<PlayerCatch>();
            playerFloating = gameObject.GetComponent<PlayerFloating>();
            
            playerCatch.isCatch
                .Subscribe(_ => PlayerChangeStatus());
            playerFloating.isFloating
                .Subscribe(_ => PlayerChangeStatus());

            if (!gameStatusObject)
            {
                gameStatusObject = GameObject.Find("Managers/GameStatus");
            }

        }

        private void PlayerChangeStatus()
        {
            if (playerState.Value == PlayerState.Dead)
            {
                PlayerDead();
                return;
            }
            
            // プレイヤーが泡に触れている
            if (playerFloating.isFloating.Value == true)
            {
                PlayerFloating();
                return;
            }
            
            // プレイヤーがものを持つ
            if (playerCatch.isCatch.Value == true)
            {
                PlayerCarry();
                return;
            }
            
            // それ以外
            PlayerIdle();

        }
        
        public void PlayerIdle()
        {
            playerState.Value = PlayerState.Idle;
        }

        public void PlayerDead()
        {
            // クリア時は無敵
            var gameStatus = gameStatusObject.GetComponent<GameStatus>();
            if (gameStatus.gameState.Value == GameState.GameClear)
            {
                return;
            }
            
            playerState.Value = PlayerState.Dead;
        }
        
        public void PlayerCarry()
        {
            playerState.Value = PlayerState.Carry;
        }
        
        public void PlayerFloating()
        {
            playerState.Value = PlayerState.Floating;
        }
    }
}
