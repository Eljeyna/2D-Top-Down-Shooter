using UnityEngine;

public static class GameManager
{
    public enum State
    {
        Normal = 0,
        Dash = 1,
        Stun = 2,
    }

    public static Rigidbody2D player;

    public static float GetAngleBetweenPoints(Vector2 lookDir)
    {
        return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }

    public static void GetPlayer()
    {
        player = GameObject.Find("Assault").GetComponent<Rigidbody2D>();
    }
}
