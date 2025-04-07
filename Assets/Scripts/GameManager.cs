using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int score = 0;
    static public TMP_Text scoreText;

    void Awake(){
        if(Instance == null) {
            Instance = this;
            scoreText = GetComponentInParent<TMP_Text>();
        } else {
            Destroy(gameObject);
        }
    }
    public static void addScore(int amount){
        score += amount;
        UpdateUI();
    }
    static void UpdateUI(){
        scoreText.text = "Score " + score.ToString();
    }
}
