using System;
using UnityEngine;

public class OPlayer_Main : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1.0f;
    [SerializeField] private Rigidbody2D SelfRigidbody;
    [SerializeField] private UIThreatInterface_Main ThreatInterface;
    [SerializeField] private CAnimatorMove AnimatorMove;
    private CThreat _Threat;

    public event Action<Transform> DOnThreatMax = delegate { };

    private CThreat Threat
    {
        get
        {
            if (_Threat == null)
            {
                _Threat = new CThreat();
                _Threat.DOnThreatMax -= OnThreatMax;
                _Threat.DOnThreatMax += OnThreatMax;
            }
            return _Threat;
        }
    }


    private void FixedUpdate()
    {
        float dt = Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(x, y);
        if (dir.magnitude <= 0.001f)
        {
            Threat.Down(dt);
        } else {
            Threat.Up(dt);
            dir.Normalize();
            SelfRigidbody.MovePosition(SelfRigidbody.position + dir * MoveSpeed * dt);
            AnimatorMove.AnimUpdate(dir);
        }

        ThreatInterface.Value = Threat.Rate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exit"))
        {
            GlobalData.Data.LevelResult = ELevelResult.Win;
            GlobalData.LoadLevelReward();
        }

        if (collision.CompareTag("Enemy"))
        {
            GlobalData.Data.LevelResult = ELevelResult.Lose;
            GlobalData.LoadLevelReward();
        }
    }

    private void OnThreatMax()
    {
        DOnThreatMax(transform);
        _Threat.DOnThreatMax -= OnThreatMax;
    }

}