using System.Collections;
using UnityEngine;

namespace LB.GameMechanics
{
    public static class Timer
    {
        public static IEnumerator WaitForSeconds(float s)
        {
            yield return new WaitForSeconds(s);
        }

    }
}
