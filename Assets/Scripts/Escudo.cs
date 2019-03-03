using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("AstGrande"))
        {
            Nave.pontos += 20;
        }
        else if (col.gameObject.CompareTag("AstMedio"))
        {
            Nave.pontos += 50;
        }
        else if (col.gameObject.CompareTag("AstPequeno") || col.gameObject.CompareTag("NaveInimiga"))
        {
            Nave.pontos += 100;
        }
        Destroy(col.gameObject);
        gameObject.SetActive(false);
    }
}
