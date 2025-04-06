using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    // public GameObject ship;
    private Image HPSprite;

    public Sprite[] healthBarArray;
    public SpaceshipController healthScript;
    int healthNum;



    void Start()
    {
        healthNum = 3;
        healthScript = GameObject.FindWithTag("Player").GetComponent<SpaceshipController>();
        HPSprite = GetComponent<Image>();
        
    }

   
    void Update()
    {
        healthNum = healthScript.health;

        Debug.Log("Health is " + healthNum);
        if(healthNum == 3){
            
            HPSprite.sprite = healthBarArray[0];
        }
        else if(healthNum == 2){
            HPSprite.sprite = healthBarArray[1];
        }
        else if(healthNum == 1){
            HPSprite.sprite = healthBarArray[2];
        }
        else{
            HPSprite.sprite = healthBarArray[3];
        }
        
        
    }
}
