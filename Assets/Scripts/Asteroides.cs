using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroides : MonoBehaviour
{
    public GameObject asteroide;
    public GameObject escudo;
    public int qtdAsteroides;
    private float x;
    private float y;
    private float velInicial;
    private float angulo;
    private float tempo;

    private void Start()
    {
        angulo = Random.Range(0f, 360f);
        velInicial = 250f;
        tempo = 0f;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(velInicial * Mathf.Cos(angulo * Mathf.Deg2Rad), velInicial * Mathf.Sin(angulo * Mathf.Deg2Rad)));
    }

    private void FixedUpdate()
    {
        x = transform.localPosition.x;
        y = transform.localPosition.y;

        if ((x > 9.7f || x < -9.7f || y > 6 || y < -6))
        {
            if (tempo <= 0f)
            {
                transform.localPosition = new Vector3(-x, -y);
                tempo = 0.5f;
            }
        }
        if (tempo > 0f)
        {
            tempo -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        while (qtdAsteroides > 0)
        {
            Instantiate(asteroide, new Vector3(x, y, 0f), Quaternion.identity);
            qtdAsteroides--;
        }

        if (gameObject.CompareTag("AstMedio"))
        {
            if (Random.Range(0.0f, 1.0f) < 0.075f)
            {
                Instantiate(escudo, new Vector3(x, y, 0f), Quaternion.identity);
            }
        }        
    }
}
