# SliderGaugeController for Unity

UnityのUI Sliderと連動して、ゲージの色やテキスト表示を動的に更新する便利なコンポーネントです。
HPバーやMPバー、読み込み状況のインジケーターなどに最適です。

## 主な機能

- **動的な色の変化**: 残量（％）に応じて、ゲージの色を自動的に変更します（例：HPが減ると緑→黄色→赤に変化）。
- **テキスト連動**: TextMeshProを使用して、「100 / 500」といった現在の値と最大値をリアルタイムに表示します。
- **柔軟なカスタマイズ**: インスペクターから閾値（しきい値）や色の組み合わせ、テキストの表示形式を自由に変更可能です。
- **エディタフレンドリー**: `OnValidate` を実装しているため、再生しなくてもインスペクター上で値をいじれば即座に見た目を確認できます。

## セットアップ方法

1.  **ファイルの配置**: `SliderGaugeController.cs` をプロジェクトの `Assets` フォルダ内に配置します。
2.  **アタッチ**: Sliderオブジェクト、または適当なGameObjectに `SliderGaugeController` をアタッチします。
3.  **コンポーネントの設定**:
    - **Target Slider**: 対象となるSliderをドラッグ＆ドロップ（自動取得も試みます）。
    - **Fill Image**: Slider内の `Fill` 画像を指定します。
    - **Value Text**: 現在値を表示したい `TextMeshPro - Text (UI)` を指定します。
4.  **色の設定**: `Color Thresholds` リストに、％と色のペアを追加します（デフォルトで20%, 50%, 100%の設定が作成されます）。

## インスペクターの設定項目

| 項目 | 説明 |
|:---|:---|
| **Target Slider** | 制御対象のSliderコンポーネント。 |
| **Fill Image** | 色を変更する対象のImage（通常はSliderのFill部分）。 |
| **Value Text** | 値を表示するTextMeshProUGUI。 |
| **Update Color / Text** | 色やテキストの更新を行うかどうかのフラグ。 |
| **Text Format** | テキストの書式。`{0}` が現在値、`{1}` が最大値に置き換わります。 |
| **Color Thresholds** | ％以下の時に適用する色のリスト。低い順に自動ソートされます。 |

## スクリーンショットのヒント
*(ここに実際の動作画面のGIFや画像を貼ると、よりGitHubらしくなります)*

## 動作環境
- Unity 2019.4 LTS 以上 (TextMeshProを使用)
- Linux / Windows / macOS / Mobile

---
© 2024 ORIGIAWORKS
