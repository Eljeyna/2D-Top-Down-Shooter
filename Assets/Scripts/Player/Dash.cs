using UnityEngine;

public class Dash : MonoBehaviour
{
    public int dashes = 0;
    public int maxDashes = 1;
    public float dashTime = 0.25f;
    public float dashReload = 8f;
    public AnimationCurve dashSpeed;
    public float dashEvaluateTime;

    [HideInInspector] public float nextDash;
    [HideInInspector] public float nextDashTime;

    //private Coroutine coroutine;

    //private Image cooldown;
    //private TMP_Text cooldownCount;

    private void Start()
    {
        dashes = maxDashes;
    }

    public void Update()
    {
        //cooldown.fillAmount -= 1f / dashReload * BoltNetwork.FrameDeltaTime;

        if (dashes < maxDashes && nextDash <= Time.time)
        {
            dashes++;
            //cooldownCount.text = dashes.ToString();
            if (dashes < maxDashes)
                nextDash = Time.time + dashReload;

            //cooldown.fillAmount = 1f;
        }
        else if (dashes >= maxDashes)
        {
            //cooldown.fillAmount = 0f;
            this.enabled = false;
        }
    }

    public void Use()
    {
        dashes--;
        //cooldownCount.text = dashes.ToString();
        nextDashTime = Time.time + dashTime;
        if (nextDash <= Time.time)
        {
            nextDash = Time.time + dashReload;
            //cooldown.fillAmount = 1f;
        }
        return;
    }
}
