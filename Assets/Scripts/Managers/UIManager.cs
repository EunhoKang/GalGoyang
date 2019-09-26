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
        Init();
    }

    void Init()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UI"));
        for(int i = 0; i < CanvasPrefabs.Count; i++)
        {
            GameObject temp = Instantiate(CanvasPrefabs[i]);
            Canvases.Add(temp);
            temp.SetActive(false);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
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
}
