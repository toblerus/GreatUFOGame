using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private RawImage sceneFader;
    private Sequence s;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject myEventSystem;

    void Start()
    {
        startButton.onClick.AddListener(() => StartCoroutine(SwitchToGameScene()));
        exitButton.onClick.AddListener(() => closeGame());

        myEventSystem = GameObject.Find("EventSystem");
        
    }

    private void Update()
    {
        if(myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
        {
            startButton.Select();
        }
    }


    void fadeScene()
    {
        sceneFader.DOFade(1, 0.5f);
    }

    IEnumerator SwitchToGameScene()
    {
        fadeScene();
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(1);
    }

    void closeGame()
    {
        Application.Quit();
    }
}
