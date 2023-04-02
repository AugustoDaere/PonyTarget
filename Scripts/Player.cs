using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 3f;
    Vector3 aim;
    Transform player;
    SpriteRenderer playersprite;

    void Awake()
    {
        //Get Component on self, it can get from children if declared
        player = GetComponent<Transform>();
        playersprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(PoolManager.SharedInstance.GameOver())
        return;

        //getting Input for moviment
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        //sprite flip
        if(inputHorizontal > 0 && playersprite.flipX == false){
            playersprite.flipX = true;
        } else if(inputHorizontal < 0 && playersprite.flipX == true){
            playersprite.flipX = false;
        }

        //Moviment
        //Translate is a fuction that move object keeping the orientation
        Vector2 move = new Vector2 (inputHorizontal*speed,inputVertical*speed);
        player.Translate(move*Time.deltaTime);

        //Apple Shoot
        //calculate angle of fire and turn into rotation
        if(Input.GetButtonDown("Fire1")){
            aim = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
            Vector3 diference = aim - this.transform.position;
            float rotation = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
            FireApple(rotation);
        }

    }
    //call the Pool to use apples
    void FireApple(float rot){
        GameObject apple = PoolManager.SharedInstance.GetApple();
        if (apple != null){
            apple.transform.position = this.transform.position;
            apple.transform.rotation = Quaternion.Euler(0f,0f,rot);

            apple.SetActive(true);
        }

    }
}
