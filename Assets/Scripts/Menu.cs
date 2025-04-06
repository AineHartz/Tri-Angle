using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
public void goToGame(){
    SceneManager.LoadScene("Game");
}    

public void goToHome(){
    SceneManager.LoadScene("Home");
}
public void goToLose(){
    SceneManager.LoadScene("Lose");
}
public void quit(){
    Application.Quit();
}
}
