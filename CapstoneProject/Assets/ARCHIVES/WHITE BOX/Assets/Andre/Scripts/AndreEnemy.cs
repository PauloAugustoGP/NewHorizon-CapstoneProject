using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AndreEnemy : EnemyBase
{
    public AudioClip EnemyBreathingNoise;

    protected override void OnTriggerEnter(Collider c)
    {
        base.OnTriggerEnter(c);
        if (c.tag == "Player")
        {
            MusicManager._instance.PlaySoundEffects(EnemyBreathingNoise);
        }

    }
    protected override void OnTriggerExit(Collider c)
    {
        base.OnTriggerExit(c);
        EnemyBreathingNoise = null;
    }

}
