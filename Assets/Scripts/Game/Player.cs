using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        idle=0,
        air,
        end,
        parkour
    };
    public enum PlayerType
    {
        b312 = 0,
        rucy,
        mumyeong
    };

    public PlayerType PlayerChar;
    public Rigidbody2D rb;
    public Animator animator;
    public Animator effector;
    public int MaxJumpCount;

    private float JumpPower;
    private float initialGravityScale;
    private Vector2 MoveVector;
    private float initialSpeed;
    private int ExtraJumpLeft;
    private Vector3 forPrevent;
    private Vector2 whenDead;
    private IEnumerator curUdada;
    private IEnumerator curMega;

    public bool isUdada=false;
    public bool isMega=false;
    public bool isParty=false;
    Vector3 initTransform;
    Vector3 targetTransform;

    [HideInInspector] public PlayerState playerstate;
    [HideInInspector] public int catFoodScore;
    [HideInInspector] public int catFoodsScore;
    [HideInInspector] public int canFoodScore;
    [HideInInspector] public int destroyScore;

    public GameObject[] playerPrefs;
    public int tagNumber = 0;
    public Vector3 PlayerStartPos;
    [HideInInspector] public Player playerScript;
    [HideInInspector] public GameObject[] players;
    [HideInInspector] public GameObject player;
    [HideInInspector] public int playerScore;

    private float doubleJumpMuitiplier = Mathf.Sqrt(6)/3;
    private bool NotOnRightPlace=false;
    private WaitForSeconds defaultAnim;
    private SpriteRenderer sr;

    [Header("Sound")]
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip breakSound;
    public AudioClip DeadSound;
    public AudioClip winSound;

    Mumyeong mumyeong;

    public void Awake()
    {
        initialGravityScale = rb.gravityScale;
        initialSpeed = CharacterManager.charmanager.playerSpeed;
        forPrevent = new Vector3(0, 0.5f, 0);
        whenDead = new Vector2(0, 0);
        ExtraJumpLeft = MaxJumpCount;
        playerstate = PlayerState.idle;
        defaultAnim = new WaitForSeconds(0.05f);
        sr = GetComponent<SpriteRenderer>();
        initTransform = transform.localScale;
        if ((int)PlayerChar == 2)
        {
            mumyeong = GetComponent<Mumyeong>();
            mumyeong.rb = rb;
        }
    }
    public void Init()
    {
        SpeedChange(CharacterManager.charmanager.speedMultiplier);
        MoveVector =new Vector2(CharacterManager.charmanager.playerSpeed,0);
        rb.velocity=MoveVector;

        targetTransform = transform.localScale * CharacterManager.charmanager.megamewMultiplier;

        if ((int)PlayerChar == 2)
        {
            mumyeong.MoveVector = MoveVector;
        }
    }

    public void preventBugs()
    {
        StartCoroutine(PreventStuck());
    }
    IEnumerator PreventStuck()
    {
        float cur = transform.position.y;
        while (true)
        {
            if (rb.velocity.x < CharacterManager.charmanager.playerSpeed && playerstate == PlayerState.idle)
            {
                Debug.Log("Has Stucked");
                MoveVector.x = CharacterManager.charmanager.playerSpeed;
                transform.position += forPrevent * CharacterManager.charmanager.playerSpeed * 0.075f;
                rb.velocity = MoveVector;
            }
            if (cur > transform.position.y)
            {
                rb.AddForce(Vector2.up* CharacterManager.charmanager.speedMultiplier*10);
            }
            cur= transform.position.y;
            yield return null;
        }
        
    }

    public void SpeedChange(float degree)
    {
        CharacterManager.charmanager.jumpPower = CharacterManager.charmanager.initialJumpPower * degree;
        CharacterManager.charmanager.gravityScale = CharacterManager.charmanager.initialGravityScale * degree * degree;
        CharacterManager.charmanager.playerSpeed = CharacterManager.charmanager.idlePlayerSpeed * degree;
        CharacterManager.charmanager.speedMultiplier = degree;

        rb.gravityScale = CharacterManager.charmanager.gravityScale;
        MoveVector = new Vector2(CharacterManager.charmanager.playerSpeed, 0);
        rb.velocity = MoveVector;
    }
    public void SpeedChangeUdada(float degree)
    {
        CharacterManager.charmanager.playerSpeed = CharacterManager.charmanager.idlePlayerSpeed * degree;
        CharacterManager.charmanager.speedMultiplier = degree;
        MoveVector = new Vector2(CharacterManager.charmanager.playerSpeed, 0);
        rb.velocity = MoveVector;
    }

    public void Jump(){
        if(ExtraJumpLeft>0){
            ExtraJumpLeft--;
            rb.velocity = MoveVector;
            if (MaxJumpCount>=2 && ExtraJumpLeft==0)
            {
                rb.velocity += Vector2.up * CharacterManager.charmanager.jumpPower * 7.5f*doubleJumpMuitiplier;
                animator.SetTrigger("DoubleJump");
                SoundManager.soundmanager.SFXSet(jumpSound, 1);
            }
            else
            {
                rb.velocity += Vector2.up * CharacterManager.charmanager.jumpPower * 7.5f;
                SoundManager.soundmanager.SFXSet(jumpSound, 1);
            }
            playerstate = PlayerState.air;
            animator.SetBool("IsJumping",true);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.layer)
        {
            case 8:
                ExtraJumpLeft = MaxJumpCount;
                rb.velocity = MoveVector;
                playerstate = PlayerState.idle;
                animator.SetBool("IsJumping", false);
                break;
            default:
                break;
        }
    }

    public void Parkour()
    {
        if ((int)PlayerChar == 2)
        {
            mumyeong.Parkour();
            
        }
    }
    public void Timing()
    {
        if ((int)PlayerChar == 2)
        {
            mumyeong.TimingCheck();
        }
    }

    public void Tagged()
    {
        Init();
        preventBugs();
        Color c = sr.color;
        c.a = 1;
        sr.color = c;
        animator.SetTrigger("playerTag");
    }

    public void Dead(){
        rb.velocity *= 0;
        rb.isKinematic = true;
        SoundManager.soundmanager.SFXSet(DeadSound, 0);
        StopAllCoroutines();
        if(curUdada!=null)
            StopCoroutine(curUdada);
        if(curMega!=null)
            StopCoroutine(curMega);
        isMega = false;
        CharacterManager.charmanager.isHitted = true;
        playerstate = PlayerState.end;
        SpeedChange(CharacterManager.charmanager.initialSpeedMultiplier);
        CharacterManager.charmanager.speedMultiplier = CharacterManager.charmanager.initialSpeedMultiplier;
        effector.SetBool("Udada", false);
        CharacterManager.charmanager.playerSpeed = CharacterManager.charmanager.idlePlayerSpeed;
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsDead",true);
        StartCoroutine(AfterDead());
    }
    IEnumerator AfterDead()//나중에 캐릭터 매니저로 옮기기
    {
        for(int i = 0; i < 24; i++)
        {
            rb.velocity *= 0;
            rb.isKinematic = true;
            isMega = false;
            yield return new WaitForSeconds(0.1f);
        }
        enabled = false;
        gameObject.SetActive(false);
    }
    public void GameClear()
    {
        SoundManager.soundmanager.SFXSet(winSound, 0);
        StopCoroutine(PreventStuck());
        StopCoroutine(NotStepOnRightPlace());
        StopCoroutine(AfterDead());
        StopCoroutine(Immune());
        StopCoroutine(DuringCatleafParty());
        StopCoroutine(DuringMegaMew());
        StopCoroutine(DuringUdada());

        if (curUdada != null)
            StopCoroutine(curUdada);
        if (curMega != null)
            StopCoroutine(curMega);
        isMega = false;
        playerstate = PlayerState.end;
        CharacterManager.charmanager.isHitted = true;
        SpeedChange(CharacterManager.charmanager.initialSpeedMultiplier);
        CharacterManager.charmanager.speedMultiplier = CharacterManager.charmanager.initialSpeedMultiplier;
        effector.SetBool("Udada", false);
        CharacterManager.charmanager.playerSpeed = CharacterManager.charmanager.idlePlayerSpeed;
        rb.velocity *= 0;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.layer){
            case 8 :
                Debug.Log("not on right place");
                StartCoroutine(NotStepOnRightPlace());
                break;
            case 9 :
                if (isUdada || isMega)
                {
                    SoundManager.soundmanager.SFXSet(breakSound, 4);
                    CharacterManager.charmanager.ScoreUIUpdate(destroyScore);
                    other.gameObject.SetActive(false);
                }
                else if(!CharacterManager.charmanager.isHitted)
                {
                    SoundManager.soundmanager.SFXSet(crashSound, 4);
                    CharacterManager.charmanager.HealthLoss();
                    if (CharacterManager.charmanager.playerHealth > 0)
                    {
                        StartCoroutine(Immune());
                    }
                }
                break;
            default :
                break;
        }
    }
    public void Udada()
    {
        if (curUdada != null)
        {
            StopCoroutine(curUdada);
        }
        curUdada = DuringUdada();
        StartCoroutine(curUdada);
    }
    public void MegaMew()
    {
        if (curMega != null)
        {
            StopCoroutine(curMega);
        }
        curMega = DuringMegaMew();
        StartCoroutine(curMega);
    }
    public void CatLeafParty()
    {
        StartCoroutine(DuringCatleafParty());
    }

    public IEnumerator Immune()
    {
        CharacterManager.charmanager.Immune();
        Color c;
        WaitForSeconds immuneAnim = new WaitForSeconds(0.2f);
        for (int i = 0; i < CharacterManager.charmanager.immuneTime * 5; i++)
        {
            c = sr.color;
            c.a = 0.5f * (i % 2 + 1);
            sr.color = c;
            yield return immuneAnim;
        }
        c = sr.color;
        c.a = 1;
        sr.color = c;
    }

    IEnumerator NotStepOnRightPlace()
    {
        NotOnRightPlace = true;
        while (NotOnRightPlace)
        {
            Debug.Log("going up");
            transform.position += forPrevent* CharacterManager.charmanager.playerSpeed *0.075f;
            yield return null;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.layer)
        {
            case 8:
                Debug.Log("Escape form wrong place");
                NotOnRightPlace = false;
                break;
            default:
                break;
        }
    }

    IEnumerator DuringUdada()
    { 
        effector.SetBool("Udada", false);
        SpeedChangeUdada(CharacterManager.charmanager.udadaSpeedMultiplier);
        effector.SetBool("Udada", true);
        for (float i = 0; i < CharacterManager.charmanager.udadaTime; i += 0.05f)
        {
            isUdada = true;
            CharacterManager.charmanager.isHitted = true;
            CharacterManager.charmanager.cantTouchTag = true;
            yield return defaultAnim;
        }
        isUdada = false;
        SpeedChangeUdada(CharacterManager.charmanager.initialSpeedMultiplier);
        effector.SetBool("Udada", false);
        CharacterManager.charmanager.isHitted = true;
        CharacterManager.charmanager.cantTouchTag = false;
        yield return StartCoroutine(Immune());
    }

    IEnumerator DuringMegaMew()
    {
        int ojump=MaxJumpCount;
        MaxJumpCount = 1;
        for (int i = 1; i <= 10; i ++)
        {
            transform.localScale = Vector3.Lerp(initTransform, targetTransform, i * 0.1f);
            isMega = true;
            CharacterManager.charmanager.isHitted = true;
            CharacterManager.charmanager.cantTouchTag = true;
            yield return defaultAnim;
        }
        for (float i = 0; i < CharacterManager.charmanager.megamewTime; i += 0.05f)
        {
            isMega = true;
            CharacterManager.charmanager.isHitted = true;
            CharacterManager.charmanager.cantTouchTag = true;
            yield return defaultAnim;
        }
        MaxJumpCount = ojump;
        for (int i = 1; i <= 10; i++)
        {
            transform.localScale = Vector3.Lerp(targetTransform, initTransform, i * 0.1f);
            isMega = true;
            CharacterManager.charmanager.isHitted = true;
            CharacterManager.charmanager.cantTouchTag = true;
            yield return defaultAnim;
        }
        isMega = false;
        CharacterManager.charmanager.cantTouchTag = false;
        yield return StartCoroutine(Immune());
    }

    IEnumerator DuringCatleafParty()
    {
        CharacterManager.charmanager.CatLeafSpawn();//UI에서 관리
        Vector2 temp = rb.velocity;
        for (float i = 0; i < CharacterManager.charmanager.catleafTime; i += 0.05f)
        {
            rb.velocity *= 0;
            rb.isKinematic = true;
            isParty = true;
            CharacterManager.charmanager.cantTouchTag = true;
            yield return defaultAnim;
        }
        CharacterManager.charmanager.CatLeafClear();
        isParty = false;
        rb.isKinematic = false;
        rb.velocity = temp;
        CharacterManager.charmanager.isHitted = true;
        CharacterManager.charmanager.cantTouchTag = false;
        yield return StartCoroutine(Immune());
    }
}
