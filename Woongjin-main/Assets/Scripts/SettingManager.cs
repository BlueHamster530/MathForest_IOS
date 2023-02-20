using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingManager : MonoBehaviour
{ 
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    [SerializeField] Toggle toggleVibration;

    [SerializeField] Image imageBGM, imageSFX;
    [SerializeField] Sprite[] toggleSound;

    [SerializeField] Image imageVib;
    [SerializeField] Sprite[] toggleVib;

    public AudioMixer masterMixer;
    bool isActivated;

    private void Start()
    {
        RenewPanel();

        BGMControl();
        SFXControl();
        SetVibration();
    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        RenewPanel();

        BGMControl();
        SFXControl();
        SetVibration();
    }

    public void RenewPanel()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGM", 0.75f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFX", 0.75f);
    }

    public void BGMControl()
    {
        float sound = BGMSlider.value;

        masterMixer.SetFloat("BGM", Mathf.Log10(sound) * 20);
        PlayerPrefs.SetFloat("BGM", sound);

        imageBGM.sprite = sound <= 0.0001 ? toggleSound[0] : toggleSound[1];
        imageBGM.SetNativeSize();
    }

    public void SFXControl()
    {
        float sound = SFXSlider.value;

        masterMixer.SetFloat("SFX", Mathf.Log10(sound) * 20);
        PlayerPrefs.SetFloat("SFX", sound);

        imageSFX.sprite = sound <= 0.0001 ? toggleSound[0] : toggleSound[1];
        imageSFX.SetNativeSize();
    }

    public void SetVibration()
    {
        if (PlayerPrefs.HasKey("setting_vibration")==false)
        {
            PlayerPrefs.SetInt("setting_vibration", 1);
        }
        PlayerPrefs.SetInt("setting_vibration", toggleVibration.isOn ? 1 : 0);

        imageVib.sprite = toggleVibration.isOn ? toggleVib[1] : toggleVib[0];
        imageVib.SetNativeSize();
        Debug.Log("Vibration Set To" + PlayerPrefs.GetInt("setting_vibration"));
    }

}
