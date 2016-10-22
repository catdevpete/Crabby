using UnityEngine;
using System.Collections;

public class Carapatriot : BaseCrab
{
    protected override void Start()
    {
        canAtt = true;
        hp = 5;
        att = 2;
        mS = 3;
        aS = 1;
        range = 1;
        cState = State.MOVE;
    }
}