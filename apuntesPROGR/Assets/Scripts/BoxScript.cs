using UnityEngine;

public class BoxScript : MonoBehaviour
{
    //este script es para los objetos que caen, para hacer que se muevan hacia abajo a una velocidad constante


    private float speedY = 1f;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Vector3.down * speedY * Time.deltaTime;
        //esto hace que el objeto se mueva a la derecha a una velocidad de 1 unidad por segundo
    }


}



