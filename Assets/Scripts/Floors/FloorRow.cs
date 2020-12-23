using UnityEngine;

public class FloorRow : MonoBehaviour, IFloorGroup
{
    [SerializeField] private float moveTime = 0.5f;
    
    private int size = 1;

    private FloorElement[] _floorElements;
    private bool _isMovingForward;
    
    private Vector3 _oldPosition;
    private Vector3 _newPosition;
    private float _currentTime;

    public void Awake()
    {
        _floorElements = GetComponentsInChildren<FloorElement>();
    }

    public void Update()
    {
        if (!_isMovingForward) return;
        
        transform.position = Vector3.Lerp(_oldPosition, _newPosition, _currentTime / moveTime);

        _currentTime += Time.deltaTime;

        if (transform.position == _newPosition)
            _isMovingForward = false;
    }

    public void StartShaking()
    {
        foreach (var floorElement in _floorElements)
        {
            floorElement.StartShaking();
        }
    }

    public void StartFalling()
    {
        foreach (var floorElement in _floorElements)
        {
            floorElement.StartFalling();
        }
    }

    public void StartMovingForward()
    {
        _oldPosition = transform.position;
        _newPosition = _oldPosition + Vector3.back;

        _currentTime = 0;
        _isMovingForward = true;
    }

    public int Size()
    {
        return size;
    }

    public FloorRow[] Rows()
    {
        return new[] {this};
    }
}
