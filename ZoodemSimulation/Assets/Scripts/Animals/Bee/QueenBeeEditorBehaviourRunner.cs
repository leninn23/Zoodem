using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animals.Bee;
using BehaviourAPI.Core;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEditor;
using UnityEngine;
using World;

public class QueenBeeEditorBehaviourRunner : EditorBehaviourRunner
{
    public Bee bee;
    public bool reproducePerDay;

    public void ResetReproduce(int f)
    {
        TimeManager.Instance.onDayChange -= ResetReproduce;
        reproducePerDay = false;
    }
    
    public bool FoodNear()
    {
        return bee.den.food > 0;
    }

   

    public bool NearWorkerBee()
    {
        return !reproducePerDay && bee.NearWorkerBee();
    }
    
    public void StartCourt()
    {
        TimeManager.Instance.onDayChange += ResetReproduce;
        bee.FindPartner();
        
        reproducePerDay = true;
        
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
        //reproducePerDay = false;
        bee.display.RemoveStatus(StatusDisplay.Statuses.Sleeping);
    }

    public void Eat()
    {
        bee.den.food -= 2f;
        bee.food += 2f;
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

    private void OnDestroy()
    {
        var abejas = FindObjectsByType<Bee>(FindObjectsSortMode.None);
        Debug.LogError(abejas.Length);
        foreach (var a in abejas)
        {
            if (gameObject != a.gameObject)
            {
               var reina = Instantiate(bee.den.GetComponent<colmena>().reinaPrefab,a.transform.position, a.transform.rotation).GetComponent<Bee>();
               reina.den = bee.den;
               Destroy(a.gameObject);
               break;
            }
        }
    }
}
