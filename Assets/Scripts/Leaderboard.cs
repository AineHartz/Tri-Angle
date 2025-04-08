using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    
    TMP_Text textbox;

    void Start()
    {
        textbox = GetComponent<TMP_Text>();
        textbox.text = GameManager.score.ToString();
    }

    
}
