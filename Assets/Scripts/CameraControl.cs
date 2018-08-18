using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

     float limitMinX;
     float limitMaxX;
     float limitMinY;
     float limitMaxY;

    Transform target;
    GameObject player;
    public GameObject map;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        limitMinX = -2.17f;
        limitMaxX = 1.36f;
        limitMinY = -2.2f;
        limitMaxY = 2.42f;
    }
	
	// Update is called once per frame
	void Update () {

        target = player.GetComponent<Transform>().transform;

        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, limitMinX, limitMaxX),
            Mathf.Clamp(target.position.y, limitMinY, limitMaxY),
            transform.position.z);

        
	}
}
