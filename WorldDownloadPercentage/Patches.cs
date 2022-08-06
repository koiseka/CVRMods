using System;

//
using ABI_RC.Core.IO;
using ABI_RC.Core.Player;
using HarmonyLib;
using System.Reflection;
using ABI_RC.Core.Base;

namespace WorldDownloadPercentage
{
    public class Patches
    {
        public static string PreloadWorldId;
        public static DownloadJob PreloadWorldJob;
        public static void Patch(HarmonyLib.Harmony harmonyInstance)
        {
            //Patches
            harmonyInstance.Patch(typeof(HudOperations).GetMethod("LoadWorldIndicator"), prefix: new HarmonyMethod(typeof(Patches).GetMethod("OnLoadWorldIndicator", BindingFlags.Static | BindingFlags.NonPublic)));
            harmonyInstance.Patch(typeof(Content).GetMethod("PreloadWorld"), postfix: new HarmonyMethod(typeof(Patches).GetMethod("OnPreloadWorld", BindingFlags.Static | BindingFlags.NonPublic)));

            Main.Log.Msg("Harmony patches completed!");
        }

        private static bool OnLoadWorldIndicator(bool reset, int stage, float value)
        {
            switch (stage)
            {
                case 0:
                    HudOperations.Instance.worldLoadStatus.text = "Verifying World Version";
                    break;
                case 1:
                    if (PreloadWorldJob != null && PreloadWorldJob.Status != DownloadJob.ExecutionStatus.JobDone && PreloadWorldJob.Status != DownloadJob.ExecutionStatus.DownloadComplete)
                    {
                        HudOperations.Instance.worldLoadStatus.text = "Downloading World: " + PreloadWorldJob.Progress + "%";
                    }
                    else if (value > 0f)
                    {
                        HudOperations.Instance.worldLoadStatus.text = "Downloading World: " + Math.Round((double)(value * 100f), 0) + "%";
                    }
                    else
                    {
                        HudOperations.Instance.worldLoadStatus.text = "Downloading World: " + CVRDownloadManager.Instance.TryGetCurentJoiningWorldDownloadPercentage() + "%";
                    }
                    break;
                case 2:
                    if (value > 0f)
                    {
                        HudOperations.Instance.worldLoadStatus.text = "Loading World Bundle: " + Math.Round((double)(value * 100f), 0) + "%";
                    }
                    else
                    {
                        HudOperations.Instance.worldLoadStatus.text = "Loading World Bundle";
                    }
                    break;
            }
            if (reset)
            {
                HudOperations.Instance.worldLoadStatus.text = string.Empty;
            }
            return false;
        }

        private static void OnPreloadWorld(string worldId)
        {
            PreloadWorldId = worldId;
            PreloadWorldJob = CVRDownloadManager.Instance.AllDownloadJobs.Find((DownloadJob x) => x.ObjectId == worldId);
        }
    }
}
