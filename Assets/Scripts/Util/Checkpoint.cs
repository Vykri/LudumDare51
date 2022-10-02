using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour {

    public static int checkpoint = 0;

    [SerializeField] private List<PlayableDirector> timelines;
    [SerializeField] private Image fade;
    [SerializeField] private TimerController timer;
    [SerializeField] private Interactable buttonInteractable;
    [SerializeField] private Transform receiver;
    [SerializeField] private Interactable pourCupInteractable;
    [SerializeField] private OutroManager outroManager;

    public void SetCheckpoint(int checkpoint) {
        Checkpoint.checkpoint = checkpoint;
    }

    public void StartGame() {
        fade.DOFade(0f, 2f);
        timelines[checkpoint].Play();
        StartCoroutine(DelayEnablingBomb());
    }

    private IEnumerator DelayEnablingBomb() {
        yield return new WaitForEndOfFrame();
        if (checkpoint >= 2) {
            timer.ResetTimer();
            buttonInteractable.isActive = true;
        }
        if (checkpoint >= 3) {
            pourCupInteractable.isActive = false;
            receiver.localPosition = new Vector3(1.67999995f, 1.1900003f, 4.05999994f);
            receiver.localEulerAngles = new Vector3(356.150818f, 184.682495f, 90.2463608f);
            outroManager.SetWinState(1);
        }
    }
}
