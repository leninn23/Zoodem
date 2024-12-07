using System.Collections;
using System.Collections.Generic;
using Animals;
using BehaviourAPI.Core;
using UnityEngine;
using World;

public class Bear : ABasicAnimal
{
    public List<TimeManager.Season> huntingSeasons;
    public bool IsHuntingSeason()
    {
        return huntingSeasons.Contains(TimeManager.Instance.currentSeason);
    }
    void Start()
    {
        animalType = AnimalType.Bear;
    }

    public void UnassignGenderAndMate()
    {
        partner = null;
        gender = GenderAnimal.unassigned;
    }


    public bool NearFoodCollecting()
    {
        foodPreferences.Clear();
        foodPreferences.Add(IFood.FoodTypes.Berry);
        foodPreferences.Add(IFood.FoodTypes.Beehive);
        return FoodNear();
    }
    public bool NearFoodHunting()
    {
        foodPreferences.Clear();
        foodPreferences.Add(IFood.FoodTypes.Meat);
        return FoodNear();
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
