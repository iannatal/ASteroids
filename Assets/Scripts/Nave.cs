using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nave : MonoBehaviour
{
    private float tempoExplosao;
    private float tempoSpawn;
    private float tempoBala;

    private float x;
    private float y;
    private float velX;
    private float velY;

    public static float angulo;
    private float velMax;

    public GameObject tiro;
    public GameObject painel;
    public GameObject escudo;
    public GameObject pause;
    public Text score;
    public Text escudoTexto;
    public Text vidaTexto;

    private int qtdVidas;
    public static int qtdEscudos;
    private int pontuacao;
    public static int pontos;


    private void Start()
    {
        tempoSpawn = 3f;
        tempoExplosao = 1.5f;
        tempoBala = 0f;

        angulo = 0f;
        velMax = 10f;

        qtdVidas = 3;
        qtdEscudos = 0;
        pontos = 0;
        pontuacao = pontos;

        score.GetComponent<Text>().text = "Score: " + pontos;

        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(9, 12, false);
    }

    private void FixedUpdate()
    {

        x = transform.localPosition.x;
        y = transform.localPosition.y;
        score.text = "Score: " + pontos;
        escudoTexto.text = "x " + qtdEscudos;
        vidaTexto.text = "x " + qtdVidas;

        if (qtdVidas > 0)
        {
            if (GetComponent<Animator>().GetInteger("Nave") == 1)
            {
                tempoExplosao -= Time.deltaTime;
                if (tempoExplosao <= 0f)
                {
                    transform.localPosition = new Vector3(0f, 0f, 0f);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                    GetComponent<Rigidbody2D>().freezeRotation = true;
                    GetComponent<Rigidbody2D>().rotation = 0f;
                    GetComponent<Animator>().SetInteger("Nave", 3);
                    tempoExplosao = 1.5f;
                    angulo = 0f;
                }

            }
            else if (GetComponent<Animator>().GetInteger("Nave") == 3)
            {
                tempoSpawn -= Time.deltaTime;
                if (tempoSpawn <= 0f)
                {
                    GetComponent<Animator>().SetInteger("Nave", 0);
                    Physics2D.IgnoreLayerCollision(8, 9, false);
                    Physics2D.IgnoreLayerCollision(9, 12, false);
                    Physics2D.IgnoreLayerCollision(9, 14, false);
                    tempoSpawn = 3f;
                }
                Movimentar();
            }
            else
            {
                GetComponent<Animator>().SetInteger("Nave", 0);
                Movimentar();
            }
            if (x > 9.4f || x < -9.4f || y > 5.4f || y < -5.4f)
            {
                transform.localPosition = new Vector3(-x, -y, 0f);
            }

            if (Input.GetKey(KeyCode.P))
            {
                Time.timeScale = 0f;
                pause.SetActive(true);
            }
        }
        else
        {
            enabled = false;
            painel.SetActive(true);
            GetComponent<Animator>().SetInteger("Nave", 1);
        }

        if(tempoSpawn <= 0f)
        {
            GetComponent<Animator>().SetInteger("Nave", 0);
        }
    }

    private void Movimentar()
    {
        GetComponent<Rigidbody2D>().freezeRotation = false;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (tempoSpawn == 3f)
            {
                GetComponent<Animator>().SetInteger("Nave", 2);
            }

            GetComponent<Rigidbody2D>().velocity += new Vector2(0.2f * Mathf.Cos((angulo + 90f) * Mathf.Deg2Rad), 0.2f * Mathf.Sin((angulo + 90f) * Mathf.Deg2Rad));

            if (GetComponent<Rigidbody2D>().velocity.x > velMax || GetComponent<Rigidbody2D>().velocity.y > velMax || GetComponent<Rigidbody2D>().velocity.x < -velMax || GetComponent<Rigidbody2D>().velocity.y < -velMax)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(velMax * Mathf.Cos((angulo + 90f) * Mathf.Deg2Rad), velMax * Mathf.Sin((angulo + 90f) * Mathf.Deg2Rad));
            }
        }
        else
        {
            velX = GetComponent<Rigidbody2D>().velocity.x;
            velY = GetComponent<Rigidbody2D>().velocity.y;

            if (velX > 0f && velY > 0f)
            {
                GetComponent<Rigidbody2D>().velocity -= new Vector2(velX / 50, velY / 50);
                if (velX < 0f && velY < 0f)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                }
            }
            else if (velX > 0f && velY < 0f)
            {
                GetComponent<Rigidbody2D>().velocity -= new Vector2(velX / 50, velY / 50);
                if (velX < 0f && velY > 0f)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                }
            }
            else if (velX < 0f && velY > 0f)
            {
                GetComponent<Rigidbody2D>().velocity -= new Vector2(velX / 50, velY / 50);
                if (velX > 0f && velY < 0f)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                }
            }
            else if (velX < 0f && velY < 0f)
            {
                GetComponent<Rigidbody2D>().velocity -= new Vector2(velX / 50, velY / 50);
                if (velX > 0f && velY > 0f)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
            }
        }

        if (tempoExplosao == 1.5f)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (tempoBala <= 0f)
                {
                    Instantiate(tiro, new Vector3(x, y, 0f), Quaternion.identity);
                    tempoBala = 0.05f;
                }     
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                angulo += 5f;
                GetComponent<Rigidbody2D>().rotation = angulo;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                angulo -= 5f;
                GetComponent<Rigidbody2D>().rotation = angulo;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) && qtdEscudos > 0)
            {
                if (!escudo.activeSelf)
                {
                    escudo.SetActive(true);
                    qtdEscudos--;
                }               
            }
            tempoBala -= Time.deltaTime;
        }

        if (angulo + 90f == 360f)
        {
            angulo = -90f;
        }
        else if (angulo + 90f == 0f)
        {
            angulo = 270f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);
        GetComponent<Rigidbody2D>().freezeRotation = true;

        if (qtdVidas > 0)
        {
            if (col.gameObject.layer != 11)
            {
                qtdVidas--;
                GetComponent<Animator>().SetInteger("Nave", 1);
                Physics2D.IgnoreLayerCollision(9, 8, true);
                Physics2D.IgnoreLayerCollision(9, 12, true);
                Physics2D.IgnoreLayerCollision(9, 14, true);
            }
        }

        if (col.gameObject.CompareTag("AstGrande"))
        {
            pontos += 20;
        }
        else if (col.gameObject.CompareTag("AstMedio"))
        {
            pontos += 50;
        }
        else if (col.gameObject.CompareTag("AstPequeno") || col.gameObject.CompareTag("NaveInimiga"))
        {
            pontos += 100;
        }
        pontuacao = pontos;
        if (pontuacao >= 10000)
        {
            qtdVidas++;
            pontuacao -= 10000;
        }
    }
}


