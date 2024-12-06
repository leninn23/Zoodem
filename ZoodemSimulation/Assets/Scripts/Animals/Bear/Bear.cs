using System.Collections;
using System.Collections.Generic;
using Animals;
using BehaviourAPI.Core;
using UnityEngine;

public class Bear : ABasicAnimal
{
    void Start()
    {
        animalType = AnimalType.Bear;
    }

    public void UnassignGenderAndMate()
    {
        partner = null;
        gender = GenderAnimal.unassigned;
    }


    public bool NearFood()
    {
        throw new System.NotImplementedException();
    }


    public void StartCollect()
    {
        throw new System.NotImplementedException();
    }

    public Status Collect()
    {
        throw new System.NotImplementedException();
    }
}   
