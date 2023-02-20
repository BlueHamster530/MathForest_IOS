using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FindHelper;
using UnityEngine.UI;

public class LevelupItemSetter : MonoBehaviour
{
    [SerializeField] int maxCardList;   //레벨업 시 업글할 수 있는 아이템 랜덤으로 뭐뭐 뜨게 할건지
    [SerializeField] int allItemLength;
    [SerializeField] int createCount;

    [SerializeField] Transform cardParent;
    [SerializeField] UpgradeCard[] cardList;
    [SerializeField] UpgradeCard cardPrefab;

    public GameObject player;

    float cardSpread;

    void Awake()
    {        
        CardArrayCreate();
        //StartCoroutine("CardSpread");
        CardSetup();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    private void CardArrayCreate()
    {
        WeaponTemplate[] loadedWeaponData = Resources.LoadAll<WeaponTemplate>("WeaponData");

        allItemLength = loadedWeaponData.Length;
        createCount = allItemLength;

        Debug.Log(loadedWeaponData.Length + " Item Loaded");

        PlayData pData = PlayData.instance;


        for (int i=0; i<allItemLength; i++)
        {
            if (pData.weaponLevelData[loadedWeaponData[i].nWeaponCode] >= loadedWeaponData[i].optionByLevel.Length)
            {
                Debug.Log("이거 만렙이다" + loadedWeaponData[i].nWeaponCode);
                createCount--;
            }
        }
    }

    private IEnumerator CardSpread()
    {
        float current = 0;
        float percent = 0;
        cardSpread = -1920;
        HorizontalLayoutGroup hori = cardParent.GetComponentInChildren<HorizontalLayoutGroup>();

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 1f;

            hori.spacing = Mathf.Lerp(cardSpread, 0, percent);

            yield return null;
        }
    }

    public void CardSetup()
    {
        foreach (UpgradeCard card in cardList)
        {
            if (card != null)
            {
                Destroy(card.gameObject);
            }                
        }
        //-----------------------------------------------------//

        CardArrayCreate();
        createCount = Mathf.Clamp(createCount, 0, 3);

        cardList = new UpgradeCard[createCount];

        for(int i = 0; i< createCount; i++)
        {
            UpgradeCard card = Instantiate(cardPrefab);
            card.transform.SetParent(cardParent, false);

            cardList[i] = card;
        }

        RandomCardPick();
    }

    private void RandomCardPick()
    {
        List<WeaponTemplate> selectedWeapons = new List<WeaponTemplate>();
        PlayData pData = PlayData.instance;

        selectedWeapons.Clear();

        for (int j=0; j<createCount;)
        {
            InfiniteLoopDetector.Run();

            int i = Random.Range(0, allItemLength);
            Debug.Log(i);
            WeaponTemplate pickedWeapon = ItemFinder.FindItem(101 + i);

            if (selectedWeapons.Contains(pickedWeapon) || pData.weaponLevelData[pickedWeapon.nWeaponCode] >= pickedWeapon.optionByLevel.Length)
            {              
                i = Random.Range(0, allItemLength);
                pickedWeapon = ItemFinder.FindItem(101 + i);
            }
            else
            {
                selectedWeapons.Add(pickedWeapon);
                cardList[j].Setup(pickedWeapon, this);
                j++;
            }
        }
       
    }

    public void CardClear()
    {
        
        for(int i=0; i<cardList.Length; i++)
        {
            Destroy(cardList[i].gameObject);
        }
        cardList = new UpgradeCard[maxCardList];
    }

}
