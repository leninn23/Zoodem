using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Animals
{
    public class ABasicAnimal : MonoBehaviour
    {
        public AnimalType animalType;
        [Space(10)]
        public float health;
        public float maxHealth;
        public float attackDamage;
        [Space(10)]
        public float moveSpeed;
        
        public float energy;
        public float maxEnergy;

        [Space(10)]
        public bool isFemale;
        public ABasicAnimal partner;
        public Nido den;
        [Space(5)]
        public float gestationTime; //in years
        public Vector2Int offspringNRange;
        private int _offspring;

        private void Awake()
        {
            health = maxHealth;
            energy = maxEnergy; 
        }

        public void GenerateOffspring(Nido nido)
        {
            _offspring = Random.Range(offspringNRange.x, offspringNRange.y);
            nido.offspringCount = _offspring;
            nido.timeLeftForSpawn = gestationTime;
            den = nido;
        }
    }
}