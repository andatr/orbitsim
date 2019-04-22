using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    private GameObject _target;
    private Camera _camera;
    private Transform _transform;

    // -------------------------------------------------------------------------------------------------------------------
    public new Camera camera
    {
        get { return _camera; }
        set
        {
            _camera = value;
            _transform = _camera.transform;
        }
    }

    // -------------------------------------------------------------------------------------------------------------------
    public GameObject Target
    {
        get { return _target; }
        set
        {
            if (_target == value) return;
            _target = value;
            if (_transform != null && _target != null)
            {
                _transform.LookAt(_target.transform);
            }
        }
    }

    // -------------------------------------------------------------------------------------------------------------------
    public void Update ()
    {
	}
}
