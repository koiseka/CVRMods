using System;

//
using MelonLoader;

namespace WorldDownloadPercentage
{
    public class Main : MelonMod
    {
        internal static readonly MelonLogger.Instance Log = new MelonLogger.Instance("WorldDownloadPercentage", ConsoleColor.DarkBlue);

        public override void OnApplicationStart()
        {
            Log.Msg("Initializing!");
            Patches.Patch(HarmonyInstance);
        }
    }
}
