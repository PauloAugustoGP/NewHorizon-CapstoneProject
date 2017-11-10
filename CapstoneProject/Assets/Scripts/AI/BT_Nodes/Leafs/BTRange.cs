﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRange : BTLeaf
{
    float damage;
    float cooldown;
    float time;

    bool bIsInCooldown;

    public BTRange() : base()
    {
        time = 0.0f;

        bIsInCooldown = false;
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

        damage = agent.GetDataFromEnemyTable("Ranged Damage");
        cooldown = agent.GetDataFromEnemyTable("Ranged Cooldown");
    }

    public override int Run(GameObject target)
    {
        if (!bIsInCooldown)
        {
            agent.EnableAttackCollision("Ranged");
            bIsInCooldown = true;
        }
        else
        {
            time += Time.deltaTime;

            if (time >= cooldown)
            {
                bIsInCooldown = false;
                time = 0.0f;
            }
            else
                return 1;
        }

        return 0;
    }

}
