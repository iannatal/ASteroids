using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarInimigos : MonoBehaviour {

    public GameObject NaveInimiga;
    private float tempo;
    private float x;
    private float y;

    private void Start () {
        tempo = 35f;
	}
	
	private void FixedUpdate () {
        tempo -= Time.deltaTime;

        if(tempo <= 0f)
        {
            y = Random.Range(-4.7f, 4.7f);
            if(Random.Range(0f, 1f) <= 0.5)
            {
                x = -9.4f;
            }
            else
            {
                x = 9.4f;
            }
            Instantiate(NaveInimiga,new Vector3(x, y, 0f), Quaternion.identity);
            tempo = 35f;
        }
	}
}
