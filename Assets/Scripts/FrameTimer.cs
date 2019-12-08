using UnityEngine;

public class FrameTimer
{
    private float _rate;
    private float _counter;
    private bool _startReady;

    // Create a timer that is checked every frame, and 'triggers' every 'every' seconds.
    // If 'startReady' is true, this timer will start ready to go off.
    public FrameTimer(float every, bool startReady)
    {
        _rate = every;
        _startReady = startReady;
        Reset();
    }

    // Call this every frame. Returns true if the timer has gone off, false otherwise.
    public bool CheckThisFrame()
    {
        _counter += Time.deltaTime;
        if (_counter >= _rate)
        {
            _counter = 0; 
            return true;
        }
        else
        {
            return false;
        }       
    }

    public void Reset()
    {
        if (_startReady)
        {
            _counter = _rate;
        } else
        {
            _counter = 0;
        }
        
    }

    public float TimeLeft
    {
        get { return _counter; }
    }
}
