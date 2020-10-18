﻿using UnityEngine;

public class BulletPistolSoldier : Bullet
{
    private void Update()
    {
        transform.position += transform.up * bulletData.speed * Time.deltaTime;
        nextFade += Time.deltaTime;

        if (nextFade >= bulletData.timeFade)
        {
            nextFade = 0f;
            Pool.Instance.AddToPool(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Tags.EntityTags attackerTag = collision.collider.GetComponent<BaseTag>().entityTag;
            if ((baseTag & attackerTag) == 0)
            {
                if (bulletData.radius > 0f)
                {
                    RadiusAttack.RadiusDamage(owner.gameObject, transform.position, bulletData.radius, damage, 1 << gameObject.layer);
                }
                else
                {
                    BaseEntity baseEntity = collision.gameObject.GetComponent<BaseEntity>();
                    baseEntity.TakeDamage(damage, owner);
                }
            }
        }

        Pool.Instance.AddToPool(gameObject);
    }
}