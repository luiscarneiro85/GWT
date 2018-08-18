using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadScene()
    {
        StartCoroutine(startScene());
    }

    IEnumerator startScene()
    {
        yield return new WaitForSeconds(.6f);
        SceneManager.LoadScene("teste");
    }
}
