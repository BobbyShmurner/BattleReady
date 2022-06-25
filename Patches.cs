using HarmonyLib;
using UnityEngine;

using System;

namespace BattleReady
{
    [HarmonyPatch(typeof(MusicManager), "PlayCleanMusic", new Type[]{})]
	class RemoveCleanMusicPatch {
		public static void Prefix(MusicManager __instance, ref float ___requestedThemes, ref bool __runOriginal) {
			__runOriginal = false;
			___requestedThemes -= 1f;
		}
		
		public static void Postfix(MusicManager __instance) {
			__instance.PlayBattleMusic();
		}
	}

	[HarmonyPatch(typeof(MusicManager), "StartMusic", new Type[]{})]
	class StartWithBattlePatch {
		public static void Postfix(ref AudioSource ___cleanTheme, ref AudioSource ___targetTheme, ref AudioSource ___battleTheme) {
			___battleTheme.volume = ___cleanTheme.volume;
			___cleanTheme.volume = 0f;

			___targetTheme = ___battleTheme;
		}
	}

	[HarmonyPatch(typeof(MusicManager), "OnEnable", new Type[]{})]
	class OnEnablePatch {
		public static void Postfix(ref AudioSource ___cleanTheme, ref AudioSource ___targetTheme, ref AudioSource ___battleTheme) {
			___battleTheme.volume = ___cleanTheme.volume;
			___cleanTheme.volume = 0f;

			___targetTheme = ___battleTheme;
		}
	}

	[HarmonyPatch(typeof(MusicManager), "ArenaMusicEnd", new Type[]{})]
	class RemoveArenaEndPatch {
		public static void Prefix(ref bool __runOriginal, ref bool ___arenaMode) {
			__runOriginal = false;
			___arenaMode = false;
		}
	}
}
