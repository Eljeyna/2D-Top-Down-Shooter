using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public BasePlayer player;
    public Transform bar;

    private void Start()
    {
        player.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(object sender, System.EventArgs e)
    {
        bar.localScale = new Vector3(player.HealthPercent(), 1f);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(GameManager.player.position.x, GameManager.player.position.y - 1.5f, 10f);
    }
}
