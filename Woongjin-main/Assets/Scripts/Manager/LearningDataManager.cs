using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningDataManager : MonoBehaviour
{
    WJ_Conn.Learning_Data cLearning;
    static public LearningDataManager instace;
    [SerializeField] WJ_Conn scWJ_Conn;

    [SerializeField] DiagnosisManager diagnosisManager;
    // Start is called before the first frame update
    void Awake()
    {
        if (instace == null)
        {
            DontDestroyOnLoad(this);
            instace = this;

            PlayerPrefs.SetInt("PlayerDN_State", 0);
            if (diagnosisManager != null) diagnosisManager.RenewButton();

        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    public void SetLearningData(WJ_Conn.Learning_Data _value)
    {
        cLearning = _value;
    }

    public WJ_Conn.Learning_Data GetLearningData()
    {
        return cLearning;
    }
    public WJ_Conn GetWJCOnn()
    {
        return scWJ_Conn;
    }
}
