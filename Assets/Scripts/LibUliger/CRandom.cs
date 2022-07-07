using System;
using System.Collections.Generic;
using UnityEngine;

namespace Uliger
{
    public static class CRandom
    {

        public static void Randomize()
        {
            UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        }

    }
}