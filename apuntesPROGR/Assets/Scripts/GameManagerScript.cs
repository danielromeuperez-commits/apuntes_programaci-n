using System;
using UnityEngine;
using TMPro;
public class GameManagerScript : MonoBehaviour
{
    //El GameManager es el encargado de gestionar el estado del juego, como el puntaje y el Game Over


    //Aquí están los componentes definidos.
    [SerializeField] private GameObject SpawnPoint1;
    [SerializeField] private GameObject SpawnPoint2;
    [SerializeField] private GameObject SpawnPoint3;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private TMP_Text scoreText;

    bool gameOver = false;
    int score = 0;

    
    void Start()
    {
        //Invoke("SpawnBox", 2f); //llama a la función SpawnBox después de 2 segundos

        InvokeRepeating("SpawnBox", 3f, 2f); //llama a la función SpawnBox cada 3 segundos, empezando después de 2 segundos

    }
    // Update is called once per frame
    void Update()
    {

    }
    //esta función se llama desde el DeadZoneScript cuando el jugador entra en la zona de muerte, para mostrar el mensaje de Game Over
    public void SetGameOver()
    {
        gameOver = true;
        scoreText.text = "Game Over";
    }
    //esta función se llama desde el DeadZoneScript cuando el jugador entra en la zona de muerte, para aumentar el puntaje cada vez que un objeto cae en la zona de muerte
    public void IncreaseScore()
    {
        if (gameOver == false)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
    }
    //esta función se llama cada vez que se llama a la función InvokeRepeating, para generar un objeto en una posición aleatoria entre las tres posiciones de spawn
    void SpawanBox()
    {

        int spawnPointIndex = UnityEngine.Random.Range(0, 3); //genera un número aleatorio entre 0 y 2 para elegir una de las tres posiciones de spawn
        Vector3 spawnPosition;
        switch (spawnPointIndex)
        {
            case 0:
                spawnPosition = SpawnPoint1.transform.position;
                break;
            case 1:
                spawnPosition = SpawnPoint2.transform.position;
                break;
            case 2:
                spawnPosition = SpawnPoint3.transform.position;
                break;
            default:
                spawnPosition = SpawnPoint1.transform.position;
                break;


        }

        Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
        //crea una instancia del prefab en la posición spawnPosition, sin rotación
    }




}

