using UnityEngine;

/// <summary>
/// 画面解像度に合わせてカメラのViewport Rectを調整し、
/// レターボックス（上下黒帯）またはピラーボックス（左右黒帯）を生成します。
/// </summary>
[ExecuteAlways]
[AddComponentMenu("Layout/Aspect Adjuster")] // メニューから追加しやすく
public class AspectAdjuster : MonoBehaviour
{
    [Header("ターゲット設定")]
    [Tooltip("対象とするカメラ。未指定の場合はこのオブジェクトのカメラを使用します。")]
    [SerializeField] private Camera targetCamera;

    [Header("指定アスペクト比")]
    [Tooltip("目標とするアスペクト比（例：16:9 なら X=16, Y=9）")]
    [SerializeField] private Vector2 aspectVec = new Vector2(16, 9);

    // 変更検知用のキャッシュ
    private int _lastScreenWidth;
    private int _lastScreenHeight;
    private Vector2 _lastAspectVec;

    void Awake()
    {
        // カメラが未設定なら自身を取得
        if (targetCamera == null)
        {
            targetCamera = GetComponent<Camera>();
        }
    }

    void Update()
    {
        // 画面サイズまたは設定したアスペクト比に変更があった場合のみ再計算
        if (_lastScreenWidth != Screen.width ||
            _lastScreenHeight != Screen.height ||
            _lastAspectVec != aspectVec)
        {
            Adjust();
        }
    }

    /// <summary>
    /// ViewportRectの計算と適用
    /// </summary>
    private void Adjust()
    {
        if (targetCamera == null) return;

        // 0除算防止
        if (Screen.height <= 0 || aspectVec.y <= 0) return;

        // 現在の画面と目的のアスペクト比を計算
        float screenAspect = Screen.width / (float)Screen.height;
        float targetAspect = aspectVec.x / aspectVec.y;

        // 目的のアスペクト比にするための倍率
        float magRate = targetAspect / screenAspect;

        Rect viewportRect = new Rect(0, 0, 1, 1);

        if (magRate < 1)
        {
            // --- ピラーボックス (左右に黒帯) ---
            // 画面が横に長すぎるので、描画幅を狭める
            viewportRect.width = magRate;
            viewportRect.x = (1.0f - magRate) * 0.5f; // 中央寄せ
        }
        else
        {
            // --- レターボックス (上下に黒帯) ---
            // 画面が縦に長すぎるので、描画高さを狭める
            viewportRect.height = 1.0f / magRate;
            viewportRect.y = (1.0f - (1.0f / magRate)) * 0.5f; // 中央寄せ
        }

        targetCamera.rect = viewportRect;

        // 現在の状態を保存（変更検知用）
        _lastScreenWidth = Screen.width;
        _lastScreenHeight = Screen.height;
        _lastAspectVec = aspectVec;
    }

    // インスペクターで値が変更されたときに即座に反映させるためのコールバック
    private void OnValidate()
    {
        Adjust();
    }
}