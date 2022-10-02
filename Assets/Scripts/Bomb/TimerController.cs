using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerController : MonoBehaviour {

    [SerializeField] private BombController bomb;
    [SerializeField] private UnityEvent onWin;
    [SerializeField] private UnityEvent onLose;

    public bool countingDown = false;

    private TextMeshPro timerText;

    private float timer = 10f;
    private bool gameOver = false;

    private void Awake() {
        timerText = GetComponent<TextMeshPro>();
    }

    private void Update() {
        if (countingDown) {
            float prevTimer = timer;
            timer = Mathf.Clamp(timer - Time.deltaTime, 0f, 10f);
            timerText.text = $"{(int)timer}.{((int)(timer % 1 * 1000)).ToString().PadLeft(3, '0')}";
            if (prevTimer > 1f && timer <= 1f) {
                bomb.PlayExplodingWarning();
            }
            if (timer == 0f) {
                TimeUp();
            }
        }
    }

    public void ResetTimer() {
        if (!gameOver) {
            countingDown = true;
            timer = 10f;
            bomb.StopExplodingWarning();
        }
    }

    private void TimeUp() {
        countingDown = false;
        gameOver = true;
        if (bomb.pressed) {
            onWin?.Invoke();
        } else {
            onLose?.Invoke();
        }
    }
}