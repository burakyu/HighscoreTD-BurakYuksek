using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private float pieceMoveY;
    [SerializeField] private Vector3 offset;

    private Camera _mainCam;
    private Vector3 _worldMousePos;
    private DraggableObject _currentDraggable;
    private Plane _draggingPlane;

    private void Awake()
    {
        _draggingPlane = new Plane(Vector3.up, new Vector3(0, pieceMoveY, 0));
        _mainCam = Camera.main;
    }

    private void OnEnable()
    {
        EventManager.TowerPlaced.AddListener(ClearDraggable);
    }

    private void OnDisable()
    {
        EventManager.TowerPlaced.RemoveListener(ClearDraggable);
    }

    void Update()
    {
        if (_currentDraggable != null)
        {
            _worldMousePos = GetMousePosWorldPoint() + offset;
            _currentDraggable.transform.position = new Vector3(_worldMousePos.x, _worldMousePos.y, _worldMousePos.z);
        }
    }
    
    public Vector3 GetMousePosWorldPoint()
    {
        Ray ray = GetRayFromScreenPoint(Input.mousePosition);
        _draggingPlane.Raycast(ray, out float distance);

        return ray.GetPoint(distance);
    }
    
    private Ray GetRayFromScreenPoint(Vector3 screenPosition)
    {
        return _mainCam.ScreenPointToRay(screenPosition);
    }

    public void SetDraggable(DraggableObject draggable)
    {
        _currentDraggable = draggable;
    }
    
    private void ClearDraggable(GridPlacableTower gridPlacableTower, TowerGrid towerGrid)
    {
        _currentDraggable = null;
    }
}