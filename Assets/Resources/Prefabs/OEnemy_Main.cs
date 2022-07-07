using System;
using System.Collections.Generic;
using UnityEngine;
using Uliger;

public class OEnemy_Main : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1.0f;
    [SerializeField] private CAnimatorMove AnimatorMove;
    [SerializeField] private OSector_Main Sector;
    [SerializeField] private GameObject EmotionImage;
    private CMoveWay Move;
    private List<Vector2Int> Way;
    private CMoveWay.EWayDirection WayDirection = CMoveWay.EWayDirection.Back;
    private Transform TargetTransform;
    private CBoundFloat TimerTarget;

    public event Func<Transform, Transform, List<Vector2Int>> DTransformWay;

    private void Awake()
    {
        Sector.DOnPlayerCollision -= OnPlayerThreatMax;
        Sector.DOnPlayerCollision += OnPlayerThreatMax;
    }

    private void FixedUpdate()
    {
        float dt = Time.deltaTime;
        Move.Move(dt);
        if (Move.Enable)
        {
            AnimatorMove.AnimUpdate(Move.Direction);
            Sector.Angle = Move.Angle;
        }
        if (TimerTarget != null) TimerTarget.Value -= dt;
    }


    public void Init(List<Vector2Int> way)
    {
        TimerTarget = null;
        Move = new CMoveWay(transform, MoveSpeed);
        Move.DOnEndWay -= OnMoveEndWay;
        Move.DOnEndWay += OnMoveEndWay;
        Way = way;
        EmotionImage.SetActive(false);

        StartWalking();
    }

    public void OnMoveEndWay() => StartWalking();

    public void SwitchDirection()
    {
        WayDirection = (WayDirection == CMoveWay.EWayDirection.Forward) ? CMoveWay.EWayDirection.Back : CMoveWay.EWayDirection.Forward;
    }


    private void StartWalking()
    {
        SwitchDirection();
        Move.MoveWay(Way, WayDirection);
    }

    private void StartRunning()
    {
        if (TimerTarget == null)
        {
            TimerTarget = new CBoundFloat(1.0f);
            TimerTarget.DOnDown -= OnTimerTargetDown;
            TimerTarget.DOnDown += OnTimerTargetDown;

            Sector.gameObject.SetActive(false);
            EmotionImage.SetActive(true);

            OnTimerTargetDown();
        }
    }

    private void OnTimerTargetDown()
    {
        Way = DTransformWay(transform, TargetTransform);
        Move.MoveTarget(Way);
    }

    public void OnPlayerThreatMax(Transform transform)
    {
        Sector.DOnPlayerCollision -= OnPlayerThreatMax;
        TargetTransform = transform;
        StartRunning();
    }

}