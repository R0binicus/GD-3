using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public static class Helper
{
	public static Vector2 Rotate(this Vector2 v, float a)
	{
		a *= Mathf.Deg2Rad;
		return new Vector2(
			v.x * Mathf.Cos(a) - v.y * Mathf.Sin(a),
			v.y * Mathf.Cos(a) + v.x * Mathf.Sin(a)
		);
	}
} 

public class BouncePlatform : MonoBehaviour
{
	// Decides how the velocity will be changed before adding the bounce force
	public enum BounceMode
	{
		Unchanged,
		// The velocity in the bounce direction is removed
		Cancel,
		// The velocity in the bounce direction is reversed
		BounceAlways,
		// The velocity in the bounce direction is reversed only if moving in the opposite direction
		BounceOpposite
	}

	public UnityEvent onBounce;

	[Min(0)]
	public float bounceForce;
	[SerializeField]
	private float _bounceAngle = 0;
	public BounceMode velocityChange = BounceMode.Cancel;
	public AudioSource bounceSFX = null;

	[Header("Editor")]
	public Transform directionVisualiser = null;

	public float bounceAngle { get => _bounceAngle; set
		{
			_bounceAngle = value;
			bounceDirectionWorldspace = ((Vector2)transform.up).Rotate(bounceAngle);
		}
	}

    private Vector2 bounceDirectionWorldspace;
    private void Start()
	{
		bounceDirectionWorldspace = ((Vector2)transform.up).Rotate(bounceAngle);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag != "Player") return;
		if (Vector2.Dot(collision.rigidbody.velocity, bounceDirectionWorldspace) > 0.01) return;

		Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
		if (velocityChange != BounceMode.Unchanged)
		{
			float dot = Vector2.Dot(rb.velocity, bounceDirectionWorldspace);
			if (velocityChange == BounceMode.Cancel)
			{
				rb.velocity -= dot * bounceDirectionWorldspace;
			}
			else if (velocityChange == BounceMode.BounceAlways)
			{
				rb.velocity -= 2 * dot * bounceDirectionWorldspace;
			}
			else if(velocityChange == BounceMode.BounceOpposite && dot < 0)
			{
				rb.velocity -= 2 * dot * bounceDirectionWorldspace;
			}
		}

		Vector2 impulse = bounceDirectionWorldspace * bounceForce;

		rb.velocity += impulse;
		
		bounceSFX?.Play();
		onBounce.Invoke();
	}
}
