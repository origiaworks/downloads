using UnityEngine;

public class ObjectShaker : MonoBehaviour {
    public float shakeSpeed = 1.0f; // 揺れる速度
    public float shakeAmount = 1.0f; // 揺れる量

    private float startY; // オブジェクトの初期位置

    void Start() {
        startY = transform.position.y; // オブジェクトの初期位置を取得
    }

    void Update() {
        // 時間に応じてオブジェクトを上下に揺らす
        float newPosition = startY + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
    }
}
