using UnityEngine;
using System.Collections;

public class MidPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider _col)
    {
        if (_col.GetComponent<BaseCrab>() != null)
        {
            //_col.GetComponent<BaseCrab>().SetBaseEndPos();
        }
    }
}