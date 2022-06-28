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
        meuAnim.SetBool("noChao", IsGround(3));

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

        if (meuRB.velocity.y == 0)
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

    //Raycast de colisão no chão
    private bool IsGround(int dir = 3)
    {
        //Variaveis que preciso
        Vector2 direcao = new Vector2(1f, 0f);
        float linha = .4f;

        //com o dir vamos saber qual a direção que ele quer olhar do 0 aou 3, o 4 e 5 são para verificar se tem chão
        switch (dir) { 
            case 3:
                //Baixo
                direcao = new Vector2(0f, -1f);
                break;

            //Aqui é verificandop se tem chão, para ele não cair
            case 4:
                //Direita baixo
                direcao = new Vector2(1f, -1f);
                break;
            case 5:
                //Esquerda Baixo
                direcao = new Vector2(-1f, -1f);
                break;
        }

        bool chao = Physics2D.Raycast(boxCol.bounds.center, direcao, linha, layerLevel);

        //Criando debug para ver as direções com linhas vermelhas
        Debug.DrawRay(boxCol.bounds.center, direcao, Color.red);
        return chao;
    }
}
