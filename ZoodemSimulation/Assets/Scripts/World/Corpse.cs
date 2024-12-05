using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

public class Corpse : MonoBehaviour, IFood
{
    public float foodValue;
    // Start is called before the first frame update
    void Start()
    {
        FoodType = IFood.FoodTypes.Meat;
        FoodState = IFood.FoodStates.Fresh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetFoodValue()
    {
        return foodValue;
    }

    public void GetEaten()
    {
        Destroy(gameObject);
    }

    public IFood.FoodTypes FoodType { get; private set; }
    public IFood.FoodStates FoodState { get; private set; }
}
