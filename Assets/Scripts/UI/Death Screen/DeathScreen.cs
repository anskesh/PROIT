using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private Button resume;
    [SerializeField] private Button exit;
    [SerializeField] private Player player;

    private void OnEnable()
    {
        player.Dead += OnDead;
        resume.onClick.AddListener(Resume);
        exit.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        player.Dead -= OnDead;
        resume.onClick.RemoveListener(Resume);
        exit.onClick.RemoveListener(Exit);
    }

    private void OnDead()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        Time.timeScale = 0;
        
        resume.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1;
        GetComponent<CanvasGroup>().alpha = 0;
        
        resume.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        FindObjectOfType<CursorIndicator>().PlayerDead = false;
        FindObjectOfType<ItemPickup>().gameObject.GetComponent<Collider2D>().enabled = true;
        
        player.SetMaxHealth();
    }

    private void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
