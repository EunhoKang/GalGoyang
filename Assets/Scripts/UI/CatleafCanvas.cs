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
            leafpos.x= Random.Range(-500,500);
            leafpos.y = Random.Range(-180,180);
            leafs[i].transform.localPosition = leafpos;
            leafs[i].SetActive(true);
        }
    }
}
