using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using UniRx;

namespace Bubbles.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        PlayableGraph graph;
        private AnimationClipPlayable currentClipPlayable;
        private AnimationPlayableOutput playableOutput;
        
        [SerializeField] AnimationClip animationClipIdle;
        [SerializeField] AnimationClip animationClipDead;

        public PlayerStateReactiveProperty playerStatus;

        void Awake()
        {
            graph = PlayableGraph.Create();
        }

        private void Start()
        {
            // outputを生成して、出力先を自身のAnimatorに設定
            playableOutput = AnimationPlayableOutput.Create(graph, "output", GetComponent<Animator>());
            
            PlayerAnimataionIdle();
            
            // 主人公のステータス監視用
            var playerStatus = GetComponent<PlayerStatus>();

            // 主人公やられアニメーション監視
            playerStatus.playerState.Where(x =>
                    x == PlayerState.Dead
                )
                .Subscribe(_ => PlayerAnimationDead());
        }

        // アニメーション実行
        private void PlayAnimation()
        {
            // playableをoutputに流し込む
            playableOutput.SetSourcePlayable(currentClipPlayable);
            graph.Play();
        }

        // 主人公通常時アニメーション
        private void PlayerAnimataionIdle()
        {
            currentClipPlayable = AnimationClipPlayable.Create(graph, animationClipIdle);
            PlayAnimation();
        }

        // 主人公やられアニメーション
        private void PlayerAnimationDead()
        {
            currentClipPlayable = AnimationClipPlayable.Create(graph, animationClipDead);
            PlayAnimation();
        }
    }
}
