using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public BasePlayer player;
    public Image bar;

    private void Start()
    {
        player.OnHealthChanged += OnHealthChanged;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(GameManager.player.position.x, GameManager.player.position.y - 1.5f, 10f);
    }

    private void OnHealthChanged(object sender, System.EventArgs e)
    {
        bar.fillAmount = player.HealthPercent();
    }
}
