using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    public float transitionSeconds = 1;
    public float KeyboardUpAmount = 683;
    public RectTransform KeyboardTransform;

    private bool _isGoingUp = false;
    private Vector2 _initialPosition;
    private Vector2 _finalPosition;
    private float _lerpAmount = 0;

    void Start()
    {
        _initialPosition = KeyboardTransform.anchoredPosition;
        _finalPosition = _initialPosition + new Vector2(0, KeyboardUpAmount);
    }

    void Update()
    {
        if(!_isGoingUp && GameObjectAccess.Player.playerState == Player.PlayerState.GoingUp) {
            _isGoingUp = true;

            print("Move");
            StartCoroutine(moveKeyboard());
        }
    }

    private IEnumerator moveKeyboard() {
        while(KeyboardTransform.anchoredPosition != _finalPosition) {
            yield return new WaitForFixedUpdate();
            _lerpAmount += transitionSeconds * Time.fixedDeltaTime;

            KeyboardTransform.anchoredPosition = Vector2.Lerp(_initialPosition, _finalPosition, Utils.easeOutQuart(_lerpAmount));
        }
    }
}
