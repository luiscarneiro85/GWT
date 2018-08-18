using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public GameObject scoreText;
    public GameObject heal;
    public GameObject label;
    public GameObject enemysLeft;
    public int score = 0;
    public AudioClip healSound;

    public float healCount = 0;
    float aux;
	// Use this for initialization
	void Start () {
        label.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if(healCount > 5)
        {
            healCount = 5f;
        }
        aux = healCount / 5f;
        heal.GetComponent<Image>().fillAmount = aux;

        scoreText.GetComponent<Text>().text = "SCORE: " + score;
        //enemysLeft.GetComponent<Text>().text = "ENEMYS LEFT: " + (20 - GameObject.Find("GeradorInimigos").GetComponent<GeradorInimigos>().wave_1.Count);
        enemysLeft.GetComponent<Text>().text = "ENEMYS LEFT: " + (20 - GameObject.Find("GeradorInimigos").GetComponent<GeradorInimigos>().index);



        if (Input.GetButtonDown("Fire2") && healCount >= 5)
        {
            healCount = 0;
            GameObject.Find("Player").GetComponent<MovementBasic>().currentLife += 2;
            GetComponent<AudioSource>().PlayOneShot(healSound);
        }

        if(GameObject.Find("Player").GetComponent<MovementBasic>().currentLife <= 0)
        {
            label.SetActive(true);
            GameObject.Find("Cannon").GetComponent<canhao>().canAttack = false;
            GameObject.Find("Player").GetComponent<MovementBasic>().canWalk = false;
        }
		
	}

    
}
