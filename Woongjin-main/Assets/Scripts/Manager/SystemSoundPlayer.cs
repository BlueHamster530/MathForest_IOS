using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SystemSoundList { click1, correct, noncorrect, testend, click2, click3, levelup, warning, tresure}
public class SystemSoundPlayer : MonoBehaviour
{
    [SerializeField] SoundSetter SoundPrefab;

    public static SystemSoundPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    public void SystemSoundPlay(int a)
    {
        SoundSetter ss = Instantiate(SoundPrefab, transform.position, Quaternion.identity);
        ss.SoundSetup(a);
    }

    public void SystemSoundPlay(SystemSoundList sl)
    {
        SoundSetter ss = Instantiate(SoundPrefab, transform.position, Quaternion.identity);
        ss.SoundSetup((int)sl);
    }
}
