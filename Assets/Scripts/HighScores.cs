using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    public Text[] nomes;
    public Text[] scores;
    private int qtd;
    private string proximo;

    void Start()
    {
        qtd = PlayerPrefs.GetInt("qtd");
        if (qtd > 0)
        {
            for (int i = 1; i <= qtd; i++)
            {
                proximo = PlayerPrefs.GetString(i.ToString());
                if(proximo.Length > 8)
                {
                    nomes[i - 1].text = i + "º  " + proximo.Substring(0, 8);
                }
                else
                {
                    nomes[i - 1].text = i + "º  " + proximo;
                }
                
                scores[i - 1].text = PlayerPrefs.GetInt(proximo).ToString();
            }
        }
    }
}
