using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D col;
        if (_virtualCamera != null)
        {
            _virtualCamera.enabled = false;
            col = _virtualCamera.gameObject.GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = true;
            }
        }

        _virtualCamera = other.gameObject.GetComponent<CinemachineVirtualCamera>();
        col = other.gameObject.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }
        if (_virtualCamera != null)
        {
            _virtualCamera.enabled = true;
        }
    }
}
