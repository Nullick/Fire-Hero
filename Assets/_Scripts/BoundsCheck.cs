using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private bool _keepOnScreen = true;

    private bool _isOnScreen = true;
    private float camHeight;
    private float camWidth;
    private bool offUp, offDown, offLeft, offRight;

    public bool OffUp => offUp;
    public bool OffDown => offDown;

    public float CamHeight => camHeight;
    public float CamWidth => camWidth;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        Vector2 pos = transform.position;
        _isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;

        if(pos.x > camWidth - _radius)
        {
            pos.x = camWidth - _radius;
            offRight = true;
        }

        if(pos.x < -camWidth + _radius)
        {
            pos.x = -camWidth + _radius;
            offLeft = true;
        }

        if(pos.y > camHeight - _radius)
        {
            pos.y = camHeight - _radius;
            offUp = true;
        }

        if(pos.y < -camHeight + _radius)
        {
            pos.y = -camHeight + _radius;
            offDown = true;
        }

        _isOnScreen = !(offDown || offUp || offLeft || offRight);

        if(_keepOnScreen && !_isOnScreen)
        {
            transform.position = pos;
            _isOnScreen = true;
            offRight = offLeft = offDown = offUp = false;
        }
    }
}
