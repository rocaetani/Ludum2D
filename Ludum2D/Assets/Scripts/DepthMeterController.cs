using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthMeterController : MonoBehaviour
{
    public Transform playerPosition;
    public float maxDepth = 1;

    public GameObject chevron;

    private RectTransform _depthTransform;
    private RectTransform _chevronTransform;

    private float _startHeight;
    private float _endHeight;

    void Start()
    {
        _depthTransform = gameObject.GetComponent<RectTransform>();
        _chevronTransform = chevron.GetComponent<RectTransform>();

        _startHeight = 0;
        _endHeight = _depthTransform.sizeDelta.y - _chevronTransform.sizeDelta.y;
    }

    void Update()
    {
        float currentDepth = playerPosition.position.y;
        float depthRatio = currentDepth / maxDepth;

        float interpolatedHeight = (depthRatio * _endHeight) + _startHeight;
        // interpolatedHeight = Mathf.Min(_startHeight, interpolatedHeight);
        // interpolatedHeight = Mathf.Max(_endHeight, interpolatedHeight);

        _chevronTransform.anchoredPosition = new Vector3(_chevronTransform.anchoredPosition.x, interpolatedHeight);
        // chevron.transform.position += new Vector3(0, -Time.deltaTime, 0);
    }
}
