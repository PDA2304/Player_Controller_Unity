using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    // объект для возможности управления камеры при трансформации
    public Vector3 offset; // векторные координаты

    public bool useOffsetValues; // от какого лица
    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        { // если не от первого лица
            offset = target.position - transform.position; // вычитать начальные значения камеры от положения на экране

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate() // более позднее изменение не 
    {
        transform.position = target.position - offset; // измение положения камеры
        transform.LookAt(target); // просмотр передижения 
    }
}


