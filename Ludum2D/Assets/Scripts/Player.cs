using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    
    [Header("Movement")]
    public float Velocity;


    private Vector3 _direction;

    private int _airBar;    

    private int _currentSeconds;
    
    [Header("Air Bar")]
    
    public int ThresholdSeconds;

    public int AmountToDecrease;

    void Start()
    {
        _direction = Vector3.down;
        _airBar = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LoseAir();


    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _direction = Vector3.up;
        }
        transform.position += Time.deltaTime * Velocity * _direction;
    }

    private void LoseAir()
    {
        if (TruncateTime() - _currentSeconds >= ThresholdSeconds)
        {
            _currentSeconds = TruncateTime();
            DecreaseAirBar();
        }
    }

    private void DecreaseAirBar()
    {
        _airBar -= AmountToDecrease;
    }

    private int TruncateTime()
    {
        return Mathf.FloorToInt(Time.time);
    }

    private void ChangeDecrease(int newAmountToDecrease)
    {
        AmountToDecrease = newAmountToDecrease;
    }

    void OnGUI()
    {
        GUI.color = Color.yellow;
        
       
        
        GUI.Label(new Rect(10, 10, 150, 100), _airBar + "");
        GUI.Label(new Rect(10, 10, 150, 100), _airBar + "");
        
        GUI.Label(new Rect(10, 20, 150, 100), Time.time + "");

    }
    
}
