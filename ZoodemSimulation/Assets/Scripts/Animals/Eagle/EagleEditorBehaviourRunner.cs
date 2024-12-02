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

    public void StartTravelBiome()
    {
        eagle.StartWalkToBiome();
    }
    
    public Status TravelBiome() {
        Debug.Log("Viajando...");
        return eagle.WalkObjective();
    }

    public void StartWalk()
    {
        Debug.Log("Start Walk");
        eagle.StartWalkRandom();;
    }
    
    public Status Walk() {

        Debug.Log("Deambulando");
        return eagle.WalkObjective();
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
        return eagle.HasPartner();
    }

    public bool HasBaby()
    {
        Debug.Log("Detectando si tiene crias");
        return eagle.HasChildren();
    }

    public bool IsFemale()
    {
        Debug.Log("Detectando si es hembra");
        return eagle.IsFemale();
    }

    public bool ExistsMate() 
    {
        Debug.Log("Detectando si hay pareja");
        return eagle.FindPartner();
    }

    public void StartCourt()
    {
        eagle.FindPartner();
    }

    public void Incubate()
    {
        eagle.GenerateOffspring();
    }

    public Status Courting()
    {
        return eagle.Courting();
    }
    ///

    public void StartWalkToNest()
    {
        Debug.Log("Viajando a nido");
        eagle.StartWalkToNest();
    }

    public void AssignGender()
    {
        eagle.AssignGender();
    }

    public void FeedOffspring()
    {
        Debug.Log("Ha llegado al nido");
        eagle.FeedOffspring();
        //Ahora a criar, no?
    }


    #endregion
    #region HuntBT
    ///PERCEPTIONS
    public bool IsFoodNear()
    {
        Debug.Log("Detectando si hay comida");
        //Sin implementar el sistema de comida
        return false;
    }

    public bool PreyNear()
    {
        Debug.Log("Detectando si presa cerca");
        //Buscar presa cerca
        return eagle.PreyNear();
    }

    public bool isPreyAlone()
    {
        Debug.Log("Detectando si presa sola");
        //Look for animals of same type as prey in a distance x around prey
        return true;
    }
    ///

    public void Eat()
    {
        Debug.Log("Comiendo");
        eagle.Eat();
    }

    public void StartWalkPrey()
    {
        Debug.Log("Siguiendo a presa");
        eagle.StartWalkPrey();
    }

    public Status WalkToPrey()
    {
        return eagle.WalkPrey();
    }
    


    public void StartAttack()
    {
        Debug.Log("Preparando ataque");
        // eagle.StartAttack();
    }

    public void Protect()
    {
        Debug.Log("Protect (not implemented)");
    }

    public Status Attack()
    {
        Debug.Log("Atacando");
        eagle.Attack();
        return Status.Success;
    }
    #endregion
}
