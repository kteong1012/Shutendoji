using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RockingBar : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Serialize Fields
    [SerializeField] private RectTransform _bigDraggerTran;
    [SerializeField] private RectTransform _smallDraggerTran;

    [Header("Custom Settings")]
    [SerializeField] private float _radius;
    [SerializeField] private Vector2 _bigDraggerInitialPosition;
    #endregion

    #region Fields
    public static event Action<Vector2> OnRockingBarRocked;

    private Vector2 _beginDragPosition;
    #endregion

    #region Monobehaviour Life Cycle
    private void Start()
    {
        _bigDraggerTran.anchorMin = Vector2.zero;
        _bigDraggerTran.anchorMax = Vector2.zero;
        _bigDraggerTran.pivot = Vector2.one * 0.5f;
        _smallDraggerTran.anchorMin = Vector2.one * 0.5f;
        _smallDraggerTran.anchorMax = Vector2.one * 0.5f;
        _smallDraggerTran.pivot = Vector2.one * 0.5f;
    }
    #endregion

    #region Interface Implementations
    public void OnBeginDrag(PointerEventData eventData)
    {
        _beginDragPosition = eventData.position;
        _bigDraggerTran.position = eventData.position;
        _smallDraggerTran.localPosition = Vector2.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset = eventData.position - _beginDragPosition;
        if (offset.magnitude > _radius)
        {
            offset = offset.normalized * _radius;
        }
        _smallDraggerTran.localPosition = offset;
        OnRockingBarRocked?.Invoke(offset / _radius);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _bigDraggerTran.localPosition = _bigDraggerInitialPosition;
        _smallDraggerTran.localPosition = Vector2.zero;
    }
    #endregion
}
