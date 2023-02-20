using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FindHelper;
using Spine.Unity;

public class TresureBox : MonoBehaviour
{
    [SerializeField] SpriteRenderer content;
    [SerializeField] bool isLocked = false;
    [SerializeField] GameObject WrongEffect;
    [SerializeField] SkeletonAnimation anim;
    ItemPool pool;
    Animator animator;
    bool isActivated;

    PlayerAttack playerattack;
    pItem_functions playerItem;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pool = GetComponent<ItemPool>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(!isActivated)
            {
                if (playerattack == null)
                {
                    playerattack = collision.GetComponent<PlayerAttack>();
                }
                if (playerItem == null)
                {
                    playerItem = collision.GetComponent<pItem_functions>();
                }
                isActivated = true;

                if (isLocked)
                {
                    CreateQuestion();
                }
                else GetItem();                  
            }
            
        }
    }

    private void CreateQuestion()
    {
        QuestionManager.instance.GetQuestion(this); 
    }

    private void GetItem()
    {
        animator.SetTrigger("Get");
        if(anim != null)
            anim.AnimationState.SetAnimation(0, "open", false);
        int code = pool.PickItems();

        if(code <= 200)
        {
            playerattack.WeaponSetup(code);           
        }
        else
        {
            playerItem.GetItem(code);
        }
        content.sprite = ItemFinder.FindItemSprite(code);
        if (SystemSoundPlayer.instance != null) SystemSoundPlayer.instance.SystemSoundPlay(SystemSoundList.tresure);


    }
    public void QuestionAnswerRight()
    {
        GetItem();
    }
    public void QuestionAnswerWrong()
    {
        GameObject clone = Instantiate(WrongEffect, this.transform.position, Quaternion.identity);
        Destroy(clone, 1);        
        Destroy(gameObject);
    }
}
