using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{  
    TMP_Text textbox;

    void Start()
    {
        textbox = GetComponent<TMP_Text>();
        textbox.text = "Your score\n" + GameManager.score.ToString();
        //Reset score after displaying
        GameManager.score = 0;
    }
}
