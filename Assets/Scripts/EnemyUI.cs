using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;

    [SerializeField] private Enemy enemy;

    
    public void UpdatedBars()
    {
        int life = enemy.GetLife();
        int maxlife = enemy.GetMaxLife();
        int shield = enemy.GetShield();
        int maxShield = enemy.GetMaxShield();

        healthBar.fillAmount = (float)life / maxlife;

        if (maxShield == 0)
        {
            shieldBar.fillAmount = 0;
        }
        else
        {
            shieldBar.fillAmount = (float)shield / maxShield;
        }

    }
}
