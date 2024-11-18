using System.Collections;
using System.Collections.Generic;
using Animals.Eagle;
using BehaviourAPI.Core;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEngine;

public class EagleEditorBehaviourRunner : EditorBehaviourRunner
{
    public Eagle eagle;

    #region CraftingNestBT
    ///PERCEPTIONS
    public bool IsInBiome()
    {
        Debug.Log("checking");
        return eagle.IsInBiome();
    }

    public bool NearNest()
    {
        Debug.Log("Detectando nido");
        return eagle.NearNest();
    }
    ///
    
    public Status TravelBiome() {
        
        Debug.Log("Viajando a bioma");
        return eagle.TravelBiome();;
    }

    public void StartWalk()
    {
        Debug.Log("Start Walk");
        eagle.StartWalk();;
    }
    
    public Status Walk() {

        Debug.Log("Deambulando");
        return eagle.Walk();
    }

    public void CreateNest() {
        Debug.Log("Crear nido");
        eagle.CreateNest();
    }

    #endregion

    #region ReproductionBT
    ///PERCEPTIONS
    public bool HasMate()
    {
        Debug.Log("Detectando si hay pareja");
        return eagle.partner;
    }

    public bool HasBaby()
    {
        Debug.Log("Detectando si tiene crias");
        return eagle.den.offspringCount > 0;
    }

    public bool IsFemale()
    {
        Debug.Log("Detectando si es hembra");
        return eagle.isFemale;
    }

    public bool ExistsMate() 
    {
        Debug.Log("Detectando si hay pareja");
        return false;
    }
    ///

    public void StartWalkToNest()
    {
        Debug.Log("Viajando a nido");
        //eagle.StartWalkToNest();
    }

    public void OnNestReached()
    {
        Debug.Log("Ha llegado al nido");
        //eagle.OnNestReached();
    }


    #endregion
}
