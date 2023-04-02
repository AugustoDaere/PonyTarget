using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PoolManager : MonoBehaviour
{
    //This is a general game controler too

    public static PoolManager SharedInstance;

    //Game Rules varables
    float gameTime = 5;
    bool itsOver = false;
    float appleShoots = 0;
    float targetPoints = 0;

    //apple pool variables
    //public variable can be select/manipuleted in engi wich value or object they are
    //mustly used for convinience, in this case, a global class.
    public GameObject apples;
    public List<GameObject> applePool;
    public int appleAmount = 1;
    

    //target pool varables
    //originaly this could make, virtualy, infinity amount of target with only 100 target spawned
    //still doing with 5000 target for the sake of testing code
    public GameObject target;
    public List<GameObject> targetPool;
    int targetAmount = 5000;

    //UI
    public Text[] texto = new Text[4];
    public GameObject box;
    public GameObject results;
    float acc;
    float miss;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        //making pools and filling it
        applePool = new List<GameObject>();
        GameObject tmp1;
        for(int i = 0; i<appleAmount; i++){
            tmp1 = Instantiate(apples);
            tmp1.SetActive(false);
            applePool.Add(tmp1);
        }

        targetPool = new List<GameObject>();
        GameObject tmp2;
        for(int i = 0; i<targetAmount; i++){
            tmp2 = Instantiate(target);
            targetPool.Add(tmp2);
        }

        gameTime = 60f;
    }

    //game rules here
    void Update()
    {
        gameTime -= 1*Time.deltaTime;
        
        if(gameTime < 0){
            itsOver = true;
            //UI for end game, they are deactived by defalt
            box.SetActive(true);
            results.SetActive(true);
        }

        //Reset and Quit Game shortcuts
        if(Input.GetKeyUp(KeyCode.R)){
        SceneManager.LoadSceneAsync("PaprikaGame");
        }
        if(Input.GetKeyUp(KeyCode.Escape)){
        Application.Quit();
        }

        //UI
        texto[0].text = "Hits: " + targetPoints;
        texto[1].text = "Apples: " + appleShoots;
        texto[2].text = "Acc.: " + acc + "%";
        texto[3].text = "Miss:" + miss + "%";
        

    }

    public void AccCalculation(){
        if(appleShoots == 0){
            acc = 100;
            miss = 0;
        }else{
            acc = Mathf.Round((targetPoints/appleShoots)*100);
            miss = Mathf.Round(((appleShoots-targetPoints)/appleShoots)*100);
        }
    }

    //global functions
    public GameObject GetApple()
    {
        for(int i = 0; i<appleAmount; i++){
            if(!applePool[i].activeInHierarchy){
                appleShoots += 1;
                //Debug.Log(appleShoots);
                return applePool[i];
            }
        }
        return null;
    }

    public GameObject TargetDestroy(){
        targetPoints += 1;
        
        //Debug.Log(targetPoints);

        return null;
    }

    public bool GameOver(){
        if (itsOver)
        return true;
        else
        return false;
    }
}
