using OpenTK.Input;
using System;

namespace MerpEngine
{
    [Serializable]
    public class Axies
    {
        public string Name;
        public Key PositiveKey;
        public Key NegitiveKey;

        public bool Inverted = false;

        public float GetAxies()
        {
            if (Input.KeyDown(PositiveKey)) return (Inverted) ? -1f : 1f;
            if (Input.KeyDown(NegitiveKey)) return (Inverted) ? 1f : -1f;
            return 0;
        }

        public bool Preseed()
        {
            if (Input.KeyDown(PositiveKey) || Input.KeyDown(NegitiveKey)) return false;
            return false;
        }
    }
}
