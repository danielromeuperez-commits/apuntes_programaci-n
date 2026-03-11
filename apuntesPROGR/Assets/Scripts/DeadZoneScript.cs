using UnityEngine;

public class DeadZoneScript : MonoBehaviour
{

    //el GameManager es el encargado de gestionar el estado del juego, como el puntaje y el Game Over

    [SerializeField] private GameObject gameManager;

    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si el objeto que colisiona con la zona de muerte es el jugador, entonces se muestra el mensaje de Game Over

        if (collision.gameObject.CompareTag("Player"))
        {
            //destruirlo
            Destroy(collision.gameObject); //desactiva el objeto en lugar de destruirlo, para poder reutilizarlo después
            gameManager.GetComponent<GameManagerScript>().SetGameOver();
            //llama a la función SetGameOver del GameManagerScript para mostrar el mensaje de Game Over
        }

        else if (collision.gameObject.name.StartsWith("Box"))
        {
           
            Destroy(collision.gameObject);
            gameManager.GetComponent<GameManagerScript>().IncreaseScore();

        }
    }
}
