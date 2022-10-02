using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ExplosionController : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private List<PlayableDirector> timelines;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource phoneAudioSource;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Explode() {
        StartCoroutine(ExplodeCo());
    }

    private IEnumerator ExplodeCo() {
        playerAudioSource.Stop();
        phoneAudioSource.Stop();
        timelines.ForEach(timeline => timeline.Stop());
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
        CinemachineBasicMultiChannelPerlin perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_FrequencyGain = frequency;
        perlin.m_AmplitudeGain = amplitude;
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
