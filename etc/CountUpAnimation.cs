using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUpAnimation : MonoBehaviour
{
    [SerializeField]
    float speed = 0.5f;

    [SerializeField]
    int blinkCount = 3;

    [SerializeField]
    float blinkSpeed = 0.2f;

    private Text text;
    private float value;
    
    public float maxValue;
    public bool animationEndFlf = false;

    private void Awake()
    {
        text = gameObject.GetComponent<Text>();
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        value = 0;
        text.text = value.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountUp()
    {
        var _transform = gameObject.transform;
        var scale = _transform.localScale;

        //カウントアップ
        while (true)
        {
            if (scale.x < 1f) scale.x += Time.deltaTime * speed;
            if (scale.y < 1f) scale.y += Time.deltaTime * speed;
            if (value < maxValue) value += Time.deltaTime * (maxValue * 0.5f);

            transform.localScale = scale;
            text.text = "× " + value.ToString("N2");
            yield return null;

            if (_transform.localScale.x >= 1f && _transform.localScale.y >= 1f && value >= maxValue) break;
        }

        _transform.localScale = new Vector3(1, 1, 1);
        var count = 0;

        //点滅
        while (true)
        {
            text.enabled = false;
            yield return new WaitForSeconds(blinkSpeed);
            text.enabled = true;
            yield return new WaitForSeconds(blinkSpeed);
            count++;

            if (count >= blinkCount) break;
        }

        animationEndFlf = true;
        yield break;
    }
}
