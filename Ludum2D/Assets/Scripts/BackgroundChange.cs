using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    [Header("Depth and Color Changes")]
    public List<float> depthChanges;
    public List<Color> colorChanges;

    private Camera _mainCamera;
    private Transform _playerPosition;

    void Start()
    {
        if(depthChanges.Count != colorChanges.Count)
        {
            Debug.LogError("Depth Changes e Color Changes devem ter o mesmo tamanho");
        }

        _mainCamera = GameObjectAccess.MainCamera;
        _playerPosition = GameObjectAccess.Player.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Color from = colorChanges[0];
        Color to = colorChanges[colorChanges.Count - 1];
        float interpolateAmount = 0;

        float currentDepth = _playerPosition.position.y;

        for(int i = depthChanges.Count - 1; i >= 0 ; i--)
        {
            // Found depth interval
            if(depthChanges[i] >= currentDepth) {
                from = colorChanges[i];
                to = colorChanges[Mathf.Min(colorChanges.Count, i+1)];

                float distanceFromDepth = Mathf.Abs(currentDepth - depthChanges[i]);
                float distanceBetweenDepths = Mathf.Abs(depthChanges[Mathf.Min(colorChanges.Count, i+1)] - depthChanges[i]);
                interpolateAmount = distanceFromDepth / distanceBetweenDepths;
                break;
            }
        }

        Color interpolatedColor = Color.Lerp(from, to, interpolateAmount);
        _mainCamera.backgroundColor = interpolatedColor;
    }

}
