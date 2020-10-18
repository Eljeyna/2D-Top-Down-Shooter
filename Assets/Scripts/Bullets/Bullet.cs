using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    public BaseEntity owner;
    public Tags.EntityTags baseTag;
    [HideInInspector] public float nextFade;
}
