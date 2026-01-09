using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAutoFade : MonoBehaviour {
    [SerializeField]
    float fadeSpeed = 3f;

    [SerializeField]
    Color fadeImageColor = Color.white;

    [SerializeField]
    float maxAlfa = 220f;

    [SerializeField]
    float minAlfa = 30f;

    Image image;
    bool flg;

    // Start is called before the first frame update
    void Start() {
        image = GetComponent<Image>();
        image.color = new Color(fadeImageColor.r, fadeImageColor.g, fadeImageColor.b, minAlfa / 255f);
    }

    // Update is called once per frame
    void Update() {
        Color _color = image.color;
        if (flg) {
            _color.a -= Time.deltaTime * fadeSpeed;
            if (_color.a <= minAlfa / 255f) flg = false;
        } else {
            _color.a += Time.deltaTime * fadeSpeed;
            if (_color.a >= maxAlfa / 255f) flg = true;
        }
        image.color = _color;
    }
}
