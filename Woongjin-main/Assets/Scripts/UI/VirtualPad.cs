using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualPad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] 
    Vector2 vInputVector;
    [SerializeField][Range(-10f, 10f)] 
    float vRadiusCorrection;
    Vector2 vBgPos;

    Image iBackground;
    Image iHandle;

    [Header("PCGamePadSetting")]
    [SerializeField] bool       isGamePadActivated;
    [SerializeField] string     axisNameHorizontal;
    [SerializeField] string     axisNameVertical;

    #region Property
    public Vector2 vPadOutput => vInputVector;
    #endregion


    private void Awake()
    {
        iBackground     = GetComponent<Image>();
        iHandle         = transform.GetChild(0).GetComponent<Image>();

        vBgPos = iBackground.rectTransform.anchoredPosition;
    }

    private void Update()
    {
        GamePadInput();
    }

    public void OnDrag(PointerEventData pd)
    {
        Vector2 pos;

        float moveRange = iBackground.rectTransform.sizeDelta.x / 2f;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(iBackground.rectTransform, pd.position, pd.pressEventCamera, out pos))
        {
            pos.x = Mathf.Clamp(pos.x, -moveRange, moveRange);
            pos.y = Mathf.Clamp(pos.y, -moveRange, moveRange);

            pos /= moveRange;

            vInputVector = pos.normalized;

            iHandle.rectTransform.anchoredPosition = pos.magnitude < 1.0f ? pos * (moveRange + vRadiusCorrection) : vInputVector * (moveRange + vRadiusCorrection);

        }
    }

    public void OnPointerDown(PointerEventData pd)
    {
        vInputVector = Vector3.zero;
        OnDrag(pd);
    }

    public void OnPointerUp(PointerEventData pd)
    {
        vInputVector = Vector3.zero;
        iHandle.rectTransform.anchoredPosition = Vector3.zero;
    }

    public void GamePadInput()
    {
        if (!isGamePadActivated) return;

        Vector2 padInput = new Vector2(Input.GetAxis(axisNameHorizontal),
                                       Input.GetAxis(axisNameVertical));

        vInputVector = padInput.normalized;
        //vPadOutput => vInputVector
        float moveRange = iBackground.rectTransform.sizeDelta.x / 2f;

        iHandle.rectTransform.anchoredPosition = padInput.magnitude < 1.0f ? padInput * (moveRange + vRadiusCorrection) : vInputVector * (moveRange + vRadiusCorrection);

    }
}
