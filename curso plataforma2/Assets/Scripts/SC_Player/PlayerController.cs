using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velH = 3;                //Velocidade
    [SerializeField] private float velV = 5;                //Força do pulo
    [SerializeField] private int qtdPulo = 1;

    private Rigidbody2D meuRB;                              //Pegando meu RighdBody
    private Animator meuAnim;

    //Elementos do Raycast
    private BoxCollider2D boxCol;
    [SerializeField] private LayerMask layerLevel;

    // Start is called before the first frame update
    void Start()
    {
        //pegando o RB altomatico
        meuRB = GetComponent<Rigidbody2D>();

        //pegando meu animator
        meuAnim = GetComponent<Animator>();

        //pegando o meu boxcollider
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Fazendo o player andar para os lados
        Movendo();

        Pulando();
    }
    private void FixedUpdate()
    {
<<<<<<< HEAD
        //meuAnim.SetBool("noChao", IsGround(3) );
=======
        meuAnim.SetBool("noChao", IsGrounded());

        //Se eu toquei no chão eu reseto os pulos
        if (IsGrounded())
        {
            qtdPulo = 1;
        }
>>>>>>> parent of 627c8cd (Fechando o dia)
    }

    private void Movendo()
    {

        //ao apertar a tecla de movimento horizontal fo Input Maneger, ele se movera
        var movi = Input.GetAxis("Horizontal") * velH;

        //Aplicando a velocidade horizontal a aou meuRB
        meuRB.velocity = new Vector2(movi, meuRB.velocity.y);

        //Alterando animação de parado para movendo
        if (movi != 0)
        {
            //Alterando a imagem para que ele olhe para o lado em que está indo
            meuRB.transform.localScale = new Vector3(Mathf.Sign(movi), meuRB.transform.localScale.y, meuRB.transform.localScale.z);
        }

        //Alterando o meu movimento com base em uma condição dentro da função, true ou false
        meuAnim.SetBool("movendo", movi != 0);
        
    }

    private void Pulando()
    {
        //aplicando o pulo o pulo
        var jump = Input.GetButtonDown("Jump");

        //Definindo o parametro so velV com base na minha velocidade Y do meuRB
        meuAnim.SetFloat("Velv", meuRB.velocity.y);

        if (jump && qtdPulo > 0)
        {
            qtdPulo --;

            //Subindo o player
            meuRB.velocity = new Vector2(meuRB.velocity.x, velV);

            //modificando a sprite
            //meuAnim.SetBool("noChao", false);
        }

        //Se eu toquei no chão eu reseto os pulos
        if (IsGround(3) || IsGround(4) || IsGround(5))
        {
            meuAnim.SetBool("noChao", true);
            qtdPulo = 1;
        }
        else
        {
            meuAnim.SetBool("noChao", false);
            qtdPulo = 0;
        }

        if (meuRB.velocity.y <= -6 || meuRB.velocity.y == 0)
        {
            meuAnim.SetBool("noChao", true);
            qtdPulo = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Aumentando a quantidade de pulo ao tocar no chão
        /*
        if (collision.gameObject.CompareTag("Parede"))
        {
            qtdPulo = 1;

            meuAnim.SetBool("noChao", true);
        }
        */
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("Parede"))
        {
            meuAnim.SetBool("noChao", false);
        }
        */
    }

    //Raycast de colisão no chão
    private bool IsGrounded()
    {
        //Criando o meu Raycast         //Pegando os limites do meu colisor
        bool chao = Physics2D.Raycast(boxCol.bounds.center, Vector2.down, .6f, layerLevel);

        Color cor;
        //Definindo uma cor
        if (chao)
        {
            //Estou colidindo no chão
            cor = Color.red;
        } else
        {
            cor = Color.green;
        }

        //Debug linha
        //Debug.DrawRay(boxCol.bounds.center, Vector2.down * .6f, cor);

        return chao;
    }
}
