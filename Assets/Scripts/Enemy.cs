using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyFX;    
    [SerializeField] int hitPoints;
    [SerializeField] int damage = 10;
    int score;
    Scoreboard scoreboard;
    GameObject parentGameObject;
    [SerializeField] GameObject hitVFX;

    void Start()
    { 
        gameObject.AddComponent<Rigidbody>().useGravity=false;
        scoreboard = FindObjectOfType<Scoreboard>(); 
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void OnParticleCollision(GameObject other)
    {
        UpdateScore();

        if (hitPoints <= 0)
        {
            DestroyEnemy();
        }
    }

    private void UpdateScore()
    {
        ShowHitVFX();

        scoreboard.IncreaseScore(damage);

        hitPoints -= damage;
    }

    private void ShowHitVFX()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
    }

    void DestroyEnemy()
    {        
        int killScore = (int)(hitPoints/3);
        scoreboard.IncreaseScore(killScore);
        GameObject fx = Instantiate(enemyFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
