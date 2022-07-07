using System;
using Uliger;

public class CThreat
{
    private const float MAXTHREAT = 10.0f;
    private CBoundFloat _Threat;
    private float SpeedUp = 3.0f;
    private float SpeedDown = 2.0f;

    public event Action DOnThreatMax = delegate { };

    private CBoundFloat Threat
    {
        get
        {
            if (_Threat == null) _Threat = new CBoundFloat(0, 0, MAXTHREAT);
            return _Threat;
        }
    }
    public float Rate { get => Threat.Rate(); }

    public CThreat()
    {
        Threat.DOnUp -= OnThreatUp;
        Threat.DOnUp += OnThreatUp;
    }
    ~CThreat()
    {
        Threat.DOnUp -= OnThreatUp;
    }

    private void OnThreatUp() => DOnThreatMax();

    public void Up(float dt) => Threat.Value += SpeedUp * dt;
    public void Down(float dt) => Threat.Value -= SpeedDown * dt;

}