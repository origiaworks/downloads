using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIを自動で回転させるスクリプト
/// </summary>
public class AutoRotationScript : MonoBehaviour
{
    [SerializeField]
    private float fltSpeed = 200f;//速さ

    public bool blnReverseFlag = false;//逆回転フラグ
    public bool blnRunFlag = true;//実行フラグ

    private float fltZ = 0;

    void Start()
    {
        StartCoroutine(RotationRun());
    }

    IEnumerator RotationRun()
    {
        while (true)
        {
            //実行フラグ
            if (blnRunFlag)
            {
                var rtf = gameObject.GetComponent<RectTransform>();

                //逆回転フラグ
                if (blnReverseFlag)
                {
                    fltZ += Time.deltaTime * fltSpeed;
                }
                else
                {
                    fltZ -= Time.deltaTime * fltSpeed;
                }

                //値を代入
                rtf.localRotation = Quaternion.Euler(rtf.localRotation.x, rtf.localRotation.y, fltZ);
                yield return null;
            }
        }
    }
}
