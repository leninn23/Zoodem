using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourAPI.Core;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;


public class WorkerBeeEditorBehaviourRunner : EditorBehaviourRunner
{
    public Bee bee;
    
    public bool HasNectar()
    {
        return bee.HasNectar();
    }

    public bool IsInBiome()
    {
        return bee.IsInBiome();
    }

    public bool NearFlower()
    {
        return bee.NearFlower();
    }

    public void StartWalkToNest()
    {
        bee.StartWalkToNest();
    }
    
    public void PutNectar()
    {
        bee.PutNectar();
    }

    public void StartCollect()
    {
        bee.StartCollect();
    }
    

    public void StartWalk()
    {
        bee.StartWalkRandom();
    }

    public Status Walk()
    {
        return bee.WalkObjective();
    }

    public void StartTravelBiome()
    {
        bee.StartWalkToBiome();
    }

    public Status TravelBiome()
    {
        return bee.WalkObjective();
    }

    public void StartWalkFood()
    {
        bee.StartWalkFood();
    }
    public Status Rest()
    {
        return bee.Rest();
    }
    public void ExitSleepStatus()
    {
        bee.display.RemoveStatus(StatusDisplay.Statuses.Sleeping);
    }
}

