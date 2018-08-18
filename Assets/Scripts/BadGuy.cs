using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadGuy : MonoBehaviour {

    Animator anim;
    float hp;
    float hpAux;
    float hpMax = 8f;
    float speed = 1f;

    Vector3 target;
    Vector3 direction;

    bool canWalk = true;

    public GameObject bar;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        hp = hpMax;
	}
	
	// Update is called once per frame
	void Update () {

        hpAux = hp / hpMax;

        bar.GetComponent<Image>().fillAmount = hpAux;
        

        if (hp <= 0)
        {
            StartCoroutine(dieAnimation());
        }

        target = GameObject.Find("Player").GetComponent<Transform>().position;
        direction = (target - transform.position).normalized;

        if (canWalk && GameObject.Find("Player").GetComponent<MovementBasic>().isAlive)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        

        if(transform.position.x < target.x)
        {
            GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }

        
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Attack")
            {
                //yield return new WaitForSeconds(0.1f); 
                Destroy(collision.gameObject);
                hp -= 1f;
            if (hp > 0)
                {
                    anim.SetTrigger("Damage");
                }
            }
            if (collision.gameObject.tag == "Attack2" && GameObject.Find("Player").GetComponent<MovementBasic>().isAlive)
            {
                //yield return new WaitForSeconds(0.1f);
                Destroy(collision.gameObject);
                hp -= 2f;
                if (hp > 0)
                {
                    anim.SetTrigger("Damage");
                }
            }
            if (collision.gameObject.tag == "Player")
            {
               
                    StartCoroutine(attackAnimation());
                    GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(
                        GameObject.Find("Player").GetComponent<MovementBasic>().hit);
                    collision.gameObject.GetComponent<MovementBasic>().currentLife -= 2;
                    collision.GetComponent<Animator>().SetTrigger("Damage");
            
            }
        
       
    }

    IEnumerator dieAnimation()
    {
        anim.SetTrigger("Die");
        canWalk = false;
        yield return new WaitForSeconds(2f);
        GameObject.Find("GameManager").GetComponent<GameControl>().score += 300;
        GameObject.Find("GameManager").GetComponent<GameControl>().healCount++;
        GameObject.Find("GeradorInimigos").GetComponent<GeradorInimigos>().numberOfEnemys--;
        Destroy(this.gameObject);
    }

    IEnumerator attackAnimation()
    {
        anim.SetTrigger("Attack");
        canWalk = false;
        yield return new WaitForSeconds(1.5f);
        canWalk = true;
    }
}
