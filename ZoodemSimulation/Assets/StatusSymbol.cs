using System;
using System.Collections;
using System.Collections.Generic;
using StatusScripts;
using TMPro;
using UnityEngine;

public class StatusSymbol : MonoBehaviour
{
    private ScriptableStatusIcon _statusInfo;
    private SpriteRenderer _renderer;

    private TextMeshPro _textMeshPro;
    // Start is called before the first frame update
    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _textMeshPro = transform.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStatus(ScriptableStatusIcon statusIcon)
    {
        _statusInfo = statusIcon;
        // Debug.LogWarning($"This object {statusIcon.icon}");
        _renderer.sprite = _statusInfo.icon;
        _textMeshPro.SetText(statusIcon.description);
    }

    private void OnMouseEnter()
    {
        throw new NotImplementedException();
    }
}
