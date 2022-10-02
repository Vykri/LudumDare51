using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private Transform projectile;
    [SerializeField] private GameObject window;
    [SerializeField] private List<GameObject> windowPieces;

    [SerializeField] private float firingAngle = 45.0f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private AudioSource windowAudioSource;

    public void Throw() {
        StartCoroutine(ThrowCo());
    }

    private IEnumerator ThrowCo() {
        projectile.position = transform.position + new Vector3(0, 0.0f, 0);

        float dist = Vector3.Distance(projectile.position, target.position);
        float vel = dist / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        float vX = Mathf.Sqrt(vel) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float vY = Mathf.Sqrt(vel) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = dist / vX;
        projectile.rotation = Quaternion.LookRotation(target.position - projectile.position);

        float timer = 0;
        bool windowSwapped = false;

        while (timer < flightDuration) {
            projectile.Translate(0, (vY - (gravity * timer)) * Time.deltaTime, vX * Time.deltaTime);

            timer += Time.deltaTime;

            if (!windowSwapped && timer > 0.55f) {
                window.SetActive(false);
                windowPieces.ForEach(piece => piece.SetActive(true));
                windowSwapped = true;
                windowAudioSource.Play();
            }

            yield return null;
        }
    }
}
