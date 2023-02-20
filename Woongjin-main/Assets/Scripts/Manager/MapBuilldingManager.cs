using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilldingManager : MonoBehaviour
{
    [SerializeField]
    Sprite[] MapTiles;
    [SerializeField]
    GameObject MapTilePreset;
    [SerializeField]
    int nMapXSize= 10;
    [SerializeField]
    int nMapYSize = 10;
    void Start()
    {
        for(int i = 0; i < nMapXSize; i ++)
        {
            for (int ii = 0; ii < nMapYSize; ii++)
            {
                int _rand = Random.Range(0, MapTiles.Length);
                GameObject Clone = Instantiate(MapTilePreset, new Vector3(i, ii, 0), Quaternion.identity);
                Clone.transform.parent = this.transform;
                Clone.GetComponent<SpriteRenderer>().sprite = MapTiles[_rand];
            }
        }
    }
}
