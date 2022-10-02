using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPromptController : MonoBehaviour {

    private TextMeshProUGUI promptText;

    private void Awake() {
        promptText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text) {
        gameObject.SetActive(true);
        promptText.text = text;
    }
}
