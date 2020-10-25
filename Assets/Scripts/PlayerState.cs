using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public bool canWalkFast = false;
    public bool canJump = false;
    public bool canSuperJump = false;
    public bool canDash = false;
    public bool canBreak = false;
    public bool isOnFloor = true;

    //Only available in demo
    public bool canTeleportDemo = false;


    public PlayerState(bool canWalkFast)
    {
        this.canWalkFast = canWalkFast;
    }

    public bool getCanWalkFast()
    {
        return canWalkFast;
    }

    public void setCanWalkFast(bool state)
    {
        canWalkFast = state;
    }

    public void setCanJump(bool state)
    {
        canJump = state;
    }
    public void setCanSuperJump(bool state)
    {
        canSuperJump = state;
    }
    public void setCanDash(bool state)
    {
        canDash = state;
    }

    public bool getCanBreak()
    {
        return canBreak;
    }
    public void setCanBreak(bool state){
        canBreak = state;
    }

    //Only available in demo
    public bool getCanTeleportDemo(){
        return canTeleportDemo;
    }
    public void setTeleportDemo(bool state)
    {
        canTeleportDemo = state;
    }

    public bool getIsOnFloor(){
        return isOnFloor;
    }

    public void setIsOnFloor(bool state){
        isOnFloor = state;
    }

}

