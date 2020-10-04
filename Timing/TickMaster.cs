
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VideoJames.Core.Timing
{
    public enum TickUpdateType { Update, FixedUpdate, LateUpdate }

    /// <summary>
    /// Replaces the monobehaviour Update, Late Update, and Fixed Update methods.
    /// Instead classes can subscribe to the Events in <see cref="TickMaster"/>.
    /// Update etc are costly and inflexible
    /// </summary>
    public class TickMaster : SingletonBehaviour<TickMaster>
    {
        private class TickAction
        {
            public Action<float> Action;
            public TickUpdateType UpdateType;
            public float LastInvokeTime;
            public float InvokeInterval;
            public int InvokeMaximum;
            public int ActionsInvoked;
        }

        private static Dictionary<TickUpdateType, List<TickAction>> _updateActions =
            new Dictionary<TickUpdateType, List<TickAction>>
            {
                { TickUpdateType.Update, new List<TickAction>() },
                { TickUpdateType.FixedUpdate, new List<TickAction>() },
                { TickUpdateType.LateUpdate, new List<TickAction>() }
            };

        public static void AddTickAction(Action<float> action, TickUpdateType updateType, float intervalInSeconds = 0, bool executeWhenAdded = false, int invokeMaximum = -1)
        {
            if (_updateActions[updateType].Exists(x => x.Action == action)) return;

            var tickAction = new TickAction()
            {
                Action = action,
                UpdateType = updateType,
                LastInvokeTime = Time.time,
                InvokeInterval = intervalInSeconds,
                InvokeMaximum = invokeMaximum
            };

            _updateActions[updateType].Add(tickAction);

            if (executeWhenAdded) tickAction.Action.Invoke(0);
        }

        public static void RemoveTickAction(Action<float> action, TickUpdateType updateType)
        {
            _updateActions[updateType].Remove(_updateActions[updateType].Find(x => x.Action == action));
        }

        private void Update() => Tick(TickUpdateType.Update);

        private void FixedUpdate() => Tick(TickUpdateType.FixedUpdate);

        private void LateUpdate() => Tick(TickUpdateType.LateUpdate);

        private void Tick(TickUpdateType tickUpdateType)
        {
            List<TickAction> expiredActions = new List<TickAction>();

            List<TickAction> actions = _updateActions[tickUpdateType].ToList<TickAction>();

            foreach(var tickAction in actions)
            {
                if (tickAction.LastInvokeTime + tickAction.InvokeInterval <= Time.time)
                {
                    tickAction.Action.Invoke(Time.deltaTime);                    
                    if (tickAction.InvokeMaximum > -1)
                    {
                        tickAction.ActionsInvoked++;
                        if (tickAction.ActionsInvoked >= tickAction.InvokeMaximum)
                        {
                            expiredActions.Add(tickAction);
                        }
                    }
                    tickAction.LastInvokeTime += tickAction.InvokeInterval;
                }
            }

            expiredActions.ForEach(x => _updateActions[tickUpdateType].Remove(x));
        }
    }
}

