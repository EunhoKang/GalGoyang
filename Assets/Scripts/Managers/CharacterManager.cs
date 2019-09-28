using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager charmanager = null;
    public void Awake()
    {
        if (charmanager == null)
        {
            charmanager = this;
        }
        else if (charmanager != this)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
        //DontDestroyOnLoad(this);
    }
    public CameraMove cameramove;
    [Header("Player")]
    public GameObject[] playerPrefs;
    public int tagNumber=0;
    public Vector3 PlayerStartPos;
    [HideInInspector] public Player playerScript;
    [HideInInspector] public GameObject[] players;
    [HideInInspector] public GameObject player;
    [HideInInspector] public int playerScore;

    [HideInInspector] public int catFoodCount=0;
    [HideInInspector] public int catFoodsCount=0;
    [HideInInspector] public int canFoodCount=0;
    [HideInInspector] public int catLeafCount=0;
    public delegate void ItemDel();
    public List<ItemDel> itemCounts = new List<ItemDel>();
    [HideInInspector] public float playerSpeed;
    [HideInInspector] public float jumpPower;
    [HideInInspector] public float gravityScale;
    [HideInInspector] public float speedMultiplier;

    public int catFoodScore;
    public int catFoodsScore;
    public int canFoodScore;
    public int destroyScore;
    public float udadaTime;
    public float udadaSpeedMultiplier;
    public float megamewTime;
    public float megamewMultiplier;
    public float catleafTime;

    public float initialJumpPower;
    public float initialGravityScale=9;
    public float idlePlayerSpeed;
    public float initialSpeedMultiplier;
    public float immuneTime;
    public int playerHealth;
    public int maxHealthCount;

    public CatleafCanvas catLeafCanvasPrefab;
    private CatleafCanvas catLeafCanvas;

    [HideInInspector] public bool cantTouchTag=false;
    private bool cantJump = false;
    [HideInInspector] public bool isHitted=false;
    private bool[] alphabetOn = { false, false, false, false, false };

    [Header("Sound")]
    public AudioClip tagSound;
    public AudioClip bonusStageSound;

    public delegate void CallBack(int degree);
    public CallBack score;
    public CallBack health;
    public CallBack alphabet;
    public delegate void ButtonChange();
    public ButtonChange action;
    public ButtonChange jump;

    public void Init()
    {
        players = new GameObject[playerPrefs.Length];
        playerSpeed = idlePlayerSpeed;
        speedMultiplier = initialSpeedMultiplier;
        itemCounts.Add(CatFoodCountUp);
        itemCounts.Add(CatFoodsCountUp);
        itemCounts.Add(CanFoodCountUp);
        itemCounts.Add(CatLeafCountUp);
        for (int i=0; i < playerPrefs.Length; i++)
        {
            players[i] = Instantiate(playerPrefs[i]);
            players[i].SetActive(false);
            players[i].transform.position = PlayerStartPos;
            Player temp=players[i].GetComponent<Player> ();
            temp.destroyScore = destroyScore;

            temp.Init();
        }
        player = players[tagNumber];
        player.SetActive(true);
        playerScript = player.GetComponent<Player>();
        playerScript.preventBugs();
        playerScore = 0;
        cameramove.CameraStart();

        catLeafCanvas = Instantiate(catLeafCanvasPrefab);
        catLeafCanvas.gameObject.SetActive(false);
    }

    public void GetCallback(CallBack scoreFunction, CallBack healthFunction, CallBack alphabetFunction,
        ButtonChange actionFunction, ButtonChange jumpFunction)
    {
        score = scoreFunction;
        health = healthFunction;
        alphabet = alphabetFunction;
        action = actionFunction;
        jump = jumpFunction;
        health(playerHealth);
    }

    public void PlayerJump()
    {
        if (enabled && !cantJump && playerScript.playerstate!= Player.PlayerState.parkour)
        {
            playerScript.Jump();
        }
    }

    public void PlayerParkour()
    {
        if ((int)playerScript.PlayerChar == 2 && enabled)
        {
            if (playerScript.playerstate != Player.PlayerState.parkour)
            {
                playerScript.Parkour();
                playerScript.playerstate = Player.PlayerState.parkour;
            }
            else
            {
                playerScript.Timing();
            }

        }
    }
    public void ReadyForAction()
    {
        action();
        cantTouchTag = true;
        Invoke("ReadyForJump", 5f);
        playerScript.playerstate = Player.PlayerState.idle;
    }
    public void ReadyForJump()
    {
        jump();
        cantTouchTag = false;
    }

    public void Tag()
    {
        if (!enabled || playerScript.playerstate!=0 || cantTouchTag)
        {
            return;
        }
        Vector3 tempPos = player.transform.position;
        player.SetActive(false);
        if (tagNumber >= playerPrefs.Length-1)
        {
            tagNumber = 0;
            SoundManager.soundmanager.SFXSet(tagSound, 1);
        }
        else
        {
            tagNumber++;
            SoundManager.soundmanager.SFXSet(tagSound, 1);
        }
        player = players[tagNumber];
        player.transform.position = tempPos;
        player.SetActive(true);
        playerScript = player.GetComponent<Player>();

        playerScript.Tagged();
    }

    public void Udada()
    {
        playerScript.Udada();
        cantTouchTag = true;
    }
    public void MegaMew()
    {
        playerScript.MegaMew();
        cantTouchTag = true;
    }
    public void CatLeafParty()
    {
        SoundManager.soundmanager.SFXSet(bonusStageSound, 3);
        playerScript.CatLeafParty();
        cantTouchTag = true;
    }
    public void AlphabetOn(int index)
    {
        alphabet(index);
        alphabetOn[index] = true;
        for(int i = 0; i < alphabetOn.Length; i++)
        {
            if (!alphabetOn[i])
            {
                return;
            }
        }
        for (int i = 0; i < alphabetOn.Length; i++)
        {
            alphabetOn[i] = false;
        }
        CatLeafParty();
    }
    public void HealthGain()
    {
        if (playerHealth == maxHealthCount)
        {
            return;
        }
        playerHealth++;
        health(playerHealth);
    }
    public void Immune()
    {
        isHitted = true;
        Invoke("FinishImmune", immuneTime);
    }
    public void FinishImmune()
    {
        isHitted = false;
    }

    public void HealthLoss()
    {
        playerHealth--;
        health(playerHealth);
        if (playerHealth <= 0)
        {
            PlayerDead();
        }
    }
    public void PlayerDead()
    {
        UIManager.uimanager.ShowCanvas(4);
        cantJump = true;
        playerScript.Dead();
        MapManager.mapmanager.PlayerEnd();
    }

    public void ScoreUIUpdate(int degree)
    {
        playerScore += degree;
        score(playerScore);
    }

    public void CatLeafSpawn()
    {
        catLeafCanvas.gameObject.SetActive(true);
        catLeafCanvas.ResetLeaf();
        catLeafCanvas.ActivateLeaf();
        cantJump = true;
    }
    public void CatLeafClear()
    {
        catLeafCanvas.ResetLeaf();
        catLeafCanvas.gameObject.SetActive(false);
        cantJump = false;
    }

    public void GameClear()
    {
        UIManager.uimanager.ShowCanvas(3);
        cantJump = true;
        MapManager.mapmanager.PlayerEnd();
        playerScript.GameClear();
    }

    public void CatFoodCountUp()
    {
        catFoodCount++;
    }
    public void CatFoodsCountUp()
    {
        catFoodsCount++;
    }
    public void CanFoodCountUp()
    {
        canFoodCount++;
    }
    public void CatLeafCountUp()
    {
        catLeafCount++;
    }
}
