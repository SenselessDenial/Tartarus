using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tartarus.Ease;

namespace Tartarus
{
    public class TimingDiagram : Component
    {
        public float Value { get; private set; }
        private float time;

        private readonly List<TimingRequest> requests;
        private readonly List<ActionRequest> actions;
        private readonly List<ActionRequest> completedActions;

        public bool IsRunning { get; private set; }
        private float backdrop;
        private TimingRequest currentRequest;

        //private static float cutoff = 0.005f;


        public TimingDiagram(Entity entity) : base(entity, true, false)
        {
            requests = new List<TimingRequest>();
            actions = new List<ActionRequest>();
            completedActions = new List<ActionRequest>();
            Value = 0f;
            time = 0f;
            backdrop = 0f;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Pause()
        {
            IsRunning = false;
        }

        public void Stop()
        {
            IsRunning = false;
            time = 0f;
        }

        public bool Check(float time)
        {
            return this.time >= time;
        }

        public void Add(float startValue, float endValue, float duration, Easing function)
        {
            requests.Add(new TimingRequest(startValue, endValue, duration, function));
            currentRequest = FindCurrentRequest();
        }

        public void AddMaintain(float duration)
        {
            requests.Add(new TimingRequest(0, 0, duration, Maintain));
            currentRequest = FindCurrentRequest();
        }

        public void AddAction(float timeToInvoke, Action action)
        {
            actions.Add(new ActionRequest(timeToInvoke, action));
        }

        public void AddAction(Action action)
        {
            float t = 0;
            foreach (var item in requests)
            {
                t += item.Duration;
            }
            AddAction(t, action);
        }



        public override void Update()
        {
            if (IsRunning)
                time += TartarusGame.DeltaTime;

            if (time > backdrop + currentRequest.Duration)
            {
                backdrop += currentRequest.Duration;
                currentRequest = FindCurrentRequest();
            }

            if (currentRequest.Function != Maintain)
            {
                Value = Calculate(time - backdrop,
                currentRequest.Duration,
                currentRequest.StartValue,
                currentRequest.EndValue,
                currentRequest.Function);
            }

            foreach (var item in actions)
            {
                if (time > item.Time && !completedActions.Contains(item))
                {
                    item.Action.Invoke();
                    completedActions.Add(item);
                }
            }
        }

        private TimingRequest FindCurrentRequest()
        {
            float tempBackdrop = 0f;
            foreach (var item in requests)
            {
                if (time < item.Duration + tempBackdrop)
                {
                    return item;
                }
                tempBackdrop += item.Duration;
            }
            return new TimingRequest(0, 0, 0, Maintain);
        }


        private struct TimingRequest
        {
            public float StartValue;
            public float EndValue;
            public float Duration;
            public Easing Function;

            public TimingRequest(float startValue, float endValue, float duration, Easing function)
            {
                StartValue = startValue;
                EndValue = endValue;
                Duration = duration;
                Function = function;
            }
        }

        private struct ActionRequest
        {
            public float Time;
            public Action Action;

            public ActionRequest(float time, Action action)
            {
                Time = time;
                Action = action;
            }
        }

        private static Easing Maintain = t => 0f;






    }
}
