using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public enum TurnState {
    FORWARD,
    TURNING,
    BACKWARD,
}

public class BombController : MonoBehaviour {

    [SerializeField] private float origZ = -0.00274698f;
    [SerializeField] private float pressedZ = -0.05f;
    [SerializeField] private UnityEvent onTurnWin;
    [SerializeField] private UnityEvent onTurnFail;

    public bool pressed = false;

    private AudioSource audioSource;
    private Transform button;
    private Tween downTween;
    private TurnState turnState = TurnState.FORWARD;
    private float turnTimer = 0f;

    private void Awake() {
        button = transform.Find("Cylinder");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        turnTimer -= Time.deltaTime;
    }

    public void PressButton() {
        pressed = true;
        downTween = button.DOLocalMoveZ(pressedZ, 0.25f);
    }

    public void ReleaseButton() {
        if (downTween != null && downTween.IsActive()) {
            downTween.OnComplete(() => {
                pressed = false;
                button.DOLocalMoveZ(origZ, 0.15f);
            });
        } else {
            pressed = false;
            button.DOLocalMoveZ(origZ, 0.15f);
        }
    }

    public void TurnAround() {
        switch (turnState) {
            case TurnState.FORWARD:
                transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0.25f, RotateMode.LocalAxisAdd).OnComplete(() => turnState = TurnState.BACKWARD);
                turnTimer = 6f;
                turnState = TurnState.TURNING;
                break;
            case TurnState.BACKWARD:
                transform.DOLocalRotate(new Vector3(0f, -180f, 0f), 0.25f, RotateMode.LocalAxisAdd).OnComplete(() => turnState = TurnState.FORWARD);
                if (turnTimer < 0f) {
                    onTurnWin?.Invoke();
                } else {
                    onTurnFail?.Invoke();
                }
                turnState = TurnState.TURNING;
                break;
        }
    }

    public void PlayExplodingWarning() {
        audioSource.Play();
    }

    public void StopExplodingWarning() {
        audioSource.Stop();
    }
}
