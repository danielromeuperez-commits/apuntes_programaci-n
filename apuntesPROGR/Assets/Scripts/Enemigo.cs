using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 2f; // Velocidad de movimiento del enemigo
    public float distancia = 3f;  // Distancia máxima que el enemigo recorrerá antes de cambiar de dirección
    private Vector3 posInicial;   // Posición inicial del enemigo para calcular la distancia recorrida   
    private int direccion = 1;   // Dirección actual del movimiento (1 para derecha, -1 para izquierda)

    void Start() { posInicial = transform.position; }   // Guardamos la posición inicial del enemigo al comenzar el juego

    void Update()   // En cada frame, movemos al enemigo y verificamos si ha alcanzado la distancia máxima para cambiar de dirección
    {
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);  // Movemos al enemigo en la dirección actual multiplicada por la velocidad y el tiempo entre frames

        // Cambiar dirección al llegar al límite
        if (Vector3.Distance(posInicial, transform.position) > distancia)  // Verificamos si la distancia recorrida desde la posición inicial es mayor que la distancia máxima permitida
        {
            direccion *= -1;  //    Cambiamos la dirección multiplicando por -1 (de derecha a izquierda o viceversa)
            posInicial = transform.position; // Reset para el siguiente tramo
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)     // Cuando el enemigo colisiona con otro objeto, verificamos si es el jugador para reiniciar el nivel
    {
        if (collision.gameObject.CompareTag("Player"))   // Verificamos si el objeto con el que colisionamos tiene la etiqueta "Player"
        {
            // Reinicia el nivel actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Cargamos la escena actual nuevamente para reiniciar el juego
        }
    }
}