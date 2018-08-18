using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MovementBasic : MonoBehaviour {

    float moveHorizontal;
    float moveVertical;
    [SerializeField]
    float speed;
    [SerializeField]
    float force;

    float dashTimer = 0;
    bool dashReady = false;
    public bool dashCooldown = false;

    Vector2 mousePos;
    Vector2 direction;

    public Transform spot;
    public GameObject smoke;
    public AudioClip hit;

    public GameObject bar;
    float life = 10;
    float lifeAux;
    public float currentLife;
    public bool isAlive = true;
    public bool run = false;
    float runTimer = 0;

    public bool canAttack = true;
    public bool canWalk = true;
    Animator anim;

    CapsuleCollider2D colider;

	// Use this for initialization
	void Start () {
        currentLife = life;
        anim = GetComponent<Animator>();
        colider = GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        FillBar();

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        // converte a posicao do mouse e salva dentro da variavel
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //cria um raio na direcao do vector3 apartir da camera
        //Camera.main.ViewportPointToRay();

        //flipa o player
        if (mousePos.x > transform.position.x)
        {
            GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if(mousePos.x < transform.position.x)
        {
            GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }

        if (canWalk)
        {
            Dodge();
            Run();
        }
        

        //pega a direcao alvo 
        //direction = (mousePos - (Vector2)transform.position).normalized;
        //seta o valor 
        //transform.up = direction;

        if (canWalk)
        {
            
            Movement(moveVertical, moveHorizontal);
        }
        
       
	   
	}

    void Run()
    {
        // se run for verdadeiro inicia a contagem de delay para apertar o botao pela segunda vez
        if (run)
        {
            runTimer = Time.deltaTime;
            // verifica se o botao foi pressionado pela segunda vez dentro do intervalo de tempo
            if (Input.GetButtonDown("Horizontal") && runTimer < 0.7f)
            {
                speed = 150f;
                anim.SetBool("Run", true);
                canAttack = false;
            }
            //se o botao for solto durante a corrida reseta a velocidade e o contador
            if (Input.GetButtonUp("Horizontal"))
            {
                run = false;
                canAttack = true;
                runTimer = 0;
                speed = 50f;
                anim.SetBool("Run", false);
                
            }

        }

        //se pressionado o botao pela primeira vez modifica o valor de run
        if (Input.GetButtonDown("Horizontal") && !run)
        {            
            run = true;
        }

        // tempo para pressionar o botao pela segunda vez
        if(runTimer > 0.7f)
        {
            runTimer = 0;
            run = false;
        }

        

    }

    void Movement(float v, float h)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(h*Time.deltaTime*speed, v*Time.deltaTime*speed);
        if(h != 0)
        {
            anim.SetFloat("Walk", Mathf.Abs(h));
        }
        else if(v != 0)
        {
            anim.SetFloat("Walk", Mathf.Abs(v));
        }

        direction.x = h;
        direction.y = v;
      
    }

    IEnumerator dashAnimation()
    {
        Instantiate(smoke, spot);
        yield return new WaitForSeconds(1f);

    }

    void FillBar()
    {
        lifeAux = currentLife / life;

        if(currentLife > 10f)
        {
            currentLife = 10f;
        }
        if(currentLife < 0)
        {
            currentLife = 0;
            isAlive = false;
        }
        bar.GetComponent<Image>().fillAmount = lifeAux;
    }


    

    void Dodge()
    {
        if (!dashCooldown)
        {
            dashTimer += Time.deltaTime;
            if (Input.GetButtonDown("Fire3") && (direction.x != 0 || direction.y != 0))
            {
                StartCoroutine(animationDodge());
                anim.SetTrigger("Dodge");
                GetComponent<Rigidbody2D>().AddForce(direction * force);
                StartCoroutine(dashAnimation());
                dashReady = false;
                dashCooldown = true;
                dashTimer = 0;
            }

            if (dashTimer >= 0.7f)
            {
                dashTimer = 0;
                dashReady = false;
            }
        }
            
        
    }

    IEnumerator animationDodge()
    {
        //desabilita a colisao durante o movimento de dodge
        anim.SetTrigger("Dodge");
        canAttack = false;
        colider.enabled = false;
        yield return new WaitForSeconds(0.75f);
        canAttack = true;
        colider.enabled = true; ;
    }

    
}
