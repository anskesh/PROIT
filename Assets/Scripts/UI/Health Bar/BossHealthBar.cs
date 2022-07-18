using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI bossName;
    [SerializeField] private CanvasGroup container;
    [SerializeField] private Canvas smallHealthBar;

    private void OnEnable()
    {
        boss.BattleStarted += OnBattleStarted;
        boss.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        boss.BattleStarted -= OnBattleStarted;
        boss.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        healthBar.fillAmount = health;
    }
    
    private void OnBattleStarted(bool isStart, string typeName)
    {
        if (isStart) StartBattle(typeName);
        else StopBattle();
    }

    private void StartBattle(string typeName)
    {
        bossName.text = typeName;
        container.alpha = 1;
        if (smallHealthBar == null) return;
        smallHealthBar.gameObject.SetActive(false);
    }

    private void StopBattle()
    {
        container.alpha = 0;
        bossName.text = "";
        if (smallHealthBar == null) return;
        smallHealthBar.gameObject.SetActive(true);
    }
}
