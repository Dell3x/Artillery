using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SimpleCameraSwitcher : MonoBehaviour
{
    [SerializeField] private List<Camera> _cameraList;

    private int _currentId;

    private void Start()
    {
        _currentId = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeCamera(_currentId + 1);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeCamera(_currentId - 1);
        }
    }

    private void ChangeCamera(int index)
    {
        if (index < 0)
        {
            index = _cameraList.Count;
        }

        _cameraList[_currentId].gameObject.SetActive(false);
        _cameraList[index].gameObject.SetActive(true);
        _currentId = index;
    }
}
