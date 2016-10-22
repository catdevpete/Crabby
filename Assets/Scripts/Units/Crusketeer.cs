using UnityEngine;
using System.Collections;

public class Crusketeer : BaseCrab
{
    protected override void Start()
    {
        canAtt = true;
        hp = 8;
        att = 3;
        mS = 3;
        aS = 2;
        range = 2;
        cState = State.MOVE;
    }
}