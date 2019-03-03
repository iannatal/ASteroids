using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour {

    public GameObject Tiro;
    private float tempoAtirar;
    private float x;
    private float y;
    private float velInicial;

    private void Start () {

        tempoAtirar = 2f;
        velInicial = 20f;

        if (transform.localPosition.x <= -9f)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(velInicial, 0f));
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-velInicial, 0f));
        }
    }

    private void FixedUpdate () {
        tempoAtirar -= Time.deltaTime;
        x = transform.localPosition.x;
        y = transform.localPosition.y;

        if (tempoAtirar <= 0f)
        {    
            tempoAtirar = 2f;
            Instantiate(Tiro, new Vector3(x, y, 0f), Quaternion.identity);
        }

        if(x <= -11f || x >= 11f)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("AstGrande") || col.gameObject.CompareTag("AstMedio") || col.gameObject.CompareTag("AstPequeno"))
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}
