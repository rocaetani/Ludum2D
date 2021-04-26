using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralColor : MonoBehaviour
{
    public bool chooseRandomColor = true;
    public List<Color> coralPalette;

    private SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = coralPalette[Random.Range(0, coralPalette.Count)];
    }
}
