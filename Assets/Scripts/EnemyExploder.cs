using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExploder : DamagePlayer
{
    public GameObject explosion;

    protected override void OnDamagePlayer()
    {
        base.OnDamagePlayer();
        AudioManager.instance.PlaySFX(SoundEffect.Explosion);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
