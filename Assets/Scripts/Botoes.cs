using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Botoes : MonoBehaviour
{
    public Text nome;
    public GameObject espec;
    public GameObject pause;
    private int qtd;
    private string proximo;

    public void SairPause()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void ZerarHC()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OnClick(string cena)
    {
        if (cena.Equals("Jogo"))
        {
            SceneManager.LoadScene(cena);
        }
        else if (cena.Equals("Menu"))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(cena);
        }
        else if (cena.Equals("Instrucoes"))
        {
            SceneManager.LoadScene(cena);
        }
        else if (cena.Equals("HighScores"))
        {
            if (SceneManager.GetActiveScene().name.Equals("Menu"))
            {
                SceneManager.LoadScene("HighScores");
            }
            else
            {
                if (ValidaNome(nome.text))
                {
                    qtd = PlayerPrefs.GetInt("qtd");
                    if (!PlayerPrefs.HasKey(nome.text.ToUpper()))
                    {
                        qtd++;
                        PlayerPrefs.SetInt("qtd", qtd);
                    }

                    if (qtd <= 5)
                    {
                        CriarPlacar();
                    }
                    else
                    {
                        string ultimo = PlayerPrefs.GetString(5.ToString());

                        if (PlayerPrefs.GetInt(ultimo) < Nave.pontos)
                        {
                            if (PlayerPrefs.HasKey(nome.text.ToUpper()))
                            {
                                CriarPlacar();
                            }
                            else
                            {
                                PlayerPrefs.DeleteKey(ultimo);
                                PlayerPrefs.DeleteKey(5.ToString());
                                qtd--;
                                PlayerPrefs.SetInt("qtd", qtd);
                                CriarPlacar();
                            }
                        }
                        else
                        {
                            qtd--;
                            PlayerPrefs.SetInt("qtd", qtd);
                        }
                    }
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("HighScores");
                }
                else
                {
                    nome.color = Color.red;
                    espec.SetActive(true);
                }
            }
        }
    }

    private void CriarPlacar()
    {
        if (PlayerPrefs.HasKey(nome.text.ToUpper()))
        {
            if (PlayerPrefs.GetInt(nome.text.ToUpper()) < Nave.pontos)
            {
                PlayerPrefs.SetInt(nome.text.ToUpper(), Nave.pontos);
            }
            else
            {
                return;
            }
        }
        else
        {
            PlayerPrefs.SetInt(nome.text.ToUpper(), Nave.pontos);
        }

        for (int i = 1; i <= 5; i++)
        {
            proximo = PlayerPrefs.GetString(i.ToString());
            if (proximo.Equals(""))
            {
                if (NaoExisteNome())
                {
                    PlayerPrefs.SetString(i.ToString(), nome.text.ToUpper());
                    break;
                }
            }
            else
            {
                if (PlayerPrefs.GetInt(proximo) < Nave.pontos)
                {
                    for (int j = qtd; j > i; j--)
                    {
                        if (NaoExisteNome())
                        {
                            proximo = PlayerPrefs.GetString((j - 1).ToString());
                            PlayerPrefs.SetString(j.ToString(), proximo);
                        }
                        else
                        {
                            RefazerPlacar(i);
                            break;
                        }                        
                    }
                    PlayerPrefs.SetString(i.ToString(), nome.text.ToUpper());
                    break;                
                }
            }
        }
    }

    private void RefazerPlacar(int pos)
    {
        for (int i = 1; i <= 5; i++)
        {
            proximo = PlayerPrefs.GetString(i.ToString());
            if (proximo.Equals(nome.text.ToUpper()))
            {
                for (int j = i; j > pos; j--)
                {
                    proximo = PlayerPrefs.GetString((j - 1).ToString());
                    PlayerPrefs.SetString(j.ToString(), proximo);
                }
                break;
            }
        }
    }

    private bool NaoExisteNome()
    {
        bool r = true;
        for (int k = 1; k <= 5; k++)
        {
            if (PlayerPrefs.GetString(k.ToString()).Equals(nome.text.ToUpper()))
            {
                r = false;
                break;
            }
            else if (PlayerPrefs.GetString(k.ToString()) == "")
            {
                break;
            }
        }
        return r;
    }

    private bool ValidaNome(string nome)
    {
        bool r = true;
        if (nome.Length >= 3 && nome.Length <= 8)
        {
            for (int i = 0; i < nome.Length; i++)
            {
                if (nome[i] < 65 || (nome[i] > 90 && nome[i] < 97) || nome[i] > 122)
                {
                    r = false;
                    break;
                }
            }
        }
        else
        {
            r = false;
        }
        return r;
    }
}
