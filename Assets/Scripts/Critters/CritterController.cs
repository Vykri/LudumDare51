using UnityEngine;

public class CritterController : MonoBehaviour {

    [SerializeField] private CritterGameManager critterGameManager;

    private Animator animator;
    private AudioSource audioSource;

    private void Awake() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate() {
        transform.LookAt(Camera.main.transform);
    }

    public void Kill() {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = critterGameManager.GetDeathClip();
        audioSource.Play();
        critterGameManager.AddKill();
        animator.SetBool("Dead", true);
    }
}
