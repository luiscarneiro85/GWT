using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mira : MonoBehaviour {

    Vector2 mousePos;
    float valor;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //valor = Mathf.Clamp(mousePos.y, -1, 1);
        //transform.position = new Vector2(mousePos.x, valor);
        transform.position = mousePos;
       
        //pega a direcao alvo 
        //direction = (mousePos - (Vector2)transform.position).normalized;
        //seta o valor 

        

    }
}
