using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    //Este script es para el personaje del jugador, para controlar su movimiento y animaciones


    //Aquí están los componentes definidos.
    //Estos serán los que necesitamos manipular
    //PERO NO BASTA CON DEFINIRLOS SOLAMENTE
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator animator;
    Vector2 movementImput;
    //public float speedX; es equivalente a lo de abajo
    [SerializeField] private float speedX;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool IsGrounded;


    void Start()
    {
        //HAY QUE INICIALIZARLOS AQUÍ
        //EL ESQUEMA ES:
        //componente = GetComponent<TIPO_DE_COMPONENTE>();
        rb = GetComponent<Rigidbody2D>(); // rigidbody2D INICIALIZADO
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        IsGrounded = false;

        //Desactva
        Physics2D.queriesStartInColliders = false;
        //Esto hace que el raycast no detecte el collider del personaje, sino que empiece a detectar a partir de ahí
    }

    void Update()
    {
        //La variable playerInput tiene un actions
        //en ese actions podemos acceder a las acciones definidas en el InputActions map
        //en este caso "Move"
        //Si hacemos esto, nos guardamos el resultado en ningun sitio
        //playerInput.actions["Move"].ReadValue<Vector2>();
        movementImput = playerInput.actions["Move"].ReadValue<Vector2>(); //movimiento

        animator.SetFloat("VelocityX", Mathf.Abs(movementImput.x)); //animaciones

        //Esto hace que el personaje se voltee dependiendo de la dirección en la que se mueva, para que no se vea raro

        if (movementImput.x > 0)
        {
            transform.localScale = new Vector3(1, 1);
        }
        else if (movementImput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1);
        }


    }
    private void FixedUpdate()
    {
        //Todo lo que tenga que ver con el rigidbody2D tenemos que usar rb
        rb.linearVelocityX = movementImput.x * speedX;
        //Aquí se le asigna una velocidad al rigidbody2D, multiplicando el input por un número para que no sea tan lento

        //Detectar el suelo con Raycast
        //Necesitamos un punto de origen, que va a ser el centro del personaje
        Vector3 origin = transform.position;
        //Dirección para emitir el rayo y longitud del rayo

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.47F);

        //Si el rayo colisiona con algo, entonces el personaje está en el suelo

        if (hit.collider != null)
        {
            IsGrounded = true;
            animator.SetBool("IsGrounded", false); //animaciones
        }
        else
        {
            IsGrounded = false;
            animator.SetBool("IsGrounded", true); //animaciones
        }
    }
    //Esta función se llama desde el InputActions map cuando se presiona el botón de salto, para hacer que el personaje salte
    public void Jump(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            //3 fases started, performed y canceled
            if (contex.performed)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    //Esta función se llama cuando el personaje colisiona con un objeto, para hacer que el personaje se quede pegado al objeto si es una caja
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Box"))
        {
            this.transform.SetParent(collision.gameObject.transform);
        }
    }
    //Esta función se llama cuando el personaje deja de colisionar con un objeto, para hacer que el personaje se despegue del objeto si es una caja
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Box"))
        {
            this.transform.SetParent(null);
        }

    }
}