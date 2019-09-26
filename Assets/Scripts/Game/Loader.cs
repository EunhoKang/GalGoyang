using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameObject MapmanagerPref;
    private GameObject Mapmanager;
    public GameObject CharmanagerPref;
    private GameObject Charmanager;
    //public GameObject soundManager;

    void Awake()
    {
        if (CharacterManager.charmanager == null)
        {
            Charmanager = Instantiate(CharmanagerPref);
        }
        if (MapManager.mapmanager == null)
        {
            Mapmanager = Instantiate(MapmanagerPref);
            //MapManager.mapmanager.cameramove = GetComponent<CameraMove>();
        }
    }
}
