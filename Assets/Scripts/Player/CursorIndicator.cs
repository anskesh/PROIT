using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorIndicator : MonoBehaviour
{
	[SerializeField] private Transform cursor;
	[SerializeField] private Player player;
	[SerializeField] private Animator animatorTool;
	private TakeTools _tool;
	private float _cooldown = 0.8f;
	private float _timer = 0;
	private Camera _camera;
	
	public bool IsDead { get; set; }

	private void Start()
	{
		_camera = Camera.main;
		_tool = FindObjectOfType<TakeTools>();
	}

	private void OnEnable()
	{
		player.Dead += OnDead;
	}

	private void OnDisable()
	{
		player.Dead -= OnDead;
	}

	private void Update ()
	{
		/*var ray = _camera.ScreenPointToRay(Input.mousePosition);
		cursor.position = ray.origin;*/
		if (Input.GetMouseButton(0) && _timer <= 0)
		{
			_timer = _cooldown;
			SearchAndDoAction();
		}
		else _timer -= Time.deltaTime;
		if (Input.GetMouseButtonDown(0)) SearchAndDoAction();
	}

	private void SearchAndDoAction()
	{
		if (IsDead) return;
		var ray = _camera.ScreenPointToRay(Input.mousePosition);
		var hit = Physics2D.Raycast(ray.origin, ray.direction);
		if (hit.collider != null)
		{
			if (hit.collider.TryGetComponent(out IMineable mineable))
			{
				if (mineable.IsNear)
				{
					StartCoroutine(DoAction());
					mineable.MineResource();
				}
			}
			else if (hit.collider.TryGetComponent(out IDamageable damageable))
			{
				if (damageable.IsNear)
				{
					StartCoroutine(DoAction());
					damageable.ApplyDamage((int) (player.Damage * _tool.MultiplierEnemyDamage));
				}
			}
		}
	}
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.TryGetComponent(out IDamageable damageable)) damageable.IsNear = true;
		else if (col.gameObject.TryGetComponent(out IMineable mineable)) mineable.IsNear = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.TryGetComponent(out IDamageable damageable)) damageable.IsNear = false;
		else if (other.gameObject.TryGetComponent(out IMineable mineable)) mineable.IsNear = false;
	}

	private IEnumerator DoAction()
	{
		animatorTool.SetBool("isAction", true);
		yield return new WaitForSeconds(0.2f);
		animatorTool.SetBool("isAction", false);
	}

	private void OnDead()
	{
		IsDead = true;
	}
}
