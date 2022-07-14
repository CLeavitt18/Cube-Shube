using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Ui;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;


    void Start()
    {
        UpdatedHealthBar();
        UpdatedShieldBar();

        if (Ui != null && Ui != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Ui = this;
        }
    }

    public void UpdatedHealthBar()
    {
        healthBar.fillAmount = (float)Player.player.GetLife() / Player.player.GetMaxLife();
    }

    public void UpdatedShieldBar()
    {
        shieldBar.fillAmount = (float)Player.player.GetShield() / Player.player.GetMaxShield();
    }
}
