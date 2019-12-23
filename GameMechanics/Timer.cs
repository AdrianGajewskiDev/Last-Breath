using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LB.GameMechanics
{
    public class Timer : MonoBehaviour
    {
        public static Timer Singleton;

        public  Task WaitForSeconds(int milliseconds)
        {
            return Task.Delay(milliseconds);
        }

        private class TimedEvent
        {
            public float TimeToExecute;
            public Callback Method;
        }

        private List<TimedEvent> events;

        public delegate void Callback();

        private void Awake()
        {
            Singleton = this;
            events = new List<TimedEvent>();
        }

        public void Add(Callback method, float time)
        {
            events.Add(new TimedEvent { Method = method, TimeToExecute = Time.time + time });
        }

        private void Update()
        {
            if (events.Count == 0)
                return;

            for (int i = 0; i < events.Count; i++)
            {
                var timedEvent = events[i];

                if (timedEvent.TimeToExecute <= Time.time)
                {
                    timedEvent.Method();
                    events.Remove(timedEvent);
                }
            }
        }
    }
}
