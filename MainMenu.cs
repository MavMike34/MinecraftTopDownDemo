using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject menuWorld;
    public GameObject playerMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        menuWorld.transform.Rotate(0f,2f * Time.deltaTime,0f);
    }

    public void ClickSingleplayer()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickWalkaround()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
}
