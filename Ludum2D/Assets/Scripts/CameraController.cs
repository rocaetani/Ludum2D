using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float transitionSeconds = 1;
    public float cameraSlideAmount = 5.86f;

    private bool _isGoingUp = false;
    private Vector3 _initialPosition;
    private Vector3 _finalPosition;
    private float _lerpAmount = 0;

    void Start()
    {
        _initialPosition = transform.localPosition;
        _finalPosition = transform.localPosition + new Vector3(0, cameraSlideAmount, 0);
    }

    void Update()
    {
        if(!_isGoingUp && GameObjectAccess.Player.playerState == Player.PlayerState.GoingUp) {
            _isGoingUp = true;

            StartCoroutine(slideCamera());
        }
    }

    private IEnumerator slideCamera() {
        while(transform.localPosition != _finalPosition) {
            yield return new WaitForFixedUpdate();
            _lerpAmount += transitionSeconds * Time.fixedDeltaTime;

            transform.localPosition = Vector3.Lerp(_initialPosition, _finalPosition, Utils.easeOutQuart(_lerpAmount));
        }
    }
}
