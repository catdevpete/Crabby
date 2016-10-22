using UnityEngine;
using System.Collections;

public class Crabuchet : BaseCrab
{
    protected override void Start()
    {
        canAtt = true;
        hp = 10;
        att = 5;
        mS = 3;
        aS = 3;
        range = 4;
        cState = State.MOVE;
    }
}