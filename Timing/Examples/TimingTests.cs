using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoJames.Core.Timing;

namespace VideoJames.Core.Timing.Examples
{
    public class TimingTests : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            TickMaster.AddTickAction(SingleUseTimer, TickUpdateType.Update, 1, false, 1);
        }

        private void SingleUseTimer(float deltaTime)
        {
            Debug.Log("Single use timer finished");
        }
    }
}