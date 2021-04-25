using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    public float Velocity;


    private Vector3 _direction;

    public bool GoingUp;


    private int _currentSeconds;

    [Header("Air Bar")]
    public float AirMaximum;
    public float AirAmount;
    public float DecreasePerSecond;
    public float DecreasePerSecondPerButton;
    public float AirRatio {
        get {
            return AirAmount / AirMaximum;
        }
    }

    void Start()
    {
        AirAmount = AirMaximum;
        _direction = Vector3.down;
        GoingUp = false;
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
            GoingUp = true;
        }
        transform.position += Time.deltaTime * Velocity * _direction;
    }

    private void LoseAir()
    {
        AirAmount -= DecreasePerSecond * Time.deltaTime;
    }

    public void AddAirAmount(float airToAdd)
    {
        if (AirMaximum > AirAmount + airToAdd)
        {
            AirAmount += airToAdd;
        }
        else
        {
            AirAmount = AirMaximum;
        }
    }

    public int GoingDirection()
    {
        if (GoingUp)
        {
            return 1;
        }

        return -1;
    }

    void OnGUI()
    {
        GUI.color = Color.red;



        GUI.Label(new Rect(100, 10, 150, 100), AirAmount + "");

        GUI.Label(new Rect(100, 20, 150, 100), Time.time + "");

        GUI.Label(new Rect(100, 30, 150, 100), transform.position.y + "");
    }

}
