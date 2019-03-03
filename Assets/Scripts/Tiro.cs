using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    private float x, y, angulo, tempo;
    private float teta, xNave,yNave;
    private GameObject nave;

    private void Start()
    {
        tempo = 1f;
        angulo = Nave.angulo + 90f;
        
        if (gameObject.CompareTag("Tiro"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(10f * Mathf.Cos(angulo * Mathf.Deg2Rad), 10f * Mathf.Sin(angulo * Mathf.Deg2Rad));
        }
        else
        {
            x = transform.localPosition.x;
            y = transform.localPosition.y;
            nave = GameObject.Find("Nave");
            xNave = nave.transform.localPosition.x;
            yNave = nave.transform.localPosition.y;
            

            if(xNave > 0)
            {
                if(yNave > 0)
                {
                    teta = Mathf.Atan2(Mathf.Abs(Mathf.Abs(yNave) - y), Mathf.Abs(Mathf.Abs(xNave) - x));
                    MovTiroInimigo();
                }
                else
                {
                    teta = Mathf.Atan2(Mathf.Abs(yNave - y), Mathf.Abs(Mathf.Abs(xNave) - x));
                    MovTiroInimigo();
                }
                
            }
            else
            {
                if(yNave > 0)
                {
                    teta = Mathf.Atan2(Mathf.Abs(yNave - y), Mathf.Abs(xNave - x));
                    MovTiroInimigo();
                }
                else
                {
                    teta = Mathf.Atan2(Mathf.Abs(yNave - y), Mathf.Abs(xNave - x));
                    MovTiroInimigo();
                }                
            }        
        }        
    }

    private void MovTiroInimigo()
    {
        if (y > yNave)
        {
            if (x > xNave)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-10f * Mathf.Cos(teta), -10f * Mathf.Sin(teta));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(10f * Mathf.Cos(teta), -10f * Mathf.Sin(teta));
            }
        }
        else
        {
            if (x < xNave)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(10f * Mathf.Cos(teta), 10f * Mathf.Sin(teta));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-10f * Mathf.Cos(teta), 10f * Mathf.Sin(teta));
            }
        }
    }

    private void FixedUpdate()
    {
        tempo -= Time.deltaTime;
        x = transform.localPosition.x;
        y = transform.localPosition.y;

        if (tempo <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (x > 9f || x < -9f || y > 5f || y < -5f)
        {
            transform.localPosition = new Vector3(-x, -y, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    { 
        if (gameObject.CompareTag("Tiro"))
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
        }
        else if (gameObject.CompareTag("TiroInimigo"))
        {
            if (!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("Power Up"))
            {
                Destroy(col.gameObject);
            }          
        }
        Destroy(gameObject);
    }
}
