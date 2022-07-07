using System;
using System.Collections.Generic;
using UnityEngine;

public class CMoveWay
{
    public enum EWayDirection
    {
        Forward,
        Back
    }

    private Transform SelfTransform;
    private float MoveSpeed;
    private bool _Enable;
    private List<Vector2Int> Way;
    private int TargetPoint;
    private Vector3 Target;
    private Vector2 _Direction;
    private float _Angle;

    public event Action DOnEndWay;
    private event Action<float> DMove;
    private event Func<bool> DNextTarget;


    public bool Enable { get => _Enable; }
    public Vector2 Direction { get => _Direction; }
    public float Angle { get => _Angle; }


    public CMoveWay(Transform transform, float movespeed)
    {
        SelfTransform = transform;
        MoveSpeed = movespeed;
        _Enable = false;
    }

    public void Move(float dt)
    {
        if (_Enable) DMove(dt);
    }


    public void MoveWay(List<Vector2Int> way, EWayDirection waydirection)
    {
        Way = way;
        if (Way != null)
        {
            _Enable = true;
            DMove = _MoveWay;
            if (waydirection == EWayDirection.Forward)
            {
                DNextTarget = _NextTargetForward;
                TargetPoint = 0;
            } else {
                DNextTarget = _NextTargetBack;
                TargetPoint = Way.Count - 1;
            }
            DNextTarget();
        }
    }
    public void MoveTarget(List<Vector2Int> way)
    {
        Way = way;
        if (Way != null)
        {
            _Enable = true;
            DMove = _MoveWay;
            DNextTarget = _NextTargetForward;
            TargetPoint = 0;
            DNextTarget();
        }
    }

    private void _MoveWay(float dt)
    {
        float speed_dt = MoveSpeed * dt;
        Vector3 position = SelfTransform.position;
        Vector3 dir = Target - position;
        if (dir.sqrMagnitude < 3 * speed_dt)
        {
            if (DNextTarget() == false)
            {
                _Enable = false;
                DOnEndWay();
            }
        } else {
            dir.Normalize();
            position += dir * speed_dt;
            _Angle = Mathf.Sign(-dir.x) * Mathf.Rad2Deg * Mathf.Acos(dir.y);
            _Direction = dir;
            ///SelfTransform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, _Angle));
            SelfTransform.position = position;
        }
    }

    private bool _NextTargetForward()
    {
        TargetPoint++;
        if (TargetPoint < Way.Count)
        {
            Vector2Int point = Way[TargetPoint];
            Target = new Vector3(point.x, point.y);
            return true;
        }
        return false;
    }
    private bool _NextTargetBack()
    {
        TargetPoint--;
        if (TargetPoint >= 0)
        {
            Vector2Int point = Way[TargetPoint];
            Target = new Vector3(point.x, point.y);
            return true;
        }
        return false;
    }

}