using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CritterGameManager : MonoBehaviour {

    [SerializeField] private int numCritters = 5;
    [SerializeField] private UnityEvent onGameWin;
    [SerializeField] private List<AudioClip> deathClips;

    private int numKilled = 0;

    public void AddKill() {
        if (++numKilled >= numCritters) {
            onGameWin?.Invoke();
        }
    }

    public AudioClip GetDeathClip() {
        return deathClips[numKilled];
    }
}
