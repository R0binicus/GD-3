using UnityEngine;

public class CameraMovement : MonoBehaviour
{   
    [SerializeField] private float cameraSpeed;
    void Update()
    {
        transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
	}
    
    
    
    /*
	public bool canFall;
    public float scrollOffset = 2;

	public Vector2Int snap;
    public Range<int>[] levelWidths;
    public bool[] levelSnapX;

    private Transform _player;
	private PlayerLife _playerlife;

    public void SetCanFall(bool _canFall) => canFall = _canFall;

	private bool _isFalling = false;

    private void Start()
    {
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_playerlife = _player.GetComponent<PlayerLife>();
    }

	int NearestY(Vector3 v)
    {        
		return Mathf.RoundToInt(v.y / snap.y);
    }

    void Update()
    {
        int sy = NearestY(_playerlife.spawnpoint);
		int y = NearestY(_player.position);

		bool _wasFalling = _isFalling;
		_isFalling = y < sy;

		if(_isFalling && !canFall)
        {
            y = sy;
            if (!_wasFalling)
            {
                _playerlife.Fall();
            }
        }

        bool snapX = (y < levelSnapX.Length && y < levelWidths.Length) ? levelSnapX[y] : true;

        Vector3 v = transform.position;

        if (!snapX)
        {
            Range<int> level = levelWidths[y];

            float xOff = transform.position.x - _player.position.x;

            v.x = Mathf.Clamp((_player.position.x + Mathf.Clamp(xOff, -scrollOffset, +scrollOffset)) / snap.x, level.min, level.max) * snap.x;
        } else
        {
            v.x = Mathf.RoundToInt(_player.position.x / snap.x) * snap.x;
        }

        v.y = y * snap.y;

        transform.position = v;
	}
    */
}
