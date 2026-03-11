using System.Collections;
using UnityEngine;

public class InitialBoxScript : MonoBehaviour
{

    //este script es para el primer objeto que aparece en la escena, para probar el funcionamiento de la destrucción del objeto

    void Start()
    {
        //opcional: destruir el objeto
        //Destroy(gameObject);

        //opcional: Invoke
        //Invoke("DestroyBox", 2f); //destruye el objeto después de 2 segundos

        //opcional: Corrutina
        //StartCoroutine(DestroyBoxCoroutine());
        // se pueden hacer mas cosas dentro de la corrutina, como esperar a que el jugador se acerque, etc
    }

    void Update()
    {

    }
    //esta función se llama desde el Start para destruir el objeto después de 2 segundos
    void Destruir()
    {
        Destroy(gameObject);
    }
    //esta función se llama desde el Start para destruir el objeto después de 2 segundos usando una corrutina
    IEnumerator DeatruirCorrutina()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}