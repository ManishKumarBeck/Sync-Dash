using System.Collections.Generic;
using UnityEngine;

public class GameEventRecorder : MonoBehaviour
{
    public static GameEventRecorder Instance;

    public struct GameEvent
    {
        public float time;
        public EventType type;
        public Vector3 position;
    }

    public enum EventType
    {
        OrbCollected,
        ObstacleHit
    }

    private List<GameEvent> events = new List<GameEvent>();

    private void Awake()
    {
        Instance = this;
    }

    public void RecordEvent(EventType type, Vector3 position)
    {
        GameEvent e = new GameEvent
        {
            time = Time.time,
            type = type,
            position = position
        };
        events.Add(e);
    }

    public List<GameEvent> GetEventsUpTo(float currentTime, float delay)
    {
        float targetTime = currentTime - delay;
        List<GameEvent> relevant = new List<GameEvent>();

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].time <= targetTime)
                relevant.Add(events[i]);
        }

        return relevant;
    }

    public void ClearEvents()
    {
        events.Clear();
    }
}