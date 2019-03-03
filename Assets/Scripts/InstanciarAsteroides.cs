using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarAsteroides : MonoBehaviour
{  
    public GameObject asteroide;
    private float tempo;
    private int qtdAsteroides;
    private float x;
    private float y;

    private void Start()
    {
        qtdAsteroides = 3;
        tempo = 0f;
    }

    private void FixedUpdate()
    {
        if (GameObject.FindWithTag("AstGrande") == null && GameObject.FindWithTag("AstMedio") == null && GameObject.FindWithTag("AstPequeno") == null)
        {          
            if(tempo > 0f)
            {
                tempo -= Time.deltaTime;
            }
            else
            {
                qtdAsteroides++;
                for (int i = 0; i < qtdAsteroides; i++)
                {
                    if (Random.Range(-1f, 1f) >= 0f)
                    {
                        x = Random.Range(-9f, -4f);
                        y = Random.Range(2f, 5f);
                    }
                    else
                    {
                        x = Random.Range(4f, 9f);
                        y = Random.Range(-5f, -2f);
                    }
                    Instantiate(asteroide, new Vector3(x, y, 0f), Quaternion.identity);
                }
                tempo = 1.5f;
            }
        }
    }
}
