using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Criando um m�todo para ir para outra cena
    public void MudaCena(string destino)
    {
        SceneManager.LoadScene(destino);
    }
}
