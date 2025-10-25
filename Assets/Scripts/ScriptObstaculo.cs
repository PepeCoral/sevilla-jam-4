using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;
using UnityEngine;
using System.Collections;

public class ScriptObstaculo : MonoBehaviour
{
    private Transform puntoA;   // Primer punto
    private Transform puntoB;   // Segundo punto
    private bool isMoving = true;
    public float velocidad = 2f;
    public float tiempoReset = 2f;

    private Vector3 objetivo;  // A dónde va el objeto ahora

    void Start()
    {
        puntoA = transform.parent;
        puntoB = transform.Find("Objetivo");
        objetivo = puntoB.position;
    }

    void Update()
    {
        // Mover el objeto hacia el objetivo
        if (isMoving)
            transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);

        // Si llega al punto destino, cambia la dirección
        if (Vector3.Distance(transform.position, objetivo) < 0.1f)
        {
            if (objetivo == puntoA.position)
            {

                objetivo = puntoB.position;
                StartCoroutine(ExampleCoroutine());
            }
            else
            {
                objetivo = puntoA.position;
                StartCoroutine(ExampleCoroutine());
               
            }
        }
    }


    IEnumerator ExampleCoroutine()
    {
        isMoving = !isMoving;
        yield return new WaitForSeconds(tiempoReset);
        isMoving = !isMoving;
    }
}
