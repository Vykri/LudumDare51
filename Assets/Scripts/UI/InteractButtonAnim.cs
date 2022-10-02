using DG.Tweening;
using UnityEngine;

public class InteractButtonAnim : MonoBehaviour {

    private void Start() {
        RectTransform rect = GetComponent<RectTransform>();
        DOTween.Sequence()
            .Append(rect.DOAnchorPosY(0, 0.25f).SetEase(Ease.Linear))
            .AppendInterval(0.25f)
            .Append(rect.DOAnchorPosY(4, 0.25f).SetEase(Ease.Linear))
            .AppendInterval(1f)
            .SetLoops(-1);
    }
}
