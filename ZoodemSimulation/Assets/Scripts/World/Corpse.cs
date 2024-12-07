using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

public class Corpse : MonoBehaviour, IFood
{
    public float foodValue;
    public float foodAge;
    // Start is called before the first frame update
    void Start()
    {
        FoodType = IFood.FoodTypes.Meat;
        FoodState = IFood.FoodStates.Fresh;
    }

    // Update is called once per frame
    void Update()
    {
        foodAge += Time.deltaTime;
        if (foodAge >= 30f)
        {
            FoodState = IFood.FoodStates.Rotten;
            GetComponent<MeshRenderer>().material.color = new Color(0.33f, 0.42f, 0.28f);
        }if (foodAge >= 60f)
        {
            Destroy(gameObject);
        }
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
