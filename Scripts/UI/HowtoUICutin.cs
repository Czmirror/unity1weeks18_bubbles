using UnityEngine;
using DG.Tweening;

namespace Bubbles.Scripts.UI
{
    public class HowtoUICutin : MonoBehaviour
    {

        [SerializeField] private RectTransform panelRectTransform;
        [SerializeField] private RectTransform cutOutpanelRectTransform;
        
        [SerializeField] private float cutInPositionInitX = 1000;
        [SerializeField] private float cutOutPositionTargetX = -1000;

        private void Start()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        }

        public void PushButton()
        {
            CutOutUI();
            CutInUI();
        }
        
        public void CutInUI()
        {
            if (!panelRectTransform)
            {
                return;
            }
            panelRectTransform.anchoredPosition = new Vector2(cutInPositionInitX,0);
            panelRectTransform.DOLocalMoveX(0f, 1f).SetEase(Ease.OutBounce);
        }

        public void CutOutUI()
        {
            if (!cutOutpanelRectTransform)
            {
                return;
            }
            cutOutpanelRectTransform.DOLocalMoveX(cutOutPositionTargetX, 1f).SetEase(Ease.OutBounce);
        }
    }
}
