using System.Collections;
using System.Collections.Generic;
using Animals;
using BehaviourAPI.Core;
using UnityEngine;

public class Bee : ABasicAnimal
{
    private float _nectar;
    public float rangeNearWorkerBee;
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
        return FoodNear();
    }
    
    public bool NearWorkerBee()
    {
        var workerBee = Physics.OverlapSphere(transform.position, rangeNearWorkerBee, LayerMask.GetMask("Animal"));
        foreach (var w in workerBee)
        {
            if (w.TryGetComponent(out ABasicAnimal animal))
            {
                if (animal.animalType == AnimalType.Bee)//poner obrera
                {
                    partner = animal;
                    return true;
                }
            }
        }

        return false;
    }

    public bool SpaceHive()
    {
        return colmena.freeSpace > 0;
    }
}
