using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スライダーの値に応じてゲージの色とテキストを更新するコンポーネント
/// </summary>
public class SliderGaugeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Slider targetSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI valueText;

    [Header("Settings")]
    [SerializeField] private bool updateColor = true;
    [SerializeField] private bool updateText = true;
    [SerializeField] private string textFormat = "{0:0} / {1:0}"; // 書式のカスタマイズ用

    [Header("Color Thresholds")]
    [SerializeField] private List<ColorThreshold> colorThresholds = new List<ColorThreshold>();

    [System.Serializable]
    public class ColorThreshold
    {
        [Range(0f, 100f)] public float percentThreshold;
        public Color color = Color.white;
    }
    void Reset()
    {
        // スライダーやイメージの自動取得もついでに行うと便利です
        if (targetSlider == null) targetSlider = GetComponent<Slider>();
        if (fillImage == null && targetSlider != null && targetSlider.fillRect != null)
        {
            fillImage = targetSlider.fillRect.GetComponent<Image>();
        }

        // デフォルトの3つの閾値をセット
        colorThresholds = new List<ColorThreshold>
        {
            new ColorThreshold { percentThreshold = 20f, color = Color.red },
            new ColorThreshold { percentThreshold = 50f, color = Color.yellow },
            new ColorThreshold { percentThreshold = 100f, color = Color.green }
        };
    }

    private void Awake()
    {
        if (targetSlider == null) targetSlider = GetComponent<Slider>();

        // リストを％の低い順に並び替えておく（判定ロジックを簡略化するため）
        if (colorThresholds != null && colorThresholds.Count > 0)
        {
            colorThresholds = colorThresholds.OrderBy(x => x.percentThreshold).ToList();
        }
    }

    private void OnEnable()
    {
        if (targetSlider != null)
        {
            // スライダーの値が変更された時に実行されるメソッドを登録
            targetSlider.onValueChanged.AddListener(OnSliderValueChanged);
            // 初回の表示更新
            RefreshUI(targetSlider.value);
        }
    }

    private void OnDisable()
    {
        if (targetSlider != null)
        {
            targetSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }

    // スライダーの値が変更された時のコールバック
    private void OnSliderValueChanged(float value)
    {
        RefreshUI(value);
    }

    /// <summary>
    /// UI全体の表示を更新
    /// </summary>
    public void RefreshUI(float value)
    {
        if (updateColor) UpdateGaugeColor(value);
        if (updateText) UpdateText(value);
    }

    private void UpdateGaugeColor(float value)
    {
        if (fillImage == null || colorThresholds == null || colorThresholds.Count == 0) return;

        float currentPercent = (value / targetSlider.maxValue) * 100f;

        // デフォルトの色（最も高い閾値の色、または現在の色）
        Color targetColor = fillImage.color;

        // 低い順にチェックし、現在の％が閾値以下ならその色を採用して抜ける
        foreach (var threshold in colorThresholds)
        {
            if (currentPercent <= threshold.percentThreshold)
            {
                targetColor = threshold.color;
                break;
            }
        }

        fillImage.color = targetColor;
    }

    private void UpdateText(float value)
    {
        if (valueText == null) return;
        valueText.text = string.Format(textFormat, value, targetSlider.maxValue);
    }

#if UNITY_EDITOR
    // インスペクターで値を変更した際に即座に反映させるための処理
    private void OnValidate()
    {
        if (targetSlider != null)
        {
            RefreshUI(targetSlider.value);
        }
    }
#endif
}