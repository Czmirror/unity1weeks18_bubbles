using Bubbles.Scripts.Manager;
using UnityEngine;
using UniRx;

namespace Bubbles.Scripts.UI
{
    public class PresenterGameOver : MonoBehaviour
    {
        [SerializeField] private GameObject gameStatusObject;
        
        private void Start()
        {
            if (!gameStatusObject)
            {
                gameStatusObject = GameObject.Find("Managers/GameStatus");
            }

            var gameStatus = gameStatusObject.GetComponent<GameStatus>();
            gameStatus.gameState
                .Where(x =>
                    x == GameState.GameOver)
                .Subscribe(_ => RefreshUI());
            
            gameObject.SetActive(false);
        }
        
        void RefreshUI()
        {
            gameObject.SetActive(true);
        }
    }
}
