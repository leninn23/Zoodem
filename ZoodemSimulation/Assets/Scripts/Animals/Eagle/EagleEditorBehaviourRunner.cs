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
        return eagle.partner;
    }

    public bool HasBaby()
    {
        Debug.Log("Detectando si tiene crias");
        return eagle.HasChildren();
    }

    public bool IsFemale()
    {
        Debug.Log("Detectando si es hembra");
        return eagle.isFemale;
    }

    public bool ExistsMate() 
    {
        Debug.Log("Detectando si hay pareja");
        return eagle.HasPartner();
    }
    ///

    public void StartWalkToNest()
    {
        Debug.Log("Viajando a nido");
        eagle.StartWalkToNest();
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
        return false;
    }

    public bool IsPreyAlone()
    {
        Debug.Log("Detectando si presa sola");
        //Look for animals of same type as prey in a distance x around prey
        return false;
    }
    ///

    public void Eat()
    {
        Debug.Log("Comiendo");
        //eagle.Eat();
    }

    public void FollowPrey()
    {
        Debug.Log("Siguiendo a presa");
        //eagle.FollowPrey();
    }

    public void StartAttack()
    {
        Debug.Log("Preparando ataque");
        //eagle.StartAttack();
    }

    public void Attack()
    {
        Debug.Log("Atacando");
        //eagle.Attack();
    }
    #endregion
}
