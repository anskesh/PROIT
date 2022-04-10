using System;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
	[SerializeField] private string bossName;
	private bool _isBattle;
	private Animator _animator;
	private BossAI _bossMove;
	
	public event Action<bool, string> BattleStarted;
	public event Action<float> HealthChanged;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_bossMove = GetComponentInChildren<BossAI>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			_isBattle = true;
			BattleStarted?.Invoke(_isBattle, bossName);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_isBattle = false;
			BattleStarted?.Invoke(_isBattle, bossName);
		}
	}

	protected override void UpdateHealthBar()
	{
		if (hpBar != null) hpBar.fillAmount = _currentHealth / maxHealth;
		HealthChanged?.Invoke(_currentHealth / maxHealth);
	}
	
	protected override void Dead()
	{
		isDead = true;
		_bossMove.SpeedMultiply = 0;
		BattleStarted?.Invoke(false, bossName);
		_animator.SetBool("isDead", true);
		hpBar.transform.parent.parent.gameObject.SetActive(false);
		ThrowItem();
		Destroy(gameObject, 2f);
	}
}
