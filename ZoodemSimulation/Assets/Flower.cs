using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

public class Flower : MonoBehaviour, IFood
{
    public float maxPollen;
    public float currentPollen;
    public float pollenPerEat;

    private BoxCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetFoodValue()
    {
        return pollenPerEat;
    }

    public void GetEaten()
    {
        currentPollen -= pollenPerEat;
        if (currentPollen <= 0)
        {
            currentPollen = 0;
            _collider.enabled = false;
        }
    }

    public IFood.FoodTypes FoodType { get; }
    public IFood.FoodStates FoodState { get; }
}
