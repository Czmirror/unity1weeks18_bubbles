using UnityEngine;
using UniRx;
using Bubbles.Scripts.Player;

namespace Bubbles.Scripts.Manager
{
    // 状態の種類のenum
    public enum GameState
    {
        Normal,
        GameOver,
        GameClear
    }

    // 独自UniRxステータス
    [SerializeField]
    public class GameStateReactiveProperty : ReactiveProperty<GameState>
    {
        public GameStateReactiveProperty()
        {
        }

        public GameStateReactiveProperty(GameState init) : base(init)
        {
        }
    }

    // インスペクタウィンドウ表示のためのエディタ拡張を定義
//    [UnityEditor.CustomPropertyDrawer(typeof(GameStateReactiveProperty))]
//    public class ExtendInspectorDisplayDrawer : InspectorDisplayDrawer
//    {
//    }

    public class GameStatus : MonoBehaviour
    {
        public GameState currentState
        {
            get { return gameState.Value; }
        }

        public GameStateReactiveProperty gameState = new GameStateReactiveProperty();

        public void Normal()
        {
            gameState.Value = GameState.Normal;
        }

        public void GameOver()
        {
            gameState.Value = GameState.GameOver;
        }

        public void GameClear()
        {
            gameState.Value = GameState.GameClear;
        }


        [SerializeField] private GameObject player;
        [SerializeField] private GameObject tresureboxCount;

        private void Start()
        {
            Normal();

            if (!player)
            {
                player = GameObject.Find("Player");
            }

            var playerStatus = player.GetComponent<PlayerStatus>();
            playerStatus.playerState
                .Where(x =>
                    x == PlayerState.Dead
                )
                .Subscribe(_ => GameOver());

            if (!tresureboxCount)
            {
                tresureboxCount = GameObject.Find("Managers/TresureboxCount");
            }

            var tresureboxCount_Component = tresureboxCount.GetComponent<TresureboxCount>();
            tresureboxCount_Component.CurrentTresurebox
                .Where(x =>
                    x > 0 &&
                    x == tresureboxCount_Component.TotalTresurebox.Value
                )
                .Subscribe(_ => GameClear());
        }
    }
}
