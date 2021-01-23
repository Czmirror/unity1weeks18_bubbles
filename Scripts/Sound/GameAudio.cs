using Bubbles.Scripts.Manager;
using UnityEngine;
using UniRx;

namespace Bubbles.Scripts.Sound
{
    
    public class GameAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioNormal;
        [SerializeField] private AudioClip _audioGameOver;
        [SerializeField] private AudioClip _audioGameClear;
        
        [SerializeField] private GameObject gameStatusObject;
        
        private void Start()
        {
            if (!_audioSource)
            {
                _audioSource = GetComponent<AudioSource>();
            }
            
            if (!gameStatusObject)
            {
                gameStatusObject = GameObject.Find("Managers/GameStatus");
            }

            var gameStatus = gameStatusObject.GetComponent<GameStatus>();
            
            gameStatus.gameState
                .Where(x =>
                    x == GameState.Normal)
                .Subscribe(_ => Normal());
            
            gameStatus.gameState
                .Where(x =>
                    x == GameState.GameOver)
                .Subscribe(_ => GameOver());
            
            gameStatus.gameState
                .Where(x =>
                    x == GameState.GameClear)
                .Subscribe(_ => GameClear());
            
        }

        private void Normal()
        {
            _audioSource.clip = _audioNormal;
            _audioSource.Play();
        }
        
        private void GameOver()
        {
            _audioSource.clip = _audioGameOver;
            _audioSource.Play();
        }
        
        private void GameClear()
        {
            _audioSource.clip = _audioGameClear;
            _audioSource.Play();
        }
        
    }
}
