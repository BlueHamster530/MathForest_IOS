using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] ItemChanceData[] itemPools;

    int allItemWeight;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<itemPools.Length;i++)
        {
            allItemWeight += itemPools[i].chance;
            itemPools[i].weight = allItemWeight;
        }
    }

    public int PickItems()
    {
        int doki = Random.Range(0, allItemWeight);

        for (int i = 0; i < itemPools.Length; i++)
        {
            if(itemPools[i].weight > doki)
            {
                return itemPools[i].code;
            }
        }

        return 9999999;
    }
}
