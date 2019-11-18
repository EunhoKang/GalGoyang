using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mumyeong : MonoBehaviour
{
    public float parkourStartingTime;
    public int maxActionCount;
    public int maxPassCount;
    public GameObject parkourCirclePrefab;

    [HideInInspector] public GameObject parkourCircle;
    [HideInInspector] public ParkourCircle parkourScript;
    private WaitForSeconds actionTimeRate = new WaitForSeconds(0.01f);
    public Rigidbody2D rb;
    public BoxCollider2D bx;
    [HideInInspector] public Vector2 MoveVector;

    private Vector3 curSpeed;

    Vector3 Linepos = new Vector3(0, 0, 0);
    Vector3 Middle = new Vector3(5f, 0.8f, 0);
    Vector3 ActionStartPos = new Vector3(0, 0, 0);
    public List<Vector3> moveDelta;
    public List<float> moveTime;
    public List<bool> moveTurn;

    [Header("Sound")]
    public AudioClip jump;
    public AudioClip touch;

    private void Awake()
    {
        parkourCircle = Instantiate(parkourCirclePrefab);
        parkourScript = parkourCircle.GetComponent<ParkourCircle>();
        parkourScript.CountReset();
        parkourScript.maxActionCount = maxActionCount;
        parkourScript.maxPassCount = maxPassCount;
        parkourCircle.SetActive(false);
    }
    public void TimingCheck()
    {
        parkourScript.CountUp();
    }

    public void Parkour()
    {
        StartCoroutine(StartParkour());
    }

    IEnumerator StartParkour()
    {
        transform.position = ActionStartPos;
        parkourCircle.SetActive(true);
        parkourScript.transform.position = CharacterManager.charmanager.player.transform.position+Middle;
        parkourScript.CountReset();
        parkourScript.maxCountSet(maxActionCount, maxPassCount);
        rb.velocity *= 0;
        rb.isKinematic = true;
        bool getmsg = false;
        parkourScript.DoParkour();
        while (!getmsg)
        {
            getmsg = parkourScript.sendmsg;
            yield return null;
        }
        parkourCircle.SetActive(false);
        if (parkourScript.PassFail)
        {
            parkourScript.ResetTF();
            yield return StartCoroutine("DuringParkour");
        }
        else
        {
            
            parkourScript.ResetTF();
            yield return StartCoroutine("EndParkour");
        }
    }

    IEnumerator DuringParkour()
    {
        bx.enabled = false;
        Vector3 move = ActionStartPos;

        for(int i=0; i < moveDelta.Count; i++)
        {
            if(i!= moveDelta.Count-1)
                SoundManager.soundmanager.SFXSet(jump, 4);
            move += moveDelta[i];
            if (moveTurn[i])
            {
                transform.Rotate(0, 180, 0);
            }
            for (float j = 0; j < parkourStartingTime * moveTime[i]; j += 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, move, ref curSpeed, parkourStartingTime * moveTime[i]+0.1f);
                yield return actionTimeRate;
            }
        }
        CharacterManager.charmanager.playerScript.Jump();
        yield return StartCoroutine("EndParkour");
    }

    IEnumerator EndParkour()
    {
        CharacterManager.charmanager.ReadyForJump();
        bx.enabled = true;
        rb.isKinematic = false;
        rb.velocity = MoveVector;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.layer)
        {
            case 10:
                CharacterManager.charmanager.ReadyForAction();
                ActionStartPos = other.gameObject.transform.position;
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.layer)
        {
            case 10:
                CharacterManager.charmanager.ReadyForJump();
                break;
            default:
                break;
        }
    }
}
