using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadGirl : MonoBehaviour
{

    Animator anim;
    float hp;
    float hpAux;
    float hpMax = 3f;
    float speed = .7f;

    Vector3 target;
    Vector3 direction;

    public GameObject attack;
    public Transform attackSpot;

    bool canWalk = true;
    bool canAttack = false;
    float fieldVision = 3f;


    public GameObject bar;

    RaycastHit2D hit;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        hp = hpMax;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        hpAux = hp / hpMax;

        bar.GetComponent<Image>().fillAmount = hpAux;

        if (hp <= 0)
        {
            StartCoroutine(dieAnimation());
        }

        /*if (GameObject.Find("Player").GetComponent<MovementBasic>().currentLife <= 0)
        {
            canAttack = false;
            canWalk = false;
        }*/

        target = GameObject.Find("Player").GetComponent<Transform>().position;
        direction = (target - transform.position).normalized;
        if (canWalk && GameObject.Find("Player").GetComponent<MovementBasic>().isAlive)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }


        if (transform.position.x < target.x)
        {
            GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }

        hit = Physics2D.Raycast((Vector2)transform.position, (Vector2)direction, fieldVision, 1 << LayerMask.NameToLayer("Default"));
        if(hit.collider != null)
        {
            if(hit.collider.tag == "Player" && !canAttack && GameObject.Find("Player").GetComponent<MovementBasic>().isAlive)
            {
                //Instantiate(attack, attackSpot.position, attackSpot.rotation);
                canAttack = true;
                StartCoroutine(attackAnimation());
            }
        }

       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Attack")
            {
                Destroy(collision.gameObject);
                hp -= 1f;
                if (hp > 0)
                {
                    anim.SetTrigger("Damage");
                }
            }
            if (collision.gameObject.tag == "Attack2")
            {
                Destroy(collision.gameObject);
                hp -= 2f;
                if (hp > 0)
                {
                    anim.SetTrigger("Damage");
                }
            }
       
    }



    IEnumerator dieAnimation()
    {
        anim.SetTrigger("Die");
        canWalk = false;
        canAttack = true;
        yield return new WaitForSeconds(2f);
        GameObject.Find("GameManager").GetComponent<GameControl>().score += 200;
        GameObject.Find("GameManager").GetComponent<GameControl>().healCount++;
        GameObject.Find("GeradorInimigos").GetComponent<GeradorInimigos>().numberOfEnemys--;
        Destroy(this.gameObject);
    }

    IEnumerator attackAnimation()
    {
        anim.SetTrigger("Attack");
        canWalk = false;
        yield return new WaitForSeconds(0.3f);
        Instantiate(attack, attackSpot.position, attackSpot.rotation);
        yield return new WaitForSeconds(3f);
        canWalk = true;
        canAttack = false;
    }
}
