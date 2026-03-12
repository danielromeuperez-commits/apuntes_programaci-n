using UnityEngine;
using System.Collections;

public class PlataformaDesaparece : MonoBehaviour
{
    [SerializeField] private float tiempoEspera = 1.0f; // Tiempo antes de caer
    [SerializeField] private float tiempoRegeneracion = 3.0f; // Tiempo para reaparecer

    private MeshRenderer meshRenderer; // Para controlar la visibilidad de la plataforma
    private Collider colisionador; // Para controlar la colisión de la plataforma

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>(); // Obtener el MeshRenderer para controlar la visibilidad
        colisionador = GetComponent<Collider>(); // Obtener el Collider para controlar la colisión
    }

    private void OnCollisionEnter(Collision collision) // Detecta cuando algo colisiona con la plataforma
    {
        // Verifica si el objeto que pisó tiene el tag "Player"
        if (collision.gameObject.CompareTag("Player")) // Si el objeto que colisiona es el jugador
        {
            StartCoroutine(Desaparecer()); // Inicia la rutina para hacer desaparecer la plataforma
        }
    }

    IEnumerator Desaparecer() //    Rutina para hacer desaparecer y reaparecer la plataforma
    {
        yield return new WaitForSeconds(tiempoEspera);

        // Desactivamos visual y físicamente sin destruir el objeto
        meshRenderer.enabled = false; // Oculta la plataforma
        colisionador.enabled = false; // Desactiva la colisión para que el jugador pueda caer a través de ella

        yield return new WaitForSeconds(tiempoRegeneracion); // Espera antes de reaparecer

        // Reaparece
        meshRenderer.enabled = true; // Muestra la plataforma nuevamente
        colisionador.enabled = true; // Activa la colisión para que el jugador pueda pisar la plataforma nuevamente
    }
}