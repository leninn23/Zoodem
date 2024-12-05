using UnityEngine;

namespace StatusScripts
{
    [CreateAssetMenu(fileName = "StatusIconSO", menuName = "ScriptableObjects/Status Icon", order = 1)]
    public class ScriptableStatusIcon : ScriptableObject
    {
        public Sprite icon;
        public string description;
    }
}