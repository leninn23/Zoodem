using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.Core;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEditor;
using UnityEngine;

public class QueenBeeEditorBehaviourRunner : EditorBehaviourRunner
{
    public Bee bee;

    public bool FoodNear()
    {
        return bee.den.food > 0;
    }

   

    public bool NearWorkerBee()
    {
        return bee.NearWorkerBee();
    }

    public bool SpaceHive()
    {
        return bee.SpaceHive();
    }

    public void StartCourt()
    {
        bee.den.freeSpace--;
        bee.Court(bee);
    }

    public Status Courting()
    {
        return bee.Courting();
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
        bee.den.food -= 0.5f;
        bee.food += 0.5f;
    }
    public void EmptyAction(){}
    public void StartWalkRandom()
    {
        bee.StartWalkRandom(StatusDisplay.Statuses.Wander);
    }
    public Status Walk()
    {
        return bee.WalkObjective();
    }
    public void StartWalkRandomNest()
    {
        bee.StartWalkRandomNest(StatusDisplay.Statuses.Wander);
    }

}
