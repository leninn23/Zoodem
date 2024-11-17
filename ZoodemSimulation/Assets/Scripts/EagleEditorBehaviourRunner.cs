using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.Core;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEngine;

public class EagleEditorBehaviourRunner : EditorBehaviourRunner
{
    public Eagle eagle;

    public bool IsInBiome()
    {
        Debug.Log("checking");
        return eagle.IsInBiome();
    }
    
    public Status TravelBiome() {
        
        Debug.Log("Viajando a bioma");
        return eagle.TravelBiome();;
    }

    public Status Walk() {

        Debug.Log("Deambulando");
        return eagle.Walk();
    }

    public void CreateNest() {
        Debug.Log("Crear nido");
        eagle.CreateNest();
    }
    public bool NearNest() {
        Debug.Log("Detectando nido");
        return eagle.NearNest();
    }
}
