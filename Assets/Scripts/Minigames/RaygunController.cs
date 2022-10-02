using DG.Tweening;
using UnityEngine;

public class RaygunController : MonoBehaviour {

    [SerializeField] private GameObject tracerPrefab;
    [SerializeField] private Transform tracerSpawnPos;
    [SerializeField] private float tracerSpeed = 0.25f;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot(CritterController target) {
        GameObject tracer = Instantiate(tracerPrefab, tracerSpawnPos.position, tracerSpawnPos.rotation);
        tracer.transform.DOMove(target.transform.position, tracerSpeed).OnComplete(() => {
            target.Kill();
            Destroy(tracer);
        });
        audioSource.Play();
    }
}
