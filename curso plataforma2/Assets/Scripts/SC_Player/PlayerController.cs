using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velH = 3;                //Velocidade
    [SerializeField] private float velV = 5;                //For?a do pulo
    private int qtdPulo = 1;
    [SerializeField] private int vida = 3;
    private float invencivel;
    private float timerInven = 2f;

    private Rigidbody2D meuRB;                              //Pegando meu RighdBody
    private Animator meuAnim;
    private bool morto = false;
    //Elementos do Raycast
    private BoxCollider2D boxCol;
    [SerializeField] private LayerMask layerLevel;
    [SerializeField] private PortaController minhaPorta;
    private GameManager meuGM;

    // Start is called before the first frame update
    void Start()
    {
        //pegando o RB altomatico
        meuRB = GetComponent<Rigidbody2D>();

        //pegando meu animator
        meuAnim = GetComponent<Animator>();

        //pegando o meu boxcollider
        boxCol = GetComponent<BoxCollider2D>();

        //Pegando a minha Vida no GameManager
        meuGM = FindObjectOfType<GameManager>();
        vida = meuGM.GetVida();
    }

    // Update is called once per frame
    void Update()
    {
        //Impedindo que se mova morto
        if (!morto) 
        {
            //Fazendo o player andar para os lados
            Movendo();
            AbrirPorta();
            Pulando();

            if (invencivel >= 0) { invencivel -= Time.deltaTime; }
        }
    }
    
    //T? funcionando, ? isso que importa
    private void FixedUpdate()
    {
        meuAnim.SetBool("noChao", IsGround(3));

    }

    public void Morto()
    {
        if (morto == false)
        {
            morto = true;
            meuRB.velocity = Vector2.zero;
        } else
        {
            morto = false;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Checando se sai da porta
        if (collision.gameObject.CompareTag("Porta"))
        {
            //Falando que n?o tenho porta, sai de perto dela
            minhaPorta = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Estou perto da porta
        if (collision.gameObject.CompareTag("Porta"))
        {
            //pegando o script da porta
            minhaPorta = collision.GetComponent<PortaController>();
        }

        //Colis?o com o inimigo
        if (collision.gameObject.CompareTag("Inimigos"))
        {
            //Fazendo ele dar uma quicada aou pular em cima do inimigo
            if(collision.transform.position.y + .2f < transform.position.y)
            {
                //Ganhando impulso
                meuRB.velocity = new Vector2(meuRB.velocity.x, velV / 2);

                //Pegando o animator do colisor e ativar o triger
                collision.GetComponentInParent<Animator>().SetTrigger("dano");

            //Fazendo ele perder vida ao colidir com o inimigo
            } else
            {
                if(invencivel <= 0)
                {
                    vida--;
                    //Informando ao meu GameManager que a vida mudou
                    meuGM.SetVida(vida);

                    //Informando ao game manager para ajustar a vida
                    meuGM.AjustandoVida();

                    //anima??o de dano
                    meuAnim.SetTrigger("dano");

                    //Informando a quantidade de vida que tenho
                    meuAnim.SetInteger("vida", vida);

                    //resetando o modo invencivel
                    invencivel = timerInven;
                }
            }
        }
    }

    private void Movendo()
    {

        //ao apertar a tecla de movimento horizontal fo Input Maneger, ele se movera
        var movi = Input.GetAxis("Horizontal") * velH;

        //Aplicando a velocidade horizontal a aou meuRB
        meuRB.velocity = new Vector2(movi, meuRB.velocity.y);

        //Alterando anima??o de parado para movendo
        if (movi != 0)
        {
            //Alterando a imagem para que ele olhe para o lado em que est? indo
            meuRB.transform.localScale = new Vector3(Mathf.Sign(movi), meuRB.transform.localScale.y, meuRB.transform.localScale.z);
        }

        //Alterando o meu movimento com base em uma condi??o dentro da fun??o, true ou false
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
        
        //Se eu toquei no ch?o eu reseto os pulos
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


    //Raycast de colis?o no ch?o
    private bool IsGround(int dir = 3)
    {
        //Variaveis que preciso
        Vector2 direcao = new Vector2(1f, 0f);
        float linha = .4f;

        //com o dir vamos saber qual a dire??o que ele quer olhar do 0 aou 3, o 4 e 5 s?o para verificar se tem ch?o
        switch (dir) { 
            case 3:
                //Baixo
                direcao = new Vector2(0f, -1f);
                break;

            //Aqui ? verificandop se tem ch?o, para ele n?o cair
            case 4:
                //Direita baixo
                direcao = new Vector2(1f, -1.6f);
                break;
            case 5:
                //Esquerda Baixo
                direcao = new Vector2(-1f, -1.6f);
                break;
        }

        bool chao = Physics2D.Raycast(boxCol.bounds.center, direcao, linha, layerLevel);

        //Criando debug para ver as dire??es com linhas vermelhas
        Debug.DrawRay(boxCol.bounds.center, direcao, Color.red);
        return chao;
    }

    //metodo para abrir a porta
    private void AbrirPorta()
    {
        //S? posso abrir a porta se tenho uma porta
        if(minhaPorta != null && !morto && qtdPulo >= 1)
        {
            if (minhaPorta.TenhoDestino())
            { 
                //Checando se apertei a porta
                if (Input.GetKeyUp(KeyCode.W))
                {
                    //Abrindo a porta
                    minhaPorta.AbrindoPorta();

                    //indo para a anima??o entrando na porta
                    meuAnim.SetTrigger("entraPorta");
                    
                    //Definindo como morto para ficar parado
                    morto = true;
                    meuRB.velocity = Vector2.zero;

                    invencivel = 5f;
                }
            }
        }
    }
    
    private void SaindoCena()
    {
        //Abrindo a porta
        minhaPorta.IndoDestino();
        
    }

    public void Riniciando()
    {
        meuGM.GameOver();
    }
}
