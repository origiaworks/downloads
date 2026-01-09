using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickScript : MonoBehaviour {
    Vector3 startPos;
    Vector3 endPos;

    public float sensitivity = 20f;
    public float flickValue_x;
    public float flickValue_y;

    void Update() {
        if (Input.GetMouseButtonDown(0) == true) {
            startPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }
        if (Input.GetMouseButtonUp(0) == true) {
            endPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            FlickDirection();
        }
    }

    void FlickDirection() {
        flickValue_x = endPos.x - startPos.x;
        flickValue_y = endPos.y - startPos.y;
    }
}
