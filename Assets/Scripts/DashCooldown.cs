using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldown : MonoBehaviour {

    MovementBasic script;

    float cooldownTime = 7;
    float timer = 0;
    float aux;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("Player");
        script = go.GetComponent<MovementBasic>();
	}
	
	// Update is called once per frame
	void Update () {
        if (script.dashCooldown)
        {
            timer += Time.deltaTime;
            aux = timer / cooldownTime;
            GetComponent<Image>().fillAmount = aux;
            if(timer > cooldownTime)
            {
                script.dashCooldown = false;
                timer = 0;
            }
        }
        else
        {
            
        }
	}
}
// 
