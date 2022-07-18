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

	private void OnTriggerStay2D(Collider2D col)
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
		hpBar.fillAmount = _currentHealth / maxHealth;
		HealthChanged?.Invoke(_currentHealth / maxHealth);
	}
	
	protected override void Dead()
	{
		_bossMove.SpeedMultiply = 0;
		transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>().enabled = false;
		transform.GetChild(1).gameObject.GetComponent<CircleCollider2D>().enabled = false;
		Destroy(hpBar.transform.parent.parent.gameObject);
		BattleStarted?.Invoke(false, bossName);
		_animator.SetBool("isDead", true);
		isDead = true;
		ThrowItem();
		Destroy(gameObject, 2f);
	}
}
