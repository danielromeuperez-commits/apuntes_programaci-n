using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private Vector3 direccion = Vector3.right; // (1,0,0) es derecha, (0,1,0) es arriba
    [SerializeField] private float distancia = 5f; // Distancia total que la plataforma se moverá desde su posición inicial
    [SerializeField] private float velocidad = 2f; // Velocidad del movimiento (cuánto tiempo tarda en completar un ciclo completo de ida y vuelta)

    private Vector3 posicionInicial; // Para almacenar la posición inicial de la plataforma

    void Start()
    {
        posicionInicial = transform.position; // Guardamos la posición inicial para usarla como referencia en el movimiento
    }

    void Update()
    {
        // Calculamos el desplazamiento usando una onda Senoidal para un movimiento suave
        float factor = Mathf.PingPong(Time.time * velocidad, distancia); // PingPong devuelve un valor que oscila entre 0 y la distancia, creando un movimiento de ida y vuelta
        transform.position = posicionInicial + (direccion.normalized * factor); // Movemos la plataforma en la dirección especificada multiplicada por el factor calculado
    }

    // Opcional: Para que el jugador se mueva CON la plataforma y no se resbale
    private void OnCollisionEnter(Collision collision) // Detecta cuando algo colisiona con la plataforma
    {
        if (collision.gameObject.CompareTag("Player")) // Si el objeto que colisiona es el jugador
        {
            collision.gameObject.transform.SetParent(transform); // Hacemos que el jugador sea hijo de la plataforma para que se mueva con ella
        }
    }

    private void OnCollisionExit(Collision collision) // Detecta cuando algo deja de colisionar con la plataforma
    {
        if (collision.gameObject.CompareTag("Player")) // Si el objeto que deja de colisionar es el jugador
        {
            collision.gameObject.transform.SetParent(null); // Deshacemos la relación padre-hijo para que el jugador ya no se mueva con la plataforma
        }
    }
}