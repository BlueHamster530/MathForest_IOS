using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyCheck_Activator : MonoBehaviour
{
    [SerializeField] GameObject[] enemys;
    [SerializeField] PlayableDirector myGo;

    bool isClear;

    private void Update()
    {
        if (isClear) return;

        foreach(GameObject en in enemys)
        {
            if (en != null && en.TryGetComponent<EnemyController>(out EnemyController enc)) return;
        }

        isClear = true;
        myGo.Play();
    }
}
