using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerShotState
{

    private int numberOfBullets;
    private GameObject player;
    private Bullet bullet;
    private int fireDelayFrames = 3;
    private int count = 0;
    private int finishTime = 600;

    public PlayerShotState(GameObject player, Bullet b)
    {
        this.player = player;
        this.bullet = b;
    }

    public abstract void Fire(float angle);

    protected void SetNumberOfBullets(int b)
    {
        numberOfBullets = b;
    }

    protected int GetNumberOfBullets()
    {
        return numberOfBullets;
    }
    
    protected GameObject getPlayer()
    {
        return player;
    }

    protected Bullet getBullet()
    {
        return bullet;
    }

    protected void setFireDelay(int frames)
    {
        fireDelayFrames = frames;
    }

    public int getFireDelay()
    {
        return fireDelayFrames;
    }

    public void IncreaseTime(int interval)
    {
        count += interval;
    }

    public int GetCount()
    {
        return count;
    }

    public int GetFinishTime()
    {
        return finishTime;
    }

}
