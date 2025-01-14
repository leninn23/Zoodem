using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.Core;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEngine;
using World;

public class BearEditorBehavoirRunner : EditorBehaviourRunner
{
    public Bear bear;

    public void EnterBeingCourtedState()
    {
        bear.display.PushStatus(StatusDisplay.Statuses.Courting);
    }
    public void ExitHuntingStatus()
    {
        bear.display.RemoveStatus(StatusDisplay.Statuses.Hunting);
    }
    public void EnterHuntingStatus()
    {
        
        bear.display.PushStatus(StatusDisplay.Statuses.Hunting);
    }
    public void ExitSleepStatus()
    {
        bear.display.RemoveStatus(StatusDisplay.Statuses.Sleeping);
    }
    #region CreateBurrowBT

    public bool IsInBiome()
    {
        Debug.Log("checking");
        return bear.IsInBiome();
    }

    public bool NearNest()
    {
        Debug.Log("Detectando nido");
        return bear.NearNest();
    }

    ///

    public void StartTravelBiome()
    {
        bear.StartWalkToBiome();
    }
    public Status Rest()
    {
        return bear.Rest();
    }
    public Status TravelBiome() {
        return bear.WalkObjective();
    }

    public void StartWalkProtect()
    {
        bear.StartWalkRandom(StatusDisplay.Statuses.Protecting);
    }

    

    public void StartWalkRandom()
    {
        bear.StartWalkRandom(StatusDisplay.Statuses.Wander);
    }
    public void StartWalkHunting()
    {
        bear.StartWalkRandom(StatusDisplay.Statuses.Hunting);
    }
    public void StartWalkRandomFleeing()
    {
        bear.StartWalkRandomNest(StatusDisplay.Statuses.Fleeing);
    }

    public void StartWalkRandomNest()
    {
        bear.StartWalkRandomNest(StatusDisplay.Statuses.Wander);
    }
    public void StartWalkRandomNestProtect()
    {
        bear.StartWalkRandomNest(StatusDisplay.Statuses.Protecting);
    }    public void StartWalkRandomNestHunt()
    {
        bear.StartWalkRandomNest(StatusDisplay.Statuses.Hunting);
    }


    public float Hour()
    {
        return TimeManager.Instance.TimeOfTheDay / TimeManager.LenghtOfDay;
    }
    
    public Status Walk() {

        Debug.Log("Deambulando");
        return bear.WalkObjective();
    }

    public void CreateNest() {
        Debug.Log("Crear nido");
        bear.CreateNest();
    }

    #endregion

    #region ReproductionBT

    public bool HasMate()
    {
        Debug.Log("Detectando si hay pareja");
        return bear.partner;
    }

    public bool HasBaby()
    {
        Debug.Log("Detectando si tiene crias");
        return bear.HasChildren();
    }

    public bool IsFemale()
    {
        Debug.Log("Detectando si es hembra");
        return bear.IsFemale();
    }

    public bool ExistsMate() 
    {
        Debug.Log("Detectando si hay pareja");
        return bear.FindPartner();
    }
    ///

    public void StartWalkToBurrow()
    {
        Debug.Log("Viajando a nido");
        bear.StartWalkToNest();
    }
    public void StartWalkToNest()
    {
        Debug.Log("Viajando a nido");
        bear.StartWalkToNest();
    }
    
    
    public void FeedOffspring()
    {
        Debug.Log("Ha llegado al nido");
        bear.FeedOffspring();
        //Ahora a criar, no?
    }

    public void AssignGender()
    {
        bear.AssignGender();
    }

    public void UnassignGenderAndMate()
    {
        bear.UnassignGenderAndMate();
    }

    public Status Incubate()
    {
        return bear.Incubate();
    }

    public void Court()
    {
        bear.Court(bear);
    }
    
    public void StartCourt()
    {
        bear.StartCourt();
    }
    

    public Status Courting()
    {
        return bear.Courting();
    }
    public bool IsBeingCourted()
    {
        return bear.IsBeingCourtedBool();
    }
    #endregion

    #region HuntBT

    public bool NearPrey()
    {
        return bear.PreyNear();
    }

    public Status Attack()
    {
        return bear.Attack();
    }

    #endregion

    #region RecolectBT

    public bool NearFoodCollecting()
    {
        return bear.NearFoodCollecting();
    }
    public bool NearFoodHunting()
    {
        return bear.NearFoodHunting();
    }
    public bool LowHealth()
    {
        return bear.LowHealth();
    }

    public bool Honeycomb()
    {
        return bear.Honeycomb();
    }

    public void StartCollect()
    {
        bear.StartCollect();
    }

    public Status Collect()
    {
        return bear.Collect();
    }

    #endregion

    public void EmptyAction()
    {
        //Do nothing
    }
}
