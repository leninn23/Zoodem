using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Animals.Eagle
{
    public class EagleNido: Nido
    {
        private List<GameObject> _eggs;

        private void Start()
        {
            var a = transform.Find("Parent/Eggs");
            _eggs = new List<GameObject>();
            foreach (Transform egg in a)
            {
                _eggs.Add(a.gameObject);
                egg.gameObject.SetActive(false);
            }

            _onIncubated += ShowEggs;
            _onBorn += HideEggs;
        }

        private void ShowEggs()
        {
            var i = 0;
            foreach (GameObject egg in _eggs)
            {
                if(i >= offspringCount) return;
                i++;
                egg.SetActive(true);
            }
        }

        private void HideEggs()
        {
            foreach (var egg in _eggs)
            {
                egg.SetActive(false);
            }
        }
        
    }
}