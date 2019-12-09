using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public class MapBlock
    {
        public GameObject map;
        public float mapSize;
    }

    public static MapManager mapmanager=null;
    public void Awake(){
        if(mapmanager==null)
        {
            mapmanager=this;
        }
        else if(mapmanager!=this)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
        //DontDestroyOnLoad(this);
    }

    [HideInInspector] public List<GameObject> MapOrder;
    [HideInInspector] public List<int> SizeOrder;
    [HideInInspector] public int finishMap;
    private List<MapBlock> Maps;
    private float MapPointerStart;
    private List<GameObject> MapList;
    private Vector3 MapPointer;
    private int mapindex;
    private Transform MapHolder;
    [HideInInspector] public List<GameObject> BackGroundPrefabs=new List<GameObject>();
    private List<GameObject> BackGrounds = new List<GameObject>();
    [HideInInspector] public float BackGroundWidth; 

    [HideInInspector] public AudioClip bgm;
    public AudioClip startsound;

    bool isReset=false;

    public void Init()
    {
        isReset = false;
        mapindex = 1;
        MapHolder = new GameObject("Holder").transform;
        MapHolder.position = new Vector3(0, 0, 0);
        //
        GameObject mapfile = Resources.Load("map/"+UIManager.uimanager.mapName+"_"+UIManager.uimanager.catName) as GameObject;
        MapFile temp = mapfile.GetComponent<MapFile>();
        MapOrder = temp.MapOrder;
        SizeOrder = temp.SizeOrder;
        finishMap = temp.finishMap;
        BackGroundPrefabs = temp.BackGroundPrefabs;
        BackGroundWidth = temp.BackGroundWidth;
        bgm = temp.bgm;
        //
        MapPointerStart = SizeOrder[0]*-1;
        Maps = new List<MapBlock>();

        for(int i = 0; i < MapOrder.Count; i++)
        {
            MapBlock instance = new MapBlock();
            instance.map = MapOrder[i];
            instance.mapSize = SizeOrder[i];
            Maps.Add(instance);
        }
        MapList=new List<GameObject>();
        MapPointer=new Vector3(MapPointerStart,0,0);
        BackGrounds = new List<GameObject>();
        for (int i = 0; i < BackGroundPrefabs.Count;i++)
        {
            BackGrounds.Add(Instantiate(BackGroundPrefabs[i]));
            BackGrounds[i].transform.position = new Vector3(BackGroundWidth * i, -0.6f, 0);
        }

        GameObject instance2;
        instance2 = Instantiate(Maps[0].map, MapPointer, Quaternion.identity);
        instance2.transform.SetParent(MapHolder);
        MapPointer.x += Maps[0].mapSize;
        instance2.SetActive(false);
        for (int i = 0; i < Maps.Count; i++)
        {
            instance2 = Instantiate(Maps[i].map, MapPointer, Quaternion.identity);
            instance2.transform.SetParent(MapHolder);
            MapPointer.x += Maps[i].mapSize;
            instance2.SetActive(false);
            MapList.Add(instance2);
        }
        for (int i = 0; i < 4; i++)
        {
            MapList[i].SetActive(true);
        }
        StartCoroutine(GamePlaying());
    }

    public void ResetAll()
    {
        isReset = true;
        StopCoroutine(GamePlaying());
        Destroy(MapHolder.gameObject);
        for (int i = 0; i < BackGroundPrefabs.Count; i++)
        {
            Destroy(BackGrounds[i]);
        }
    }
    IEnumerator GamePlaying(){
        SoundManager.soundmanager.BGMSet(bgm);
        SoundManager.soundmanager.SFXSet(startsound, 0);
        float playerXPos= CharacterManager.charmanager.player.transform.position.x;
        float CurrentMap=MapPointerStart+Maps[0].mapSize;
        float BGPosCount=playerXPos;
        int BGChangeCount = 0;
        Vector3 BGPlus = new Vector3(BackGroundWidth * BackGrounds.Count, 0, 0);
        while(mapindex<finishMap-1 && CharacterManager.charmanager.player!=null){
            playerXPos= CharacterManager.charmanager.player.transform.position.x;
            if(playerXPos>CurrentMap+Maps[mapindex].mapSize){
                CurrentMap += Maps[mapindex].mapSize;
                mapindex++;
                MapRefresh(mapindex);
            }
            if (playerXPos - BGPosCount >= BackGroundWidth)
            {
                BackGrounds[BGChangeCount% BackGrounds.Count].transform.position += BGPlus;
                BGChangeCount++;
                BGPosCount = playerXPos;
            }
            yield return null;
        }
        if (!isReset)
        {
            CharacterManager.charmanager.GameClear();
        }
        SoundManager.soundmanager.BGMStop();
    }

    public void PlayerEnd()
    {
        SoundManager.soundmanager.BGMStop();
        StopCoroutine("GamePlaying");
    }

    public void MapRefresh(int playerPosNum){
        if (playerPosNum - 3 >= 0)
        {
            MapList[playerPosNum - 3].SetActive(false);
        }
        if (playerPosNum+2<MapList.Count) {
            MapList[playerPosNum + 2].SetActive(true);
        }
    }
    public void MapHardRefresh(int playerPosNum){
        for(int i=0;i<Maps.Count;i++){
            MapList[i].SetActive(false);
        }
        MapList[playerPosNum - 2].SetActive(true);
        MapList[playerPosNum-1].SetActive(true);
        MapList[playerPosNum].SetActive(true);
        MapList[playerPosNum+1].SetActive(true);
        MapList[playerPosNum+2].SetActive(true);
    }
}
