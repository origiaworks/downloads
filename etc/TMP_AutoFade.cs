using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMP_AutoFade : MonoBehaviour {
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    float maxAlpha = 1f;

    [SerializeField]
    float minAlpha = 0f;

    [SerializeField]
    bool stop;

    TextMeshProUGUI text;
    bool fadeInOutFlg;
    float addvalue;

    // Start is called before the first frame update
    void Start() {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        if (stop) return;
        var _color = text.color;
        if (fadeInOutFlg == false) addvalue = speed * -1f;
        if (fadeInOutFlg) addvalue = speed;
        _color.a += Time.deltaTime * addvalue;
        text.color = _color;
        if (_color.a <= minAlpha && fadeInOutFlg == false) fadeInOutFlg = !fadeInOutFlg;
        if (_color.a >= maxAlpha && fadeInOutFlg) fadeInOutFlg = !fadeInOutFlg;
    }
}
