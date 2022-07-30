using System;

//
using ABI_RC.Core.IO;
using MelonLoader;
using System.Collections;

namespace WorldDownloadPercentage
{
    public class Main : MelonMod
    {
        internal static readonly MelonLogger.Instance Log = new MelonLogger.Instance("WorldDownloadPercentage", ConsoleColor.DarkBlue);

        public override void OnApplicationStart()
        {
            //MelonCoroutines.Start(Initialize());
            Log.Msg("Initializing!");
            Patches.Patch(HarmonyInstance);
        }

        private IEnumerator Initialize()
        {
            while (ReferenceEquals(CVRDownloadManager.Instance, null))
                yield return null;

            Log.Msg("Initializing!");
        }

    }
}
