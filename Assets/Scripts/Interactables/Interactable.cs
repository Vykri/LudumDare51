using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public string description = "Interact";
    public bool isActive = false;
    public UnityEvent onSelect;
    public UnityEvent onDeselect;
    public UnityEvent onInteract;
    public UnityEvent onUninteract;

    public void SetIsActive(bool isActive) {
        this.isActive = isActive;
    }
}
