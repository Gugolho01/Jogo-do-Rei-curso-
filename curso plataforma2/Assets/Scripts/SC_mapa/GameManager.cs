using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static int vidaPlayer = 3;
    [SerializeField] private int InicialVidaPlayer = 3;
    [SerializeField] private Image[] coracoes;

    // Start is called before the first frame update
    void Start()
    {
        AjustandoVida();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(vidaPlayer);
    }

    //Criando um método para ir para outra cena
    public void MudaCena(string destino)
    {
        SceneManager.LoadScene(destino);
    }

    public int GetVida() {
        return vidaPlayer;
    }

    public void SetVida(int qtdVida)
    {
        vidaPlayer = qtdVida;
        
    }

    public void GameOver()
    {
        //Resetando a Vida
        vidaPlayer = InicialVidaPlayer;

        //Indo para a primeira Cena
        SceneManager.LoadScene("SC jogo");
    }

    public void AjustandoVida()
    {
        //Rodando pelo vetor
        for (var i = 0; i < coracoes.Length; i++)
        {
            //Checando se o valor atual é maior do que a vida atual
            if (i < vidaPlayer)
            {
                //Vida
                coracoes[i].enabled = true;
            }
            else
            {
                coracoes[i].enabled = false;
            }
        }
    }
}
