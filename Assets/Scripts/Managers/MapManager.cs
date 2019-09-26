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
    
    public int StageNum;

    [Header("Maps")]
    public List<GameObject> MapOrder;
    public List<int> SizeOrder;
    private List<MapBlock> Maps;
    public float MapPointerStart=-20;
    public int finishMap;
    private List<GameObject> MapList;
    private Vector3 MapPointer;
    private int mapindex = 1;
    [Header("BackGround")]
    public List<GameObject> BackGroundPrefabs=new List<GameObject>();
    private List<GameObject> BackGrounds = new List<GameObject>();
    public float BackGroundWidth;

    [Header("Sound")]
    public AudioClip bgm;
    public AudioClip startsound;

    public void Init()
    {
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

        for(int i = 0; i < BackGroundPrefabs.Count;i++)
        {
            BackGrounds.Add(Instantiate(BackGroundPrefabs[i]));
            BackGrounds[i].transform.position = new Vector3(BackGroundWidth * i, -0.6f, 0);
        }
        GameStart();
    }
    public void GameStart(){
        GameObject instance;
        instance=Instantiate(Maps[0].map,MapPointer,Quaternion.identity);
        MapPointer.x+=Maps[0].mapSize;
        instance.SetActive(false);
        for(int i=0; i<Maps.Count; i++){
            instance =Instantiate(Maps[i].map,MapPointer,Quaternion.identity);
            MapPointer.x+=Maps[i].mapSize;
            instance.SetActive(false);
            MapList.Add(instance);
        }
        for(int i=0;i<4;i++){
            MapList[i].SetActive(true);
        }
        StartCoroutine("GamePlaying");
    }

    IEnumerator GamePlaying(){
        SoundManager.soundmanager.BGMSet(bgm);
        SoundManager.soundmanager.SFXSet(startsound, 0);
        float playerXPos= CharacterManager.charmanager.player.transform.position.x;
        float CurrentMap=MapPointerStart+Maps[0].mapSize;
        float BGPosCount=playerXPos;
        int BGChangeCount = 0;
        Vector3 BGPlus = new Vector3(BackGroundWidth * BackGrounds.Count, 0, 0);
        while(mapindex<finishMap-1){
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
        CharacterManager.charmanager.GameClear();
        SoundManager.soundmanager.BGMStop();
    }

    public void PlayerEnd()
    {
        SoundManager.soundmanager.BGMStop();
        StopCoroutine("GamePlaying");
    }

    public void MapRefresh(int playerPosNum){
        MapList[playerPosNum-2].SetActive(false);
        if (playerPosNum+2<MapList.Count) {
            MapList[playerPosNum + 2].SetActive(true);
        }
    }
    public void MapHardRefresh(int playerPosNum){
        for(int i=0;i<Maps.Count;i++){
            MapList[i].SetActive(false);
        }
        MapList[playerPosNum-1].SetActive(true);
        MapList[playerPosNum].SetActive(true);
        MapList[playerPosNum+1].SetActive(true);
        MapList[playerPosNum+2].SetActive(true);
    }
}
