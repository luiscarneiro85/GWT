using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canhao : MonoBehaviour {

    Vector2 mousePos;
    Vector2 direction;
    public GameObject tiro;
    public GameObject tiro2;

    public AudioClip attack;
    public AudioClip attack2;

    float contador = 0;
    bool readyTiro2 = false;

    public bool attackCooldown2 = false;

    Animator anim;

    MovementBasic script;

    public bool canAttack = true;

    // Use this for initialization
    void Start () {
        anim = GetComponentInParent<Animator>();
        GameObject go = GameObject.Find("Player");
        script = go.GetComponent<MovementBasic>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // converte a posicao do mouse e salva dentro da variavel
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //pega a direcao alvo 
        direction = (mousePos - (Vector2)transform.position);
        //seta o valor 
        transform.up = direction;

        //verifica se o botao foi pressionado ou solto
        if (Input.GetButtonDown("Horizontal"))
        {
            readyTiro2 = true;
        }
        if(Input.GetButtonUp("Horizontal"))
        {
            contador = 0;
            readyTiro2 = false;
        }


        if (readyTiro2 && script.canAttack && canAttack)
        {
            //controla delay para utilizacao do ataque 2 tempo entre apertar as teclas
            contador += Time.deltaTime;
            if(contador > 0.7f)
            {
                contador = 0;
                readyTiro2 = false;
            }
  
            if(contador < 0.7f && Input.GetButtonDown("Fire1") && !attackCooldown2)
            {
                anim.SetTrigger("Attack");
                attackCooldown2 = true;
                readyTiro2 = false;
                contador = 0;
                StartCoroutine(animationAttack2());

            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && script.canAttack && canAttack)
            {
               anim.SetTrigger("Attack");
               StartCoroutine(animationAttack());
                StartCoroutine(attackDelay());
            }
        }

    }

    IEnumerator animationAttack2()
    {
        yield return new WaitForSeconds(.15f);
        Instantiate(tiro2, transform.position, transform.rotation);
        GetComponent<AudioSource>().PlayOneShot(attack2);
        
    }

    IEnumerator animationAttack()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(tiro, transform.position, transform.rotation);
        GetComponent<AudioSource>().PlayOneShot(attack);
    }

    IEnumerator attackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(.5f);
        canAttack = true;
    }
}
