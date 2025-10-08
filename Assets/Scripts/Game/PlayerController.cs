using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerPosX;

    public PlayerController(int posX)
    {
        playerPosX = posX;
    }

    public int Running(int forward, int backward)
    {
        playerPosX += forward;
        playerPosX -= backward;
        return playerPosX;
    }

    public int GetPosition()
    {
        return playerPosX;
    }
}
