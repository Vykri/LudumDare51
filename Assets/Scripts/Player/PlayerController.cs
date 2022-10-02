using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [SerializeField] private InteractPromptController interactPrompt;
    [SerializeField] private float camPitchMin = -70f;
    [SerializeField] private float camPitchMax = 70f;
    [SerializeField] private float camYawMin = -90f;
    [SerializeField] private float camYawMax = 90f;
    [SerializeField] private GameObject menus;

    public float mouseSensitivity { get; set; } = 1f;
    public bool interactionDisabled = false;
    public bool gameHasStarted = false;

    private PlayerInput input;
    private Transform cam;

    private float xRot = 0f;
    private float yRot = 0f;
    private Interactable curInteractable;
    private bool interacting = false;
    private RaycastHit hit;
    private bool gameWon = false;

    private void Awake() {
        input = new PlayerInput();
        cam = transform.Find("PlayerCam");

        input.Player.Interact.started += _ => HandleInteract();
        input.Player.Interact.canceled += _ => HandleUninteract();
        input.Player.Menu.performed += _ => {
            if (gameHasStarted && !gameWon) {
                menus.SetActive(!menus.activeInHierarchy);
            }
        };
    }

    private void OnEnable() {
        input.Enable();
    }

    private void OnDisable() {
        input.Disable();
    }

    private void Update() {
        HandleLook();
        HandleInteractSelect();
    }

    public void SetInteractionDisabled(bool disabled = true) {
        interactionDisabled = disabled;
    }

    public void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        gameWon = true;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleInteract() {
        if (curInteractable && !interactionDisabled) {
            curInteractable.onInteract?.Invoke();
            interacting = true;
        }
    }

    private void HandleUninteract() {
        curInteractable?.onUninteract?.Invoke();
        interacting = false;
    }

    private void HandleLook() {
        Vector2 lookInput = input.Player.Look.ReadValue<Vector2>() * mouseSensitivity;

        yRot += lookInput.x;
        yRot = Mathf.Clamp(yRot, camYawMin, camYawMax);

        xRot -= lookInput.y;
        xRot = Mathf.Clamp(xRot, camPitchMin, camPitchMax);

        cam.localEulerAngles = new Vector3(xRot, yRot, 0f);
    }

    private void HandleInteractSelect() {
        if (Physics.Raycast(cam.position, cam.forward, out hit)) {
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            if (interactable && interactable.isActive) {
                if (curInteractable != interactable) {
                    curInteractable = interactable;
                    interactPrompt.SetText(curInteractable.description);
                    curInteractable.onSelect?.Invoke();
                }
            } else {
                if (interacting) {
                    HandleUninteract();
                }
                interactPrompt.gameObject.SetActive(false);
                curInteractable?.onDeselect?.Invoke();
                curInteractable = null;
            }
        } else {
            if (interacting) {
                HandleUninteract();
            }
            interactPrompt.gameObject.SetActive(false);
            curInteractable?.onDeselect?.Invoke();
            curInteractable = null;
        }
    }
}
