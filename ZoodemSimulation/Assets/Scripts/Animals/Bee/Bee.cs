using System.Collections;
using System.Collections.Generic;
using Animals;
using BehaviourAPI.Core;
using UnityEngine;

public class Bee : ABasicAnimal
{
    private float _nectar;
    public float rangeNearWorkerBee;
    // Start is called before the first frame update
    void Start()
    {
        if (den == null)
        {
            den = GameObject.Find("NidoAbeja").GetComponent<Nido>();
        }
    }

    
    public bool HasNectar()
    {
        return _nectar > 0;
    }

    public void PutNectar()
    {
        den.food += _nectar;
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

    public void Eatt()
    {
        Eat();
        _nectar += 2;
    }
    
}
