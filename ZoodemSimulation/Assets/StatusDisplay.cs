using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animals;
using AYellowpaper.SerializedCollections;
using StatusScripts;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    [Header("Prefabs")] [SerializeField] private StatusBar barPrefab;
    [SerializeField] private StatusSymbol symbolPrefab;

    [Header("Bars")]
    [SerializeField] private StatusBar healthBar;
    [SerializeField] private StatusBar energyBar;
    [SerializeField] private StatusBar foodBar;
    [Header("Other")] public float iconSize;
    public float iconSeparation;
    [SerializedDictionary("Status", "Scriptable icon")]
    public SerializedDictionary<Statuses, ScriptableStatusIcon> statusIconsDic;
    private Dictionary<Statuses, ScriptableStatusIcon> _currentStatues;
    [SerializeField] private Transform statusesParent;
    
    private ABasicAnimal _animal;
    private List<List<Statuses>> _conflictingStatuses;

    public enum Statuses
    {
        Fleeing,
        Hungry,
        Courting,
        Hunting,
        Hurt,
        GoingHome,
        Sleeping,
        Wander
    }
    
    void Start()
    {
        _animal = transform.parent.GetComponent<ABasicAnimal>();
        healthBar.SetMax(_animal.maxHealth);
        energyBar.SetMax(_animal.maxEnergy);
        energyBar.SetColors(new Color(0.75f, 0.7f, 0f), new Color(0.87f, 0.82f, 0.79f));
        foodBar.SetMax(_animal.maxFood);
        foodBar.SetColors(new Color(0.41f, 0.2f, 0.03f), new Color(0.87f, 0.82f, 0.79f));

        _currentStatues = new Dictionary<Statuses, ScriptableStatusIcon>();

        var conflictingMovement = new List<Statuses>()
        {
            Statuses.Sleeping, Statuses.GoingHome, Statuses.Hunting, Statuses.Fleeing
        };
        _conflictingStatuses = new List<List<Statuses>>();
        _conflictingStatuses.Add(conflictingMovement);
    }

    private void SetMainBars()
    {
        healthBar.SetCurrentValue(_animal.health);
        energyBar.SetCurrentValue(_animal.energy);
        foodBar.SetCurrentValue(_animal.food);
    }
    
    // Update is called once per frame
    void Update()
    {
        SetMainBars();
    }

    public void PushStatus(Statuses status)
    {
        var sts = statusIconsDic[status];
        if (_currentStatues.TryAdd(status, sts))
        {
            var symb = Instantiate(symbolPrefab, statusesParent);
            symb.SetStatus(sts);
            symb.name = sts.name;
            foreach (var conflictingStatusList in _conflictingStatuses)
            {
                if (!conflictingStatusList.Contains(status)) continue;
                
                foreach (var confStatus in conflictingStatusList.Where(confStatus => confStatus != status))
                {
                    RemoveStatus(confStatus);
                }
            }
            RestructureStatusSymbols();
        }
    }

    private void RestructureStatusSymbols()
    {
        var numberOfStatuses = statusesParent.childCount;
        var offset = iconSize + iconSeparation;
        var halfTotalOffset = (iconSize + iconSeparation )* (numberOfStatuses - 1) /2f;
        var pos = - Vector3.right * halfTotalOffset;
        foreach (Transform child in statusesParent)
        {
            child.localPosition = pos;
            pos += Vector3.right*offset;
        }
    }

    public void RemoveStatus(Statuses status)
    {
        if (_currentStatues.Remove(status))
        {
            var sts = statusIconsDic[status];
            Destroy(statusesParent.Find(sts.name).gameObject);
            //Needed since game objets aren't destroyed immediately :(
            StartCoroutine(ReestructurateEndOfFrame());
        }
    }

    private IEnumerator ReestructurateEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        RestructureStatusSymbols();
    }
}
