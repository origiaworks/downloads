using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextJapaneseLocalize : MonoBehaviour {
    [SerializeField] Text[] text;
    [SerializeField] string[] japaneseText;

    void Start() {
        for (int i = 0; i < text.Length; i++) {
            if (Application.systemLanguage == SystemLanguage.Japanese) {
                text[i].text = japaneseText[i];
            }
        }
    }
}
