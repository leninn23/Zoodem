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
    public float food()
    {
        return 1 - bee.den.food/bee.maxFood;
    }
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

    public void Collect()
    {
        bee.StartCollect();
    }
    

    public void StartWalkFleeing()
    {
        bee.StartWalkRandom(StatusDisplay.Statuses.Fleeing);
    }
    public void StartWalkProtect()
    {
        bee.StartWalkRandom(StatusDisplay.Statuses.Protecting);
    }

   
    public void StartWalkHunting()
    {
        bee.StartWalkRandom(StatusDisplay.Statuses.Hunting);
    }

    public void StartWalkRandomNest()
    {
        bee.StartWalkRandomNest(StatusDisplay.Statuses.Wander);
    }
    public void StartWalkRandomNestProtect()
    {
        bee.StartWalkRandomNest(StatusDisplay.Statuses.Protecting);
    }    public void StartWalkRandomNestHunt()
    {
        bee.StartWalkRandomNest(StatusDisplay.Statuses.Hunting);
    }
    public void StartWalkRandom()
    {
        bee.StartWalkRandom(StatusDisplay.Statuses.Wander);
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

    public void Eat()
    {
        bee.Eatt();
    }
}

