using System;
using UnityEngine;

namespace Rodlix
{
    [Serializable]
    public class Octave
    {
        public float frequency = 1.0f;
        public float amplitude = 1.0f;
        [Range(0, 1)] public float lowerThreshold = 0;
        [Range(0, 1)] public float upperThreshold = 1;
    }
}