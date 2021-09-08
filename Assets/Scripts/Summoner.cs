using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX, maxX, minY, maxY, timeBetweenSummons ;
    private float summonTime, attackTime;
    private Vector2 targetPosition;
    public Enemy enemyToSummon;
    public float attackSpeed, stopDistance;

    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void Update()
    {
        if ( player != null)
        {
            if(Vector2.Distance(transform.position, targetPosition) > .5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                if(Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    Summon();
                }
            }
            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {

                if (Time.time >= attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    public void Summon()
    {
        if(player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }
    IEnumerator Attack()
    {
        player.GetComponent<PlayerMovement2>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
