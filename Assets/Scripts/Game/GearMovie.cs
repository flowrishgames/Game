using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMovie : MovieClip
{

    public override void gotoAndPlay(int param)
    {
        base.gotoAndPlay(param);

        switch (param)
        {
            case 1:
                animator.SetInteger("gearLevel", 0);
                break;
            case 4:
                animator.SetInteger("gearLevel", 1);
                break;
        }
    }

}
