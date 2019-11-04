using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeShotState : PlayerShotState
{
    private const float angleOffset = 5;

    public ThreeShotState(GameObject go, Bullet b):base(go, b)
    {
        setFireDelay(9);
    }

    public override void Fire(float angle)
    {
        Bullet newBullet1 = MonoBehaviour.Instantiate(getBullet());
        newBullet1.setPosition(getPlayer().transform.position);
        newBullet1.setAngle(angle - angleOffset);

        Bullet newBullet2 = MonoBehaviour.Instantiate(getBullet());
        newBullet2.setPosition(getPlayer().transform.position);
        newBullet2.setAngle(angle);

        Bullet newBullet3 = MonoBehaviour.Instantiate(getBullet());
        newBullet3.setPosition(getPlayer().transform.position);
        newBullet3.setAngle(angle + angleOffset);
    }
}
