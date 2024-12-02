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
            Insect,
            Other
        }
        
        public enum FoodStates
        {
            Alive,
            Fresh,
            Spoiled,
            Rotten,
        }

        public FoodTypes FoodType { get; }
        public FoodStates FoodState { get; }
        
    }
}