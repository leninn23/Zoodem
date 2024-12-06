using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.Core;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEditor;
using UnityEngine;

public class QueenBeeEditorBehaviourRunner : EditorBehaviourRunner
{
    public Bee bee;

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
        bee.Court(bee);
    }

    public Status Courting()
    {
        return bee.Courting();
    }
    
}
