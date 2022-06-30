using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaController : MonoBehaviour
{
    private Animator meuAnim;

    // Start is called before the first frame update
    void Start()
    {
        meuAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrindoPorta()
    {
        meuAnim.SetTrigger("abrir");
    }
    public void FechandoPorta()
    {
        meuAnim.SetTrigger("fechar");
    }
}
