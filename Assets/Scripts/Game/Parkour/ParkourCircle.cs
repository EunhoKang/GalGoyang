using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourCircle : MonoBehaviour
{
    public float radius;
    public float revolveSpeed;
    public GameObject note;
    public GameObject noteLine;
    public GameObject faceDefault;
    public GameObject facePass;
    public GameObject faceFail;
    [HideInInspector] public int maxActionCount;
    [HideInInspector] public int maxPassCount;
    int count = 0;
    [HideInInspector] public bool PassFail=false;
    [HideInInspector] public bool sendmsg = false;
    [HideInInspector] public bool ispushed = false;
    Vector3 temp;
    Vector3 randomPlace;
    Vector3 Middle = new Vector3(5f, 0.8f, 0);
    float mathtemp;

    [Header("Sound")]
    public AudioClip clear;
    public AudioClip fail;
    public AudioClip touch;

    private void Awake()
    {
        temp = new Vector3(0,0,0);
        randomPlace = new Vector3(0,0,0);
        mathtemp = Mathf.PI * 2;
    }
    private void OnEnable()
    {
        CountReset();
        noteLine.transform.position = transform.position;
        noteLine.transform.position += Vector3.right * radius;
    }

    public void PushButton()
    {
        ispushed = true;
    }
    public void ReleaseButton()
    {
        ispushed = false;
    }

    public void CountUp()
    {
        if (ispushed)
        {
            SoundManager.soundmanager.SFXSet(touch, 4);
            count++;
        }
        Note2RandomPlace();
    }
    public void Note2RandomPlace()
    {
        float randtemp = Random.Range(0, mathtemp);
        randomPlace.x = Mathf.Cos(randtemp) * radius + CharacterManager.charmanager.player.transform.position.x + Middle.x;
        randomPlace.y = Mathf.Sin(randtemp) * radius + CharacterManager.charmanager.player.transform.position.y + Middle.y;
        note.transform.position = randomPlace;
    }
    public void CountReset()
    {
        count = 0;
        faceDefault.SetActive(true);
        facePass.SetActive(false);
        faceFail.SetActive(false);
    }
    public void maxCountSet(int act, int pass)
    {
        maxActionCount = act;
        maxPassCount = pass;
    }
    public void ResetTF()
    {
        PassFail = false;
        sendmsg = false;
    }
    public void DoParkour()
    {
        StartCoroutine("StartParkour");
    }

    IEnumerator StartParkour()
    {
        float current = 0;
        while (count < maxPassCount && current < maxActionCount*mathtemp)
        {
            temp.x = Mathf.Cos(current) * radius+ CharacterManager.charmanager.player.transform.position.x+Middle.x;
            temp.y = Mathf.Sin(current) * radius+ CharacterManager.charmanager.player.transform.position.y+Middle.y;
            noteLine.transform.position = temp;
            current += revolveSpeed;
            yield return null;
        }
        if (count >= maxPassCount)
        {
            faceDefault.SetActive(false);
            facePass.SetActive(true);
            SoundManager.soundmanager.SFXSet(clear, 4);
            yield return new WaitForSeconds(0.8f);
            PassFail = true;
        }
        else
        {
            faceDefault.SetActive(false);
            faceFail.SetActive(true);
            SoundManager.soundmanager.SFXSet(fail, 4);
            PassFail = false;
        }
        sendmsg = true;
    }
}
