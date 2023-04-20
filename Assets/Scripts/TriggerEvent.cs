using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
	public UnityEvent onTriggerEnter;
    [SerializeField, HideInInspector]
    public int maxTriggers = 1;
    [SerializeField, HideInInspector]
	public bool needsPlayer = true;
	[SerializeField, HideInInspector]
	public bool needsGrounded = true;
	[SerializeField, HideInInspector]
	public bool needsAlive = true;
	[SerializeField, HideInInspector]
	public bool killOnTrigger = false;
	[SerializeField, HideInInspector]
	public bool hidePlayerOnKill = true;

	private Transform respawnPosition = null;
	private int _triggers = 0;
	private HashSet<GameObject> _pendingTriggers = new();

	private void Start()
	{
		respawnPosition = transform.Find("RespawnPosition");
	}

	private void AttemptTrigger(GameObject other)
	{
		PlayerLife pl = null;
		PlayerMovement pm = null;

        bool success = maxTriggers <= 0 || _triggers < maxTriggers;

		if(needsPlayer)
		{
			success = success &&
				other.tag == "Player" &&
				other.TryGetComponent(out pl) &&
				other.TryGetComponent(out pm) &&
				(!needsGrounded || pm.IsGrounded()) &&
				(!needsAlive || pl.alive);

        }

		if (!success) return;

		if (needsPlayer)
		{
			if (killOnTrigger)
			{
				pl.Kill(hidePlayerOnKill);
			}
			if(respawnPosition != null)
			{
				pl.SetSpawnpoint(respawnPosition.position);
			}
        }

        onTriggerEnter.Invoke();
        _pendingTriggers.Remove(other);
        _triggers++;

		if (_triggers == maxTriggers)
		{
			gameObject.SetActive(false);
		}
    }



	private void OnTriggerEnter2D(Collider2D other)
	{
		_pendingTriggers.Add(other.gameObject);
		AttemptTrigger(other.gameObject);
	}

	private void OnTriggerStay2D(Collider2D other)
	{

		if (_pendingTriggers.Contains(other.gameObject)) AttemptTrigger(other.gameObject);
	}
}