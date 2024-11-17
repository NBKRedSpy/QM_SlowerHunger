using System;
using HarmonyLib;
using MGSC;
using UnityEngine;

namespace QM_SlowerHunger
{
    [HarmonyPatch(typeof(StarvationEffect), nameof(StarvationEffect.ProcessActionPoint))]
    internal class Patches
    {
        [HarmonyPrefix]
        private static void saveInitialCurrentLevel(StarvationEffect __instance, out int __state)
        {
            __state = __instance.CurrentLevel;
        }

        [HarmonyPostfix]
        private static void transformCurrentLevelToFloat(StarvationEffect __instance, int __state)
        {
            float value = Plugin.Config.HungerRateMultiplier;
            int num = Mathf.RoundToInt((float)__state - (float)(__state - __instance.CurrentLevel) * value);

            if (num >= __state)
            {
                num--;
            }

            __instance.CurrentLevel = num;
        }
    }
}
