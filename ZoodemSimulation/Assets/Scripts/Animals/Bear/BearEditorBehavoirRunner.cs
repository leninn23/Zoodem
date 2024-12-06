using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.Core;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEngine;

public class BearEditorBehavoirRunner : EditorBehaviourRunner
{
    public Bear bear;

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
    
    public Status TravelBiome() {
        return bear.WalkObjective();
    }

    public void StartWalk()
    {
        Debug.Log("Start Walk");
        bear.StartWalkRandom();;
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
        return bear.HasPartner();
    }
    ///

    public void StartWalkToBurrow()
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

    public bool NearFood()
    {
        return bear.NearFood();
    }

    public bool LowHealth()
    {
        return bear.LowHealth();
    }

    public void Honeycomb()
    {
        bear.Honeycomb();
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
}
