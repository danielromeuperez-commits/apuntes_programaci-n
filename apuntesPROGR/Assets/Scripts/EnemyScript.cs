using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //ENEMIGO: Este script es para el enemigo, para controlar su movimiento y comportamiento al ser golpeado por el jugador
    //(este es elo el enemigo del mario)

    public float speed = 3f;
    public Color flippedColor = new Color(0.5f, 0.5f, 1f);

    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private int direction = 1;
    public bool isFlipped = false;

    void Start()
    {
        //INICIALIZAMOS LOS COMPONENTES

        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        direction = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    void Update()
    {

        //MOVIMIENTO DEL ENEMIGO

        if (isFlipped)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Si el enemigo está volteado, no se mueve horizontalmente
            return;
        }

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y); // Movimiento horizontal constante

        if (direction > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1); // Si se mueve a la derecha, aseguramos que el sprite esté orientado hacia la derecha
        else transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1); // Si se mueve a la izquierda, aseguramos que el sprite esté orientado hacia la izquierda
    }

    public void Flip()
    {
        if (isFlipped) return; // Si ya está volteado, no hacemos nada
        isFlipped = true;
        transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), 1); // Volteamos el sprite verticalmente
        spriteRend.color = flippedColor;  // Cambiamos el color para indicar que está volteado
        rb.linearVelocity = new Vector2(0, 5f);  // Le damos un impulso hacia arriba para simular el efecto de ser golpeado
        Invoke("Recover", 8f);   // Después de 8 segundos, el enemigo se recupera y vuelve a la normalidad
    }

    void Recover()
    {
        if (isFlipped) // Solo recuperamos si el enemigo está volteado
        {
            isFlipped = false;  // Volvemos a la normalidad
            spriteRend.color = Color.white;  // Restauramos el color original
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), 1);  // Restauramos la orientación original del sprite
        }
    }

    //COLISIONES DEL ENEMIGO
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el enemigo colisiona con una pared o un tubo, cambia de dirección

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Pipe")) // CAMBIO AQUÍ: Usamos CompareTag para verificar las etiquetas
        {
            direction *= -1; // Cambia la dirección multiplicando por -1
        }

        if (collision.gameObject.CompareTag("Player")) // CAMBIO AQUÍ: Usamos CompareTag para verificar la etiqueta del jugador
        {
            if (isFlipped) // Si el enemigo está volteado, el jugador lo mata
            {
                // A) EL JUGADOR MATA AL ENEMIGO -> Sumar Puntos
                if (Contador.instance != null) // CAMBIO AQUÍ: Usamos Contador
                {
                    Contador.instance.AddScore(100);  // Suma 100 puntos al contador
                }

                Destroy(gameObject);  // Destruye el enemigo
            }

            // Si el enemigo no está volteado, mata al jugador
            else
            {
                // B) EL ENEMIGO MATA AL JUGADOR -> Game Over
                Destroy(collision.gameObject);

                if (Contador.instance != null) // CAMBIO AQUÍ: Usamos Contador
                {
                    Contador.instance.GameOver(); // Llama al método GameOver del contador para mostrar el mensaje de Game Over
                }
            }
        }
    }
}
