using System;
using System.Collections;
using System.Collections.Generic;
using Game.Code;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class Unit : SerializedMonoBehaviour
{
    [FormerlySerializedAs("Speed")] public float speed;
    
    [OdinSerialize]public Navigation Navigation { get; private set; }

    private GraphicBody _graphicBody;
    
    private void Awake()
    {
        Navigation = new Navigation(this);
        Navigation.WhenStartMoving += NavigationOnWhenStartMoving;
        Navigation.WhenStopMoving += NavigationOnWhenStopMoving;
        _graphicBody = GetComponentInChildren<GraphicBody>();
    }

    private void NavigationOnWhenStopMoving()
    {
        _graphicBody.StopPingPong();
    }

    private void NavigationOnWhenStartMoving()
    {
        _graphicBody.StartPingPong(speed);
    }


    // Update is called once per frame
    void Update()
    {
        Navigation.Perform();
    }

    public void Move(Vector3 direction, float speed, float delta)
    {
        transform.position += direction * (speed * Time.deltaTime);
    }
}

[Serializable]
public class Navigation
{
    public readonly Unit Unit;

    public Vector3 Position => Unit.transform.position;
    
    public Vector3? Point { get; private set; }

    public Vector3 Direction => (Point.Value - Position).normalized;

    public event Action WhenStartMoving = () => { };
    public event Action WhenMoving = () => { };
    public event Action WhenStopMoving = () => { };
    
    public Navigation(Unit unit)
    {
        Unit = unit;
    }

    public void Perform()
    {
        if (Point is null)
            return;

        var distance = DistanceToPoint();
        if (distance >= 0.1f)
        {
            Unit.Move(Direction, Unit.speed, Time.deltaTime);
            WhenMoving();
        }
        else
        {
            Point = null;
            WhenStopMoving();
        }
    }

    public void GoToPoint(Vector3? point)
    {
        Point = point;
        WhenStartMoving();
    }

    public float DistanceToPoint()
    {
        if (Point is not null)
            return Vector3.Distance(Point.Value, Position);
        return -1;
    }
}
