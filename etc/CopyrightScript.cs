using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CopyrightScript : MonoBehaviour {
    TextMeshProUGUI _text;

    [SerializeField]
    private int Year = 2025;

    void Start() {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "Copyright © " + Year + " " + Application.companyName + "All Rights Reserved. " + "Ver." + Application.version;
    }
}
