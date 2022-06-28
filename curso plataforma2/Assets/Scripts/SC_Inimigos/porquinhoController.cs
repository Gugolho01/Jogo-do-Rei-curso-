using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porquinhoController : MonoBehaviour
{
    private Rigidbody2D meuRB;
    private BoxCollider2D boxCol;
    //Boxcolider do colisor de ataque
    [SerializeField] private BoxCollider2D filhoCol;
    private Animator meuAnim;
    [SerializeField] private LayerMask layerLevel;  //pegando a layer do level
    private float velH = 2f;
    [SerializeField] private float velHMax = 2f;
    private float timerVirando = 2;
    private float timerParado = 1;
    private bool parado = false;
    private bool morto = false;

    // Start is called before the first frame update
    void Start()
    {
        //Pegando meuRB
        meuRB = GetComponent<Rigidbody2D>();

        //pegando meu boxCollider
        boxCol = GetComponent<BoxCollider2D>();

        meuAnim = GetComponent<Animator>();

        //Aplicando velocidade e movendo ele
        meuRB.velocity = new Vector2(velH, meuRB.velocity.y);
        meuRB.transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //aqui contem um timer para fazer o porquinho virar a direção
        //Só posso me mover se estou vivo
        if (!morto) { Virando(); }
    }

    //IsGrounded verifica se ele está tocando em algo em alguma direção
    private bool IsGround(int dir = 0)
    {
        //Variaveis que preciso
        Vector2 direcao = new Vector2(1f, 0f);
        float linha = .4f;

        //com o dir vamos saber qual a direção que ele quer olhar do 0 aou 3, o 4 e 5 são para verificar se tem chão
        switch (dir)
        {
            case 0:
                //Direita
                direcao = new Vector2(1f, 0f);
                break;
            case 1:
                //Cima
                direcao = new Vector2(0f, 1f);
                break;
            case 2:
                //Esquerda
                direcao = new Vector2(-1f, 0f);
                break;
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

    private void Virando()
    {
        if (!parado)
        {
            //Checando se estou me movendo e mudando a animação
            meuAnim.SetBool("parado", false);

            velH = velHMax;
            meuRB.velocity = new Vector2(velH, meuRB.velocity.y);

            if (timerVirando >= 0) timerVirando -= Time.deltaTime;

            //Verificando se há colisão na Direita ou na esquerda com o IsGround, e se eu não estou prestes a cair
            //Olhando a Direita e a esquerda e vendo se vou cair
            //Direita
            if (velH > 0)
            {
                if (IsGround(0) || !IsGround(4))
                {
                    timerVirando = 0;
                    parado = true;
                }
            }
            // Esquerda
            else if (velH < 0)
            {
                if (IsGround(2) || !IsGround(5))
                {
                    timerVirando = 0;
                    parado = true;
                }
            }

            //Virando o personagem
            if (timerVirando <= 0)
            {
                //invertendo a velH
                velH *= -1;

                if(velH != 0) { velHMax = velH; }
                
                //invertendo a imagem
                meuRB.transform.localScale = new Vector3(Mathf.Sign(velH) * -1, 1f, 1f);

                timerVirando = Random.Range(2f, 7f);
            }
        }
        else {
            velH = 0;
            meuRB.velocity = new Vector2(velH, meuRB.velocity.y);

            //Checando se estou me movendo e mudando a animação
            meuAnim.SetBool("parado", true);

            if(timerParado >= 0)timerParado -= Time.deltaTime;
            else 
            {
                timerParado = 1f;
                parado = false;
            }
            
            
        }
    }

    public void Morrendo()
    {
        morto = true;

        //Tirando minha velocidade
        meuRB.velocity = Vector2.zero;

        //Me destruindo depois de um tempo
        Destroy(gameObject, 2f);

        //Desabilitando o colisor de dano
        filhoCol.enabled = false;
    }
}
