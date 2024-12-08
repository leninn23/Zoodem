using System;
using UnityEngine;
using World;

namespace Animals.Bee
{
    public class colmena:Nido,IFood
    {
        public GameObject reinaPrefab;

        private void Start()
        {
            FoodType = IFood.FoodTypes.Beehive;
            FoodState = IFood.FoodStates.Fresh;
        }

        public float GetFoodValue()
        {
            return 4f;
        }

        public void GetEaten()
        {
            food -= 2f;
        }

        public IFood.FoodTypes FoodType { get; set; }
        public IFood.FoodStates FoodState { get; set; }
    }
}