using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Imageのフェードイン・アウトとTextのスライドイン・アウトをセットにしたスクリプト。
/// </summary>
public class ImageFadeTextSlideSetScript : MonoBehaviour
{
    [SerializeField]
    public Text text;

    [SerializeField]
    Image image;

    [SerializeField]
    public float stopTime = 1f;

    [SerializeField]
    float moveSpeed = 4000f;

    [SerializeField]
    float fadeSpeed = 1f;

    [SerializeField][Range(0,1f)]
    float backImageAlph = 0.8f;

    [SerializeField]
    RectTransform rectTransform;

    public void AnimationRun()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        //fadein_image
        while (true)
        {
            var color = image.color;
            color.a += Time.deltaTime * fadeSpeed;

            if (color.a >= backImageAlph)
            {
                color.a = backImageAlph;
                image.color = color;
                break;
            }
            image.color = color;
            yield return null;
        }

        //slidein_text
        while (true)
        {
            var pos = rectTransform.localPosition;
            pos.x += Time.deltaTime * moveSpeed;

            if (pos.x >= 0)
            {
                pos.x = 0;
                rectTransform.localPosition = pos;
                break;
            }
            rectTransform.localPosition = pos;
            yield return null;
        }

        //stop_text
        yield return new WaitForSeconds(stopTime);

        //slideout_text
        while (true)
        {
            var pos = rectTransform.localPosition;
            pos.x += Time.deltaTime * moveSpeed;

            if (pos.x >= 730f)
            {
                break;
            }
            rectTransform.localPosition = pos;
            yield return null;
        }
        //fadeout_image
        while (true)
        {
            var color = image.color;
            color.a -= Time.deltaTime * fadeSpeed;

            if (color.a <= 0f)
            {
                Destroy(gameObject);
                break;
            }
            image.color = color;
            yield return null;
        }
        yield break;
    }

    private void OnDestroy()
    {
        //callback
    }
}
