using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[Serializable]
public enum WinState {

    BEFORE_CALL,
    DURING_CALL,
    AFTER_KNOWING,
}

public class OutroManager : MonoBehaviour {

    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource phoneAudioSource;
    [SerializeField] private List<PlayableDirector> timelines;

    [SerializeField] private UnityEvent onWinBeforeCall;
    [SerializeField] private UnityEvent onWinDuringCall;
    [SerializeField] private UnityEvent onWinAfterKnowing;

    private WinState winState = WinState.BEFORE_CALL;

    public void SetWinState(int state) {
        switch (state) {
            case 0:
                winState = WinState.BEFORE_CALL;
                break;
            case 1:
                winState = WinState.DURING_CALL;
                break;
            case 2:
                winState = WinState.AFTER_KNOWING;
                break;
        }
    }

    public void OnWin() {
        switch (winState) {
            case WinState.BEFORE_CALL:
                timelines.ForEach(timeline => timeline.Stop());
                onWinBeforeCall?.Invoke();
                break;
            case WinState.DURING_CALL:
                timelines.ForEach(timeline => timeline.Stop());
                onWinDuringCall?.Invoke();
                break;
            case WinState.AFTER_KNOWING:
                timelines.ForEach(timeline => timeline.Stop());
                onWinAfterKnowing?.Invoke();
                break;
        }
    }
}
