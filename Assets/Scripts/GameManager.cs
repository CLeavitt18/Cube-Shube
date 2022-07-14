using TMPro;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private bool paused;

    private StringBuilder sb = new StringBuilder();

    private void Start() 
    {
        UpdatedHealthBar();
        UpdatedShieldBar();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Pause()
    {
        if (paused)
        {
            Resume();
            return;
        }

        UnlockMouse();
        paused = true;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        LockMouse();
        paused = false;
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        Destroy(Player.player.gameObject);
        SceneManager.LoadScene(0);
    } 

    public void Quit()
    {
        Application.Quit();
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
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

    public bool GetPaused()
    {
        return paused;
    }
}
