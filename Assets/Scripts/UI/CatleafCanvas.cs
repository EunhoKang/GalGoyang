using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatleafCanvas : MonoBehaviour
{
    public List<GameObject> leafs;

    public void Awake()
    {
        ResetLeaf();
    }

    public void ResetLeaf()
    {
        for(int i=0; i<leafs.Count; i++)
        {
            leafs[i].SetActive(false);
        }
    }

    public void ActivateLeaf()
    {
        Vector3 leafpos = new Vector3(0, 0, 0);
        for (int i=0; i < leafs.Count; i++)
        {
            leafpos.x= Random.Range(-750,750);
            leafpos.y = Random.Range(-350,350);
            leafs[i].transform.localPosition = leafpos;
            leafs[i].SetActive(true);
        }
    }
}
