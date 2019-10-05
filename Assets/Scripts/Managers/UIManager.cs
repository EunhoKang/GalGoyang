﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uimanager = null;
    public void Awake()
    {
        if (uimanager == null)
        {
            uimanager = this;
        }
        else if (uimanager != this)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    public List<GameObject> CanvasPrefabs;
    private List<GameObject> Canvases=new List<GameObject>();
    WaitForSeconds confirm = new WaitForSeconds(0.1f);

    private void Start()
    {
        StartCoroutine(Init());
    }


    IEnumerator Init()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UI"));
        for(int i = 0; i < CanvasPrefabs.Count; i++)
        {
            GameObject temp = Instantiate(CanvasPrefabs[i]);
            Canvases.Add(temp);
            temp.SetActive(false);
            yield return null;
        }
        ShowCanvas(0);
    }

    public void RemoveCanvas(int index)
    {
        Canvases[index].SetActive(false);
    }
    public void ShowCanvas(int index)
    {
        Canvases[index].SetActive(true);
    }

    IEnumerator RestartCoroutine()
    {
        RemoveCanvas(2);
        RemoveCanvas(4);
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        yield return null;
        CharacterManager.charmanager.Init();
        MapManager.mapmanager.Init();
        ShowCanvas(2);
    }

    public void GameRestart()
    {
        StartCoroutine(RestartCoroutine());
    }
}
