 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class Loader: MonoBehaviour
{
    [SerializeField]
    private string[] sceneNames = { "MainMenu", "Game", "EndGame"};
    [SerializeField]
    private GameObject gameManager;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.Find("GameManager");
        }
    }
    public void LoadSelectedScene(int sceneSelected)
    {
        
        if (sceneSelected >= 0 && sceneSelected < sceneNames.Length)
        {
            if (sceneSelected == 2)
            {
                DontDestroyOnLoad(gameManager);
            }
            string selectedSceneName = sceneNames[sceneSelected];
            Debug.Log("Loading scene: " + selectedSceneName);
            SceneManager.LoadScene(selectedSceneName);
        }
        else
        {
            Debug.LogError("Invalid scene index!");
        }
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

}
