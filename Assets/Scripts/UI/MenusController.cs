using UnityEngine;

public class MenusController : MonoBehaviour {

    [SerializeField] private PlayerController player;

    private bool wasInteractionDisabled = false;

    private void OnEnable() {
        wasInteractionDisabled = player.interactionDisabled;
        player.SetInteractionDisabled(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable() {
        player.SetInteractionDisabled(wasInteractionDisabled);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        player.gameHasStarted = true;
    }
}
