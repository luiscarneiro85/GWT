using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootIn360 : MonoBehaviour {
    [SerializeField]
    float velocidade;
    [SerializeField]
    float distancia;
    [SerializeField]
    float anguloRotacao;
    [SerializeField]
    int complementoRotacao;
    float count = 0;
    public GameObject tiroSecundario;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        count += Time.deltaTime;

        if(count > distancia)
        {

            for (int i = 0; i<complementoRotacao; i++)
            {
                Instantiate(tiroSecundario, transform.position, transform.rotation);
               // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotacao),Time.deltaTime);
                transform.Rotate(new Vector3(0, 0, 1), anguloRotacao);
            }
            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<Transform>().Translate(Vector2.up * Time.deltaTime * velocidade);
        }
	}
}
