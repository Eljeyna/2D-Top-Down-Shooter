using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    public BaseEntity owner;
    public Tags.EntityTags baseTag;
    public float damage;
    [HideInInspector] public float nextFade;
}
