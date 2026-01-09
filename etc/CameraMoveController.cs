using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// カメラにアタッチしてカメラの動作を制御するスクリプト
/// </summary>
public class CameraMoveController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("カメラの移動場所の格納")]
    Vector3[] cameraAngles;

    [SerializeField]
    [Tooltip("カメラの移動速度")]
    float moveSpeed;

    [SerializeField]
    [Tooltip("レイキャスト遮断用UI")]
    GameObject protect;

    public void CameraMoveRun(int angleNum)
    {
        StartCoroutine(CameraMove(angleNum));
    }

    //場所の指定に使用する定数
    public const int home = 0;
    public const int mana = 1;
    public const int playerL = 2;
    public const int playerC = 3;
    public const int playerR = 4;
    public const int enemyL = 5;
    public const int enemyC = 6;
    public const int enemyR = 7;

    IEnumerator CameraMove(int angleNum)
    {
        var prefab = Instantiate(protect, GameObject.Find("Canvas").transform);

        var anglePos = cameraAngles[angleNum];

        while (true)
        {
            var adjust = 0.05f;
            var pos = transform.position;
            var distanceX = pos.x - anglePos.x;
            var distanceY = pos.y - anglePos.y;
            var distanceZ = pos.z - anglePos.z;
            var move = Time.deltaTime * moveSpeed;

            //x
            if (distanceX > 0) pos.x -= move;
            if (distanceX < 0) pos.x += move;
            if (distanceX >= -adjust && distanceX <= adjust) pos.x = anglePos.x;

            //y
            if (distanceY > 0) pos.y -= move;
            if (distanceY < 0) pos.y += move;
            if (distanceY >= -adjust && distanceY <= adjust) pos.y = anglePos.y;

            //z
            if (distanceZ > 0) pos.z -= move;
            if (distanceZ < 0) pos.z += move;
            if (distanceZ >= -adjust && distanceZ <= adjust) pos.z = anglePos.z;

            transform.position = pos;

            //終了判定
            if (distanceX == 0 && distanceY == 0 && distanceZ == 0)
            {
                //微調整
                transform.position = cameraAngles[angleNum];
                break;
            }
            yield return null;
        }

        Destroy(prefab);
        yield break;
    }
}
