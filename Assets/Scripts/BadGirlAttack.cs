using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGirlAttack : MonoBehaviour {

    float speed = 3f;
    Vector3 target;
    Vector3 direction;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        Destroy(this.gameObject, 5f);
        transform.Translate(Vector3.up * speed * Time.deltaTime);
	}

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            yield return new WaitForSeconds(0.1f);
            collision.GetComponent<MovementBasic>().currentLife -= 1;
            GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(
                GameObject.Find("Player").GetComponent<MovementBasic>().hit);
            collision.GetComponent<Animator>().SetTrigger("Damage");
            Destroy(this.gameObject);
        }
    }
}
