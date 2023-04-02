using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    CircleCollider2D mycollider;
    SpriteRenderer targetSprite;
    public Sprite targetBreak;

    bool ouch;
    bool visible;
    float timer = 3;

    void Awake()
    {
        targetSprite = GetComponent<SpriteRenderer>();
        mycollider = GetComponent<CircleCollider2D>();
        ouch = false;
        //once created, send to rdn place
        transform.position = Random.insideUnitCircle * 100;
        transform.rotation = Quaternion.Euler(0f,0f,Random.Range(-180f,180f));
    }

    void OnDisable(){

    }
    //tagert is not destroid, but deactived for future uses
    //once actived again, send to other place
    void OnEnable(){
        ouch = false;
        transform.position = Random.insideUnitCircle * 100;
        transform.rotation = Quaternion.Euler(0f,0f,Random.Range(-180f,180f));
    }

    void Update()
    {
        //this make object disappear after being hit
        //Time.deltatime is function base on pc clock
        if(ouch){
            timer -= 1*Time.deltaTime;
            if(timer < 0){
                gameObject.SetActive(false);
            }
        }

        if(!visible)
        return;

        //movimg target
        Vector2 move = new Vector2(Mathf.Cos(Time.time),0);
        transform.Translate(move*Time.deltaTime);

        
    }

    void OnBecameVisible(){
        visible = true;

    }
    void OnBecameInvisible(){
        visible = false;

        if(ouch)
        gameObject.SetActive(false);

    }
    
    //only actived on collision with other object with Collision class
    void OnCollisionEnter2D(Collision2D collision){
        PoolManager.SharedInstance.TargetDestroy();
        targetSprite.sprite = targetBreak;
        mycollider.enabled = false;
        ouch = true;
        timer = 3;
    }
}
