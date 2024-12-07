using System.Collections.Generic;

namespace World
{
    public interface IFood
    {
        public enum FoodTypes
        {
            Meat,
            Vegetable,
            Fruit,
            Berry,
            Insect,
            Flower,
            Beehive,
            Nest,
            Other
        }
        
        public enum FoodStates
        {
            Alive,
            Fresh,
            Spoiled,
            Rotten,
        }

        public float GetFoodValue();
        public void GetEaten();

        public FoodTypes FoodType { get; }
        public FoodStates FoodState { get; }
        
    }
}