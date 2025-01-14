using System;
using BehaviourAPI.Core;
using UnityEngine;
using World;
using Random = UnityEngine.Random;

namespace Animals
{
    public class Rabbit : ABasicAnimal
    {
        private int _numberOfTurns;

        private float _timeStamp;
        private float _timeToWait;
        private static bool _createdRabbits;
        
        // Start is called before the first frame update
        private void Start()
        {
            if (!_createdRabbits)
            {
                _createdRabbits = true;
                for (int i = 0; i < 70; i++)
                {
                    SpawnRandomRabbit();
                }
            }
            StartWalkRandom(StatusDisplay.Statuses.Wander);

            _numberOfTurns = Random.Range(0, 4);

            onDeath += SpawnRandomRabbit;
        }

        // Update is called once per frame
        private void Update()
        {
            if(Time.time - _timeStamp <= _timeToWait)    return;
            
            if (WalkObjective() != Status.Running)
            {
                _numberOfTurns--;
                if (_numberOfTurns <= 0)
                {
                    _numberOfTurns = Random.Range(0, 4);
                    _timeStamp = Time.time;
                    _timeToWait = Random.Range(1f, 3f);
                    return;
                }
                StartWalkRandom(StatusDisplay.Statuses.Wander);
            }
        }
        

        public void SpawnRandomRabbit()
        {
            var pos = new Vector2Int(Random.Range(0, terrainGenerator.mapSize.x),
                Random.Range(0, terrainGenerator.mapSize.y));
            var realPos = terrainGenerator.MapPosToRealPos(pos);
            var newRabbit = Instantiate(gameObject, realPos, Quaternion.identity, transform.parent);
            newRabbit.name = "Rabbit";
        }
        
    }
}
