using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


// CONTADOR: Este script es para el contador de puntos, para controlar la puntuación del jugador y reiniciar el juego cuando se pierda
public class Contador : MonoBehaviour
{
    // Ahora la instancia se llama 'Contador'
    public static Contador instance; // CAMBIO AQUÍ: Renombramos la instancia a 'Contador'

    [Header("Configuración UI")] // NUEVO: Agregamos un encabezado para organizar mejor el inspector
    public TextMeshProUGUI scoreText; // CAMBIO AQUÍ: Usamos TextMeshProUGUI para el texto de puntuación

    private int score = 0; //   Puntuación actual del jugador

    void Awake() // NUEVO: Usamos Awake para inicializar la instancia antes de que otros scripts puedan acceder a ella
    {
        // Aseguramos que solo haya una instancia de Contador en la escena
        if (instance == null) 
        {
            instance = this;
        }
        // Si ya existe una instancia, destruimos el objeto duplicado para evitar conflictos
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Inicializamos el texto de puntuación al inicio del juego
        UpdateScoreText();
    }
    // Método para agregar puntos al contador
    public void AddScore(int amount)
    {
        // Incrementamos la puntuación y actualizamos el texto en pantalla
        score += amount; // NUEVO: Sumamos la cantidad de puntos al total
        UpdateScoreText(); // NUEVO: Llamamos a la función para actualizar el texto cada vez que se agregan puntos
    }
    // Método para actualizar el texto de puntuación en pantalla
    void UpdateScoreText() // NUEVO: Función para actualizar el texto de puntuación
    {
        if (scoreText != null) // NUEVO: Verificamos que el componente de texto no sea nulo antes de intentar actualizarlo
        {
            scoreText.text = "Puntos: " + score;    // NUEVO: Actualizamos el texto con la puntuación actual
        }
    }

    // Método para manejar el Game Over
    public void GameOver()
    {
        Debug.Log("¡Juego Terminado! Puntuación final: " + score);// NUEVO: Imprimimos un mensaje de Game Over en la consola con la puntuación final
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);// NUEVO: Reiniciamos la escena actual para empezar de nuevo
    }
}