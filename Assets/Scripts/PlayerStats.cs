using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Ui;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;

    private StringBuilder sb = new StringBuilder();


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
        int life = Player.player.GetLife();
        int maxLife = Player.player.GetMaxLife();

        healthBar.fillAmount = (float)life / maxLife;

        sb.Clear();

        sb.Append(life.ToString());
        sb.Append(" / ");
        sb.Append(maxLife);

        lifeText.text = sb.ToString();

        sb.Clear();
    }

    public void UpdatedShieldBar()
    {
        int shield = Player.player.GetShield();
        int maxShield = Player.player.GetMaxShield();

        shieldBar.fillAmount = (float)shield / maxShield;

        sb.Clear();

        sb.Append(shield.ToString());
        sb.Append(" / ");
        sb.Append(maxShield);

        shieldText.text = sb.ToString();

        sb.Clear();
    }
}
