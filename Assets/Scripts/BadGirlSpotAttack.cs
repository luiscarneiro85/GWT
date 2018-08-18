using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGirlSpotAttack : MonoBehaviour {

    Vector3 target;
    Vector3 direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // converte a posicao do mouse e salva dentro da variavel
        target = GameObject.Find("Player").GetComponent<Transform>().position;

        //pega a direcao alvo 
        direction = (target - transform.position);
        //seta o valor 
        transform.up = direction;
    }
}


