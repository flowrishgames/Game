using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Dumbbell
{
    public int em_time = 0;
    public int em_buru = 0;
    public int em_y = 0;

    public Dumbbell(int value)
    {
        em_time = value;
    }

    public void UpdateState()
    {
        if (em_time > 0)
        {
            em_time--;

            if (em_time >= 16)
            {
                //落下予兆
                em_buru = (em_buru + 1) % 2;
            }
            else if (em_time >= 12)
            {
                //落下中
                em_buru = 0;
                em_y = em_y + 10;
            }
            else if (em_time >= 7)
            {
                //落下済み
                em_y = 42;
            }
            else if (em_time > 0)
            {
                //巻き上げ
                em_y = em_y - 6;
            }
            else
            {
                //停止
                em_y = 0;
            }
        }
    }
}
