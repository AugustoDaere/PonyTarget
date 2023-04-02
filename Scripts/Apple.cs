using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    float timer;

    SpriteRenderer appleSprite;

    void Awake()
    {
        appleSprite = GetComponent<SpriteRenderer>();
    }
    void OnDisable(){
        PoolManager.SharedInstance.AccCalculation();
    }

    void OnEnable()
    {
        timer = 1;

        float rot = this.transform.rotation.z;
        //ok, dont blame me, its unity
        //for some reason rotation value is beteewn 1 and -1
        //and then is "tranlated" to degree on the inspector
        //kinda missleading, but happen
        if(rot >= 0.7f || rot  < -0.7f){
            appleSprite.flipY = true;
        } else{
            appleSprite.flipY = false;
        }
    }

    void Update()
    {

        timer -= 1*Time.deltaTime;
        if(timer >= 0){
            transform.Translate(new Vector2(5*Time.deltaTime,0));
        }
        else {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        gameObject.SetActive(false);
    }
}
