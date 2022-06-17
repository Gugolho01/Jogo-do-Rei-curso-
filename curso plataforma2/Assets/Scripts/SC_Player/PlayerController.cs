using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D meuRB;                          //Pegando meu RighdBody
    [SerializeField] private float velocidade = 3;      //Velocidade

    // Start is called before the first frame update
    void Start()
    {
        //pegando o RB altomatico
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Fazendo o player andar para os lados
        var horizontal = Input.GetAxis("Horizontal") * velocidade;

        meuRB.velocity = new Vector2(horizontal, meuRB.velocity.y);
    }
}
