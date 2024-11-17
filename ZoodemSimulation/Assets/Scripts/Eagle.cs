using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    //public bool isInBiome;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isInBiome(){
        return true;
    }

    public void detectBiome() {
        Debug.Log("Detectando bioma");
    }

    public void travelBiome() {
        Debug.Log("Viajando a bioma");
    }

    public void walk() {

        Debug.Log("Deambulando");
    }

    public void createNest() {
        Debug.Log("Crear nido");
    }
    public void nearNest() {
        Debug.Log("Detectando nido");
    }


}

