using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        OnBallHit();
    }

    protected virtual void OnBallHit()
    {
        GameManager.Instance.AddScoreForBlock(this);
        Destroy(gameObject);
    }
}
