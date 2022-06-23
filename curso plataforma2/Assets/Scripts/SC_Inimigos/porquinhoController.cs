using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porquinhoController : MonoBehaviour
{
    private Rigidbody2D meuRB;
    private BoxCollider2D boxCol;
    [SerializeField] private LayerMask layerLevel;  //pegando a layer do level
    [SerializeField] private float velH = 2f;
    private float velHMax = 2f;
    [SerializeField] private float timerVirando = 2;

    // Start is called before the first frame update
    void Start()
    {
        //Pegando meuRB
        meuRB = GetComponent<Rigidbody2D>();

        //pegando meu boxCollider
        boxCol = GetComponent<BoxCollider2D>();

        //Aplicando velocidade e movendo ele
        meuRB.velocity = new Vector2(velH, meuRB.velocity.y);
        meuRB.transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //aqui contem um timer para fazer o porquinho virar a dire��o
        Virando();
    }

    //IsGrounded verifica se ele est� tocando em algo em alguma dire��o
    private bool IsGround(int dir = 0)
    {
        //Variaveis que preciso
        Vector2 direcao = new Vector2(1f, 0f);
        float linha = .4f;

        //com o dir vamos saber qual a dire��o que ele quer olhar do 0 aou 3, o 4 e 5 s�o para verificar se tem ch�o
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

            //Aqui � verificandop se tem ch�o, para ele n�o cair
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

        Debug.DrawRay(boxCol.bounds.center, direcao, Color.red);
        return chao;
    }

    private void Virando()
    {
        timerVirando -= Time.deltaTime;

        //Verificando se h� colis�o na Direita ou na esquerda com o IsGround, e se eu n�o estou prestes a cair
        if (IsGround(0) || IsGround(2))
        {
            velH = 0;
            meuRB.velocity = new Vector2(velH, meuRB.velocity.y);
        } else
        {
            velH = velHMax;
        }
        if (!IsGround(4) || !IsGround(5))
        {
            velH = 0;
            meuRB.velocity = new Vector2(velH, meuRB.velocity.y);
        } else
        {
            velH = velHMax;
        }

        if (timerVirando <= 0)
        {
            //invertendo a velH
            velH *= -1;

            meuRB.velocity = new Vector2(velH, meuRB.velocity.y);
            meuRB.transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);

            timerVirando = Random.Range(2f, 6f);
        }
    }
}