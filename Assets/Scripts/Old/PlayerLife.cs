using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLife : MonoBehaviour
{
    public bool alive { get => _alive; }

    public UnityEvent onKill;
    public UnityEvent onFall;
    public UnityEvent onRespawn;

    public Vector3 offset { get => transform.position - feet.position; }
    public Vector3 spawnpoint { get => _spawnpoint; }
    [SerializeField]
    public Vector3 _spawnpoint;
    [SerializeField]
    private bool _alive = true;
    private Transform feet;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private PlayerMovement _playerMovement;

    public void Start()
    {
        _spawnpoint = transform.position;
        feet = transform.Find("Feet");
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
        Respawn();
    }

    public void SetSpawnpoint(Vector3 p) => _spawnpoint = p + offset;

    public void Respawn()
    {
        transform.position = _spawnpoint;
        _rb.simulated = true;
        _rb.velocity = Vector2.zero;
        _spriteRenderer.enabled = true;
        _playerMovement.enabled = true;
        _alive = true;
        onRespawn.Invoke();
    }

    public void ScheduleRespawn(float t)
    {
        IEnumerator Wait() {
            yield return new WaitForSeconds(t);
            Respawn();
        };

        StartCoroutine(Wait());
    }

    public void Kill(bool hidePlayer)
    {
        _rb.simulated = !hidePlayer;
        _spriteRenderer.enabled = !hidePlayer;
        _playerMovement.enabled = false;
        _alive = false;
        onKill.Invoke();
    }
    
    public void Fall()
    {
        _rb.simulated = false;
        _spriteRenderer.enabled = false;
        _playerMovement.enabled = false;
        _alive = false;
        onFall.Invoke();
    }
}
