using System.Collections;
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
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        float height = Screen.height;
        Screen.SetResolution((int)(height * 16 / 9), (int)height, false);
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UI"));
        for (int i = 0; i < CanvasPrefabs.Count; i++)
        {
            GameObject temp = Instantiate(CanvasPrefabs[i]);
            Canvases.Add(temp);
            temp.SetActive(false);
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        ShowCanvas(1);
    }

    public void RemoveCanvas(int index)
    {
        Canvases[index].SetActive(false);
    }
    public void ShowCanvas(int index)
    {
        Canvases[index].SetActive(true);
    }

    IEnumerator RestartCoroutine()//
    {
        LoadingScreen();

        CharacterManager.charmanager.ResetAll();
        MapManager.mapmanager.ResetAll();
        yield return new WaitForSeconds(0.5f);
        //SceneManager.UnloadSceneAsync("Game");
        //yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        //yield return new WaitForSeconds(1f);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));

        CharacterManager.charmanager.Init();
        MapManager.mapmanager.Init();
        for (int i = 1; i < Canvases.Count; i++)
        {
            if(i!=3)
                RemoveCanvas(i);
        }
        ShowCanvas(3);
        EndLoading();
    }
    public void GameRestart()
    {
        StartCoroutine(RestartCoroutine());
    }

    public void LoadingScreen()
    {
        ShowCanvas(0);
    }
    public void EndLoading()
    {
        RemoveCanvas(0);
    }
}
