using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEffectCreator : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject ClickEffect;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject ce = Instantiate(ClickEffect, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Quaternion.identity);
        }
    }
    public void OnPointerDown(PointerEventData pd)
    {
        
    }
}
