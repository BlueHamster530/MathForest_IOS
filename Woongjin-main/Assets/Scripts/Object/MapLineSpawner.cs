
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLineSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] TreeObject;
    [SerializeField]
    Transform TreeParent;
    [SerializeField]
    Vector2 cellsize;
    [SerializeField]
    Vector2 cellpadding;
    [SerializeField]
    Vector3 startPos;
    [SerializeField]
    Vector2 RandomSize;
    
    void Start()
    {
        for (int i = 0; i < cellsize.x; i++)
        {
            for (int ii = 0; ii < cellsize.y; ii++)
            {
                Vector3 spawnpos = startPos + new Vector3(cellpadding.x * i, cellpadding.y * ii, i);
                if (i == 0 || ii == 0 || i == cellsize.x - 1 || ii == cellsize.y - 1)
                {
                    if (i == 0 || i == cellsize.x - 1)
                    {
                        spawnpos += new Vector3(Random.Range(-RandomSize.x, RandomSize.x), 0, 0);
                    }
                    if (ii == 0 || ii == cellsize.y - 1)
                    {
                        spawnpos += new Vector3(0,Random.Range(-RandomSize.y, RandomSize.y), 0);
                    }
                    int rand = Random.Range(0, TreeObject.Length);
                    GameObject clone = Instantiate(TreeObject[rand], spawnpos, Quaternion.identity);
                    clone.GetComponent<SpriteRenderer>().sortingOrder = i;

                    clone.transform.SetParent(TreeParent);
                }
            }
        }
    }
}
