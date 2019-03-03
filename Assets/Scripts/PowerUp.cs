using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    private float tempoParaSumir;

    private void Start()
    {
        tempoParaSumir = 5f;
    }

    private void FixedUpdate()
    {
        tempoParaSumir -= Time.deltaTime;
        if(tempoParaSumir <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Nave.qtdEscudos++;
        Destroy(gameObject);
    }
}
