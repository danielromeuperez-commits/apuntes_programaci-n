using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    // Este script controla el movimiento horizontal, el salto y el doble salto del jugador, as� como las animaciones correspondientes.(NO PROFESOR)

    public float velocidad = 5f; // Velocidad de movimiento horizontal
    public float fuerzaSalto = 10f; //  Fuerza del salto
    public int saltosMaximos = 2; // Para doble salto
    private int saltosRestantes; // 

    private Rigidbody2D rb; // Para controlar la f�sica del jugador
    private Animator anim; // Para controlar las animaciones del jugador
    private bool estaEnSuelo; //    Para verificar si el jugador est� tocando el suelo
    public Transform checkSuelo; // Un objeto vac�o a los pies del player
    public float radioCheck = 0.2f; // Radio del c�rculo para verificar el suelo
    public LayerMask capaSuelo;  // Capa que representa el suelo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtener el componente Rigidbody2D para controlar la f�sica del jugador
        anim = GetComponent<Animator>();  // Obtener el componente Animator para controlar las animaciones del jugador
        saltosRestantes = saltosMaximos;  // Inicializar los saltos restantes al m�ximo permitido
    }

    void Update()
    {
        // Movimiento Horizontal
        float movH = Input.GetAxis("Horizontal"); // Obtener el input horizontal (A/D o Flechas Izquierda/Derecha)
        rb.linearVelocity = new Vector2(movH * velocidad, rb.linearVelocity.y); // Aplicar la velocidad horizontal al Rigidbody2D, manteniendo la velocidad vertical actual

        // Animaci�n de correr (si movH != 0)
        anim.SetFloat("Speed", Mathf.Abs(movH)); // Usamos el valor absoluto de movH para que la animaci�n funcione tanto para izquierda como para derecha

        // Girar el sprite
        if (movH > 0) transform.localScale = new Vector3(1, 1, 1);  // Escala normal para mirar a la derecha
        else if (movH < 0) transform.localScale = new Vector3(-1, 1, 1);  // Escala negativa para mirar a la izquierda

        // L�gica de Suelo
        estaEnSuelo = Physics2D.OverlapCircle(checkSuelo.position, radioCheck, capaSuelo);  // Verificar si el jugador est� tocando el suelo usando un c�rculo de verificaci�n
        if (estaEnSuelo)
        {
            saltosRestantes = saltosMaximos;  // Restablecer los saltos restantes al m�ximo permitido cuando el jugador est� en el suelo
            anim.SetBool("isJumping", false); // Cambiar la animaci�n a no saltar cuando el jugador est� en el suelo
        }

        // Salto y Doble Salto (Tecla Espacio)
        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)   //    Verificar si se presiona la tecla de salto (Espacio) y si hay saltos restantes
        {
            Saltar(); // Llamar a la funci�n Saltar para aplicar la fuerza de salto y reducir los saltos restantes
        }

        // Salto con otra tecla (ejemplo: 'W')
        if (Input.GetKeyDown(KeyCode.W) && saltosRestantes > 0) // Verificar si se presiona la tecla de salto alternativa (W) y si hay saltos restantes
        {
            Saltar(); // Llamar a la funci�n Saltar para aplicar la fuerza de salto y reducir los saltos restantes
        }
    }

    void Saltar()  // Funci�n para manejar el salto del jugador
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);  // Aplicar la fuerza de salto al Rigidbody2D, manteniendo la velocidad horizontal actual
        saltosRestantes--;   // Reducir el n�mero de saltos restantes en 1
        anim.SetBool("isJumping", true);    // Cambiar la animaci�n a saltar cuando el jugador salta
    }
}