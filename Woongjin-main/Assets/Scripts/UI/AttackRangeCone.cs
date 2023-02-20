using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AttackRangeCone : MonoBehaviour
{
    [Range(0f, 360f)][SerializeField] float angle;

    [SerializeField] RectTransform Base;
    [SerializeField] RectTransform LeftBorder;
    [SerializeField] RectTransform RightBorder;

    Image c_image;

    private void Awake()
    {
        c_image = GetComponent<Image>();
    }
    private void Update()
    {
        AngleManage();
    }

    private void AngleManage()
    {
        Base.GetComponent<Image>().fillAmount = angle / 360f;

        Base.localRotation          = Quaternion.Euler(0, 0, angle / 2f);
        LeftBorder.localRotation    = Quaternion.Euler(0, 0, 180 + angle/2f + base.GetComponent<RectTransform>().rotation.z);
        RightBorder.localRotation   = Quaternion.Euler(0, 0, 180 - angle/2f + base.GetComponent<RectTransform>().rotation.z);
    }
}
