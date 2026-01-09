using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// エンドロール(フェードテキスト＞スタッフロール)を制御するスクリプト。
/// </summary>
public class EndCreditScript : MonoBehaviour
{
    [SerializeField]
    Image fadeTextBackImage;

    [SerializeField]
    Text fadeDispText;

    [SerializeField]
    GameObject scrollObject;

    [SerializeField]
    Text titleText;

    [SerializeField]
    Text scrollText;

    [SerializeField]
    Image startFadeImage;

    [SerializeField]
    float fadeSpeed = 0.7f;

    [SerializeField]
    float scrollSpeed = 0.5f;

    [SerializeField]
    float scrollEndY = 1000f;

    [SerializeField, TextArea ,Header("フェード表示するテキスト")]
    string[] fadeDispTextData;

    [SerializeField, Header("タイトル"), TextArea]
    string title;
    [SerializeField, Header("制作"), TextArea]
    string product;
    [SerializeField, Header("音楽"), TextArea]
    string bgm;
    [SerializeField, Header("効果音"), TextArea]
    string se;
    [SerializeField, Header("グラフィック"), TextArea]
    string graphic;
    [SerializeField, Header("挨拶"), TextArea]
    string end;


    AudioController audioController;
    Database database;
    NextScene nextScene;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioController>();
        database = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Database>();
        nextScene = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NextScene>();
        startFadeImage.color = Color.black;
        ScrollTextDataCreate();
    }
    void Start()
    {
        audioController.ChangeBgm(0);
        StartCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        yield return StartCoroutine(ImageFadeOut(startFadeImage));
        yield return StartCoroutine(ImageFadeIn(fadeTextBackImage));

        for (int i = 0; i < fadeDispTextData.Length; i++)
        {
            fadeDispText.text = fadeDispTextData[i];
            yield return StartCoroutine(TextFade(fadeDispText));
        }

        yield return StartCoroutine(ImageFadeOut(fadeTextBackImage));
        yield return StartCoroutine(TextScroll());
        yield break;
    }
    IEnumerator ImageFadeIn(Image image)
    {
        //徐々に表示
        while (true)
        {
            image.color = new Color(0, 0, 0, image.color.a + fadeSpeed * Time.deltaTime);
            yield return null;
            if (image.color.a >= 200f / 255f) break;
        }
        yield break;
    }
    IEnumerator ImageFadeOut(Image image)
    {
        //徐々に表示
        while (true)
        {
            image.color = new Color(0, 0, 0, image.color.a - fadeSpeed * Time.deltaTime);
            yield return null;
            if (image.color.a <= 0f)  break;
        }
        Destroy(image.gameObject);
        yield break;
    }
    IEnumerator TextFade(Text text)
    {
        var dispTime = (text.text.Length) * 0.2f;
        Debug.Log("表示時間:" + dispTime);

        //徐々に表示
        while (true)
        {
            text.color = new Color(1f, 1f, 1f, text.color.a + fadeSpeed * Time.deltaTime);
            yield return null;
            if (text.color.a >= 1f) break;
        }

        //待ち
        yield return new WaitForSeconds(dispTime);

        //徐々に消える
        while (true)
        {
            text.color = new Color(1f, 1f, 1f, text.color.a - fadeSpeed * Time.deltaTime);
            yield return null;

            if (text.color.a <= 0f) break;
        }

        //待ち
        yield return new WaitForSeconds(0.5f);

        yield break;
    }
    IEnumerator TextScroll()
    {
        var _rectTransform = scrollObject.GetComponent<RectTransform>();

        //スクロールさせる
        while (true)
        {
            var pos = _rectTransform.localPosition;
            pos.y += Time.deltaTime + scrollSpeed;
            _rectTransform.localPosition = pos;
            yield return null;
            if (pos.y >= scrollEndY)  break;
        }
        yield break;
    }

    private void ScrollTextDataCreate()
    {
        titleText.text = null;
        scrollText.text = null;

        titleText.text = title;
        scrollText.text += "＜制作＞\n" + product + "\n\n\n";
        scrollText.text += "＜音楽＞\n" + bgm + "\n\n\n";
        scrollText.text += "＜効果音＞\n" + se + "\n\n\n";
        scrollText.text += "＜グラフィック＞\n" + graphic + "\n\n\n\n\n\n\n\n\n";
        scrollText.text +=  end;

    }

    public void CloseEndCredit()
    {
        nextScene.NextSceneRun(1);
        audioController.SePlay(6);
        audioController.ChangeBgm(1);
        Destroy(gameObject);
    }
}
