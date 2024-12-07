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
        Debug.Log("checking " + name);
        return eagle.IsInBiome();
    }

    public bool NearNest()
    {
        Debug.Log("Detectando nido " + name);
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
    
    
    public Status Walk() {

        // Debug.Log("Deambulando " + name);
        return eagle.WalkObjective();
    }

    public void CreateNest() {
        Debug.Log("Crear nido " + name);
        eagle.CreateNest();
    }

    #endregion

    #region ReproductionBT
    ///PERCEPTIONS
    public bool HasMate()
    {
        Debug.Log("Detectando si hay pareja " + name);
        return eagle.HasPartner();
    }

    public bool HasBaby()
    {
        Debug.Log("Detectando si tiene crias " + name);
        return eagle.HasChildren();
    }

    public void EnterCourtingStatus()
    {
        eagle.display.PushStatus(StatusDisplay.Statuses.Courting);
    }
    
    public bool IsFemale()
    {
        Debug.Log("Detectando si es hembra " + name);
        return eagle.IsFemale();
    }

    public bool ExistsMate() 
    {
        Debug.Log("Detectando si hay pareja " + name);
        return eagle.FindPartner();
    }

    public void StartCourt()
    {
        eagle.StartCourt();
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
        Debug.Log("Viajando a nido " + name);
        eagle.StartWalkToNest();
    }

    public bool IsBeingCourted()
    {
        return eagle.IsBeingCourtedBool();
    }
    
    public void AssignGender()
    {
        eagle.AssignGender();
    }

    public void FeedOffspring()
    {
        Debug.Log("Ha llegado al nido " + name);
        eagle.FeedOffspring();
        //Ahora a criar, no?
    }


    #endregion
    #region HuntBT
    ///PERCEPTIONS
    public bool IsFoodNear()
    {
        Debug.Log("Detectando si hay comida");
        return eagle.FoodNear();
    }

    public Status Rest()
    {
        return eagle.Rest();
    }

    public void EnterBeingCourtedState()
    {
        eagle.display.PushStatus(StatusDisplay.Statuses.Courting);
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
        eagle.display.RemoveStatus(StatusDisplay.Statuses.Hunting);
    }


    public void ExitHuntingStatus()
    {
        eagle.display.RemoveStatus(StatusDisplay.Statuses.Hunting);
    }

    public void EnterHuntingStatus()
    {
        
        eagle.display.PushStatus(StatusDisplay.Statuses.Hunting);
    }
    public void StartWalkPrey()
    {
        Debug.Log("Siguiendo a presa");
        // eagle.StartWalkPrey();
    }

    public Status WalkToPrey()
    {
        return eagle.WalkPrey();
    }


        public void StartWalkFleeing()
        {
            eagle.StartWalkRandom(StatusDisplay.Statuses.Fleeing);
        }
    public void StartWalkProtect()
    {
        eagle.StartWalkRandom(StatusDisplay.Statuses.Protecting);
    }

    public void StartWalkRandom()
    {
        eagle.StartWalkRandom(StatusDisplay.Statuses.Wander);
    }
    public void StartWalkHunting()
    {
        eagle.StartWalkRandom(StatusDisplay.Statuses.Hunting);
    }

    public void StartWalkRandomNest()
    {
        eagle.StartWalkRandomNest(StatusDisplay.Statuses.Wander);
    }
    public void StartWalkRandomNestProtect()
    {
        eagle.StartWalkRandomNest(StatusDisplay.Statuses.Protecting);
    }    public void StartWalkRandomNestHunt()
    {
        eagle.StartWalkRandomNest(StatusDisplay.Statuses.Hunting);
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

    public void ExitSleepStatus()
    {
        eagle.display.RemoveStatus(StatusDisplay.Statuses.Sleeping);
    }
    public void EmptyAction(){}
}
