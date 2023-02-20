using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderStatus : MonoBehaviour
{
    [SerializeField] Slider mySlider;
    [SerializeField] TextMeshProUGUI textHP;

    [SerializeField] Vector3 posOffset;
    PlayerStatus pStatus;

    [SerializeField] Slider progressSlider;

    [SerializeField] Transform parent;

    public void Setup(PlayerStatus ps)
    {
        pStatus = ps;
        WaveManager.instance.waveSlider = progressSlider;
    }
    private void Update()
    {
        transform.localScale = parent.transform.localScale.x == 1 ? new Vector3(1, 1, 1) * 0.45f : new Vector3(-1, 1, 1) * 0.45f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!pStatus.gameObject.activeSelf) gameObject.SetActive(false);
        if(pStatus != null)
        {
            //transform.position = Camera.main.WorldToScreenPoint(pStatus.transform.position + posOffset);
            mySlider.value = (float)pStatus.hp / (float)pStatus.maxHp;
            textHP.text = pStatus.hp.ToString();
        }
    }
}
