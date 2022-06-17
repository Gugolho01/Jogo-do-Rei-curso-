using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D meuRB;                          //Pegando meu RighdBody
    [SerializeField] private float velH = 3;            //Velocidade
    private Animator meuAnim;

    // Start is called before the first frame update
    void Start()
    {
        //pegando o RB altomatico
        meuRB = GetComponent<Rigidbody2D>();

        //pegando meu animator
        meuAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Fazendo o player andar para os lados
        Movendo();
    }

    private void Movendo()
    {
        //ao apertar a tecla de movimento horizontal fo Input Maneger, ele se movera
        var movi = Input.GetAxis("Horizontal") * velH;

        //Aplicando a velocidade horizontal a aou meuRB
        meuRB.velocity = new Vector2(movi, meuRB.velocity.y);

        //Alterando animação de parado para movendo
        if(movi != 0)
        {
            //Alterando a imagem para que ele olhe para o lado em que está indo
            meuRB.transform.localScale = new Vector3(Mathf.Sign(movi), meuRB.transform.localScale.y, meuRB.transform.localScale.z);
        }

        //Alterando o meu movimento com base em uma condição dentro da função, true ou false
        meuAnim.SetBool("movendo", movi != 0);
    }
}
