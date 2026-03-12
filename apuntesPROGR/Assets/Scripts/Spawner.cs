using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objetoACrear; // Prefab del objeto que se va a crear
    public float tiempoEntreSpawn = 2f; // Tiempo en segundos entre cada spawn

    void Start() // En el método Start, comenzamos a invocar el método SpawnObject repetidamente después de un retraso inicial de 1 segundo y luego cada tiempoEntreSpawn segundos
    {
        InvokeRepeating("SpawnObject", 1f, tiempoEntreSpawn); // Llama al método SpawnObject después de 1 segundo y luego cada tiempoEntreSpawn segundos
    }

    void SpawnObject() //   En el método SpawnObject, instanciamos el objetoACrear en la posición del spawner con una rotación sin cambios (Quaternion.identity)
    {
        Instantiate(objetoACrear, transform.position, Quaternion.identity); //  Crea una instancia del objetoACrear en la posición del spawner con una rotación sin cambios
    }
}