using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack2Cooldown : MonoBehaviour {

    canhao script;

    float cooldownTime = 3;
    float timer = 0;
    float aux;

    // Use this for initialization
    void Start () {
        GameObject go = GameObject.Find("Cannon");
        script = go.GetComponent<canhao>();
    }
	
	// Update is called once per frame
	void Update () {
        if (script.attackCooldown2)
        {
            timer += Time.deltaTime;
            aux = timer / cooldownTime;
            GetComponent<Image>().fillAmount = aux;
            if (timer > cooldownTime)
            {
                script.attackCooldown2 = false;
                timer = 0;
            }
        }
        else
        {

        }
    }
}
