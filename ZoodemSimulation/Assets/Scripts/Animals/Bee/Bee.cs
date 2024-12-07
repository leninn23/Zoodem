using System.Collections;
using System.Collections.Generic;
using Animals;
using BehaviourAPI.Core;
using UnityEngine;

public class Bee : ABasicAnimal
{
    private float _nectar;

    public Nido colmena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasNectar()
    {
        return _nectar > 0;
    }

    public void PutNectar()
    {
        colmena.food += _nectar;
        _nectar = 0;
    }

    public void StartCollect()
    {
        throw new System.NotImplementedException();
    }

    public Status Collecting()
    {
        throw new System.NotImplementedException();
    }

    public bool NearFlower()
    {
        throw new System.NotImplementedException();
    }

    public void StartWalkHive()
    {
        throw new System.NotImplementedException();
    }

    public bool NearWorkerBee()
    {
        throw new System.NotImplementedException();
    }

    public bool SpaceHive()
    {
        throw new System.NotImplementedException();
    }
}
