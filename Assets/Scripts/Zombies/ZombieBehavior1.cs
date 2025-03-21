using UnityEngine;
using System.Collections;

public class BossBehavior : ZombieBehavior
{

    AudioSource bossSoundAudio;

    void OnAwake()
    {
        float currentRound = ZombieManager.Instance.curRound;
        if (currentRound == 0)
        {
            currentRound = 1;
        }

        health = health * currentRound;

        scoreVal = scoreVal * currentRound;

        attackDamage = attackDamage * currentRound;

        // get radius of circle collider
        float radius = GetComponent<CircleCollider2D>().radius;
        // scale the radius
        radius = radius * currentRound;

        // set the new radius
        GetComponent<CircleCollider2D>().radius = radius;

        // set the distnace
        distance = radius + distance;
    }
}
