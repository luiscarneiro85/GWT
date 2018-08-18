using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparo : MonoBehaviour {

    [SerializeField]
    float velocidade;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        GetComponent<Transform>().Translate(Vector2.up*Time.deltaTime*velocidade);

        Destroy(this.gameObject, 3f);
	}
}
