using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFocus : MonoBehaviour
{
    private static CameraFocus _focusedObject;
    private bool _focused;
    private static Vector3 _fixedCameraPos;
    private static Quaternion _fixedCameraRot;
    private Transform _myCamera;
    private static Transform _mainCamera;
    private const float Speed = 1f;

    private void Start()
    {
        if(_mainCamera == null)
        {
            if (Camera.main != null) _mainCamera = Camera.main.transform;
            _fixedCameraRot = _mainCamera.rotation;
            _fixedCameraPos = _mainCamera.position;
        }
        _myCamera = transform.Find("PersonalCameraPosition");
        if (_myCamera.childCount > 0)
        {
            Destroy(_myCamera.GetChild(0).gameObject);
        }
    }

    private void Unfocus()
    {
        _focused = false;
    }
    
    public void OnMouseDown()
    {
        Debug.LogWarning("Clicked: " + name);
        if (!_focused)
        {
            _focused = true;
            if(_focusedObject)
            {
                _focusedObject.Unfocus();
            }
            _focusedObject = this;
            _mainCamera.SetPositionAndRotation(_myCamera.position, _myCamera.rotation);
            _mainCamera.SetParent(_myCamera);
        }else
        {
            _focusedObject = null;
            _focused = false;
            ResetCamera();
            // LerpCameraObject();
        }
    }

    // private void LerpCameraObject()
    // {
    //     // StopAllCoroutines();
    //     // StartCoroutine(
    //     //     MoveCamera(_myCamera.position, _myCamera.rotation));
    // }

    private void ResetCamera()
    {
        _mainCamera.SetParent(null);
        _mainCamera.SetPositionAndRotation(_fixedCameraPos, _fixedCameraRot);
        // StopAllCoroutines();
        // StartCoroutine(MoveCamera(_fixedCameraPos, _fixedCameraRot));
    }


    private void OnDestroy()
    {
        if(_myCamera && _myCamera.childCount>0)
            ResetCamera();
    }

    private void Update()
    {
        // if (_focused)
        // {
        //     _mainCamera.SetPositionAndRotation(_myCamera.position, _myCamera.rotation);
        // }
    }

    private IEnumerator MoveCamera(Vector3 objective, Quaternion objectiveRot)
    {
        var t = 0.0f;
        while (t <= 1)
        {
            var pos =Vector3.Lerp(_mainCamera.position, objective, t);
            var rot = Quaternion.Slerp(_mainCamera.rotation, objectiveRot, t);
            _mainCamera.SetPositionAndRotation(pos, rot);
            // _mainCamera.position = 
            t += Time.fixedTime * Speed;
            yield return new WaitForFixedUpdate();
        }
    }
}
