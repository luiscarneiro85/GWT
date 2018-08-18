using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigos : MonoBehaviour {

    GameObject gerador_1;
    GameObject gerador_2;
    GameObject gerador_3;
    GameObject gerador_4;

    public GameObject girl;
    public GameObject guy;

    public GameObject label;

    public int index = 0;
    int random;
    float timer = 0;
    public int numberOfEnemys = 0;
    bool ready = true;

    public List<bool> freeSpot = new List<bool>();

    public List<GameObject> wave_1 = new List<GameObject>();

    private void Awake()
    {
        gerador_1 = transform.GetChild(0).gameObject;
        gerador_2 = transform.GetChild(1).gameObject;
        gerador_3 = transform.GetChild(2).gameObject;
        gerador_4 = transform.GetChild(3).gameObject;
    }

    // Use this for initialization
    void Start () {

        label.SetActive(false);
        Instantiate(wave_1[index], gerador_1.transform.position, gerador_1.transform.rotation);
        index++;
        numberOfEnemys++;
        Instantiate(wave_1[index], gerador_3.transform.position, gerador_3.transform.rotation);
        index++;
        numberOfEnemys++;

    }
	
	// Update is called once per frame
	void Update () {

        if (!ready)
        {
            timer += Time.deltaTime;
            if(timer > 5f)
            {
                timer = 0;
                ready = true;
            }
        }

        if(index > 19)
        {
            label.SetActive(true);
            GameObject.Find("Cannon").GetComponent<canhao>().canAttack = false;
            GameObject.Find("Player").GetComponent<MovementBasic>().canWalk = false;
        }

        if (numberOfEnemys < 6 && ready)
        {
            random = Mathf.RoundToInt(Random.Range(1, 4));
            switch (random)
            {
                case 0:
                    break;
                case 1:
                    Instantiate(wave_1[index], gerador_1.transform.position, gerador_1.transform.rotation);
                    break;
                case 2:
                    Instantiate(wave_1[index], gerador_2.transform.position, gerador_2.transform.rotation);
                    break;
                case 3:
                    Instantiate(wave_1[index], gerador_3.transform.position, gerador_3.transform.rotation);
                    break;
                case 4:
                    Instantiate(wave_1[index], gerador_4.transform.position, gerador_4.transform.rotation);
                    break;
            }

            ready = false;
            numberOfEnemys++;
            index++;
        }

    }




}
