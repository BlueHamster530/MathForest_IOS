using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextEffect : MonoBehaviour
{
    [SerializeField]
    GameObject CiriticalEffect;

    [SerializeField]
    TextMeshProUGUI text;
    float currenttime;
    Vector3 vOriginyPosition;

    public void Init(int _Damage, bool _isCritical)
    {
        currenttime = 0;
        vOriginyPosition = transform.position;
        transform.localScale = Vector3.one * 0.5f;
        text.text = _Damage.ToString();
        if (_isCritical)
        {
            CiriticalEffect.SetActive(true);
            text.color = Color.red;
        }
        else
        {
            CiriticalEffect.SetActive(false);
            text.color = Color.white;
        }
    }
    private void FixedUpdate()
    {
        if (currenttime < 1.0f)
        {
            currenttime += Time.deltaTime*2.0f;
            if (currenttime >= 1.0f)
            {
                currenttime = 1.0f;
                Destroy(gameObject, 0.5f);
            }
        }
        this.transform.position = Vector3.Slerp(vOriginyPosition, vOriginyPosition + new Vector3(0, 1.0f, 0), currenttime);
        this.transform.localScale = Vector3.Slerp(Vector3.one*0.5f,Vector3.one, currenttime);
    }
}
