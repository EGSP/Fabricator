using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Code.Entities
{
    public class ConstructionFrame : SerializedMonoBehaviour
    {
        [OdinSerialize] public Frame Frame { get; set; }
    }

    [Serializable]
    public class Frame
    {
        public float MaxVolume { get; set; }
        
        public float CurrentVolume { get; set; }

        public Frame(float maxVolume, float currentVolume)
        {
            MaxVolume = maxVolume;
            CurrentVolume = currentVolume;
        }
        
        
        public void Fill(float volume)
        {
            if (volume < 0)
                throw new InvalidOperationException();
            
            CurrentVolume += volume;
            if (CurrentVolume > MaxVolume)
                CurrentVolume = MaxVolume;
        }

        public float Remove(float volume)
        {
            if (CurrentVolume <= volume)
            {
                CurrentVolume = 0;
                return CurrentVolume;
            }

            CurrentVolume -= volume;
            return volume;
        }
    }
}
