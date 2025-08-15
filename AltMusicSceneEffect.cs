using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Events;
using static Terraria.Main;
using Microsoft.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace FargoAltMusicMod
{
    static class MusicUtils
    {
        private static Mod souls = null;
        private static bool checkedSouls = false;
        public static Mod Souls
        {
            get
            {
                if (!checkedSouls)
                {
                    checkedSouls = true;
                    if (ModLoader.HasMod("FargowiltasSouls"))
                        souls = ModLoader.GetMod("FargowiltasSouls");
                }
                return souls;
            }
        }
        public static NPC FindClosestBoss(int type)
        {
            float num = 99999;
            NPC closestNPC = null;
            foreach (NPC npc in npc.Where(n => n != null && n.active && n.type == type))
            {
                if (npc.BossMusicRange() && npc.Distance(LocalPlayer.Center) < num)
                {
                    num = npc.Distance(LocalPlayer.Center);
                    closestNPC = npc;
                }
            }
            return closestNPC;
        }

        public static NPC FindClosestSoulsBoss(string name)
        {
            if (MusicUtils.Souls == null)
                return null;
            return FindClosestBoss(Souls.Find<ModNPC>(name).Type);
        }

        public static bool ZoneShallow(this Player player) => player.ZoneDirtLayerHeight || player.ZoneOverworldHeight;

        public static bool ZoneUnderground(this Player player) => player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight;

        public static bool BossMusicRange(this NPC npc)
        {
            int range = 5500;
            Rectangle value = new Rectangle((int)(npc.position.X + (float)(npc.width / 2)) - range, (int)(npc.position.Y + (float)(npc.height / 2)) - range, range * 2, range * 2);
            Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
            return (rectangle.Intersects(value));
        }

        public static int CountPillars()
        {
            int solar = NPC.CountNPCS(NPCID.LunarTowerSolar);
            int vortex = NPC.CountNPCS(NPCID.LunarTowerVortex);
            int nebula = NPC.CountNPCS(NPCID.LunarTowerNebula);
            int stardust = NPC.CountNPCS(NPCID.LunarTowerStardust);
            return solar + vortex + nebula + stardust;
        }

    }

    public class VanillaMusic : ModSystem
    {
        public static int Current = 0;
        // This is the entire vanilla music choice method. Edited to update CurrentVanillaMusic rather than Main.curMusic.
        #region Vanilla Music Update
        public override void PostUpdateEverything()
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            bool flag9 = false;
            bool flag10 = false;
            bool flag11 = false;
            bool flag12 = false;
            bool flag13 = false;
            bool flag14 = false;
            bool flag15 = false;
            bool flag16 = false;
            bool flag17 = false;
            bool flag18 = LocalPlayer.townNPCs > 2f;
            bool flag19 = slimeRain;
            if (Main.SceneMetrics.ShadowCandleCount > 0 || LocalPlayer.inventory[LocalPlayer.selectedItem].type == 5322)
            {
                flag18 = false;
            }
            float num = 0f;
            /*
            for (int i = 0; i < maxMusic; i++)
            {
                if (musicFade[i] > num)
                {
                    num = musicFade[i];
                    if (num == 1f)
                    {
                        lastMusicPlayed = i;
                    }
                }
            }
            */
            /*
            if (lastMusicPlayed == 50)
            {
                musicNoCrossFade[51] = true;
            }
            */
            if (!showSplash)
            {
                Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle((int)screenPosition.X, (int)screenPosition.Y, screenWidth, screenHeight);
                int num2 = 5000;
                for (int j = 0; j < 200; j++)
                {
                    if (!npc[j].active)
                    {
                        continue;
                    }
                    num2 = 5000;
                    int num3 = 0;
                    switch (npc[j].type)
                    {
                        case 13:
                        case 14:
                        case 15:
                        case 127:
                        case 128:
                        case 129:
                        case 130:
                        case 131:
                            num3 = 1;
                            break;
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 111:
                        case 471:
                            num3 = 11;
                            break;
                        case 113:
                        case 114:
                        case 125:
                        case 126:
                            num3 = 2;
                            break;
                        case 134:
                        case 135:
                        case 136:
                        case 143:
                        case 144:
                        case 145:
                        case 266:
                            num3 = 3;
                            break;
                        case 212:
                        case 213:
                        case 214:
                        case 215:
                        case 216:
                        case 491:
                            num3 = 8;
                            break;
                        case 245:
                            num3 = 4;
                            break;
                        case 222:
                            num3 = 5;
                            break;
                        case 262:
                        case 263:
                        case 264:
                            num3 = 6;
                            break;
                        case 381:
                        case 382:
                        case 383:
                        case 385:
                        case 386:
                        case 388:
                        case 389:
                        case 390:
                        case 391:
                        case 395:
                        case 520:
                            num3 = 9;
                            break;
                        case 398:
                            num3 = 7;
                            break;
                        case 422:
                        case 493:
                        case 507:
                        case 517:
                            num3 = 10;
                            break;
                        case 439:
                            num3 = 4;
                            break;
                        case 438:
                            if (npc[j].ai[1] == 1f)
                            {
                                num2 = 1600;
                                num3 = 4;
                            }
                            break;
                        case 657:
                            num3 = 13;
                            break;
                        case 636:
                            num3 = 14;
                            break;
                        case 370:
                            num3 = 15;
                            break;
                        case 668:
                            num3 = 16;
                            break;
                    }
                    if (NPCID.Sets.BelongsToInvasionOldOnesArmy[npc[j].type])
                    {
                        num3 = 12;
                    }
                    if (num3 == 0 && npc[j].boss)
                    {
                        num3 = 1;
                    }
                    if (remixWorld && getGoodWorld && (npc[j].type == 127 || npc[j].type == 134 || npc[j].type == 125 || npc[j].type == 126))
                    {
                        num3 = 17;
                    }
                    if (num3 == 0)
                    {
                        continue;
                    }
                    Rectangle value = new Rectangle((int)(npc[j].position.X + (float)(npc[j].width / 2)) - num2, (int)(npc[j].position.Y + (float)(npc[j].height / 2)) - num2, num2 * 2, num2 * 2);
                    if (rectangle.Intersects(value))
                    {
                        switch (num3)
                        {
                            case 1:
                                flag = true;
                                break;
                            case 2:
                                flag3 = true;
                                break;
                            case 3:
                                flag4 = true;
                                break;
                            case 4:
                                flag5 = true;
                                break;
                            case 5:
                                flag6 = true;
                                break;
                            case 6:
                                flag7 = true;
                                break;
                            case 7:
                                flag8 = true;
                                break;
                            case 8:
                                flag9 = true;
                                break;
                            case 9:
                                flag10 = true;
                                break;
                            case 10:
                                flag11 = true;
                                break;
                            case 11:
                                flag12 = true;
                                break;
                            case 12:
                                flag13 = true;
                                break;
                            case 13:
                                flag14 = true;
                                break;
                            case 14:
                                flag15 = true;
                                break;
                            case 15:
                                flag16 = true;
                                break;
                            case 16:
                                flag2 = true;
                                break;
                            case 17:
                                flag17 = true;
                                break;
                        }
                        break;
                    }
                }
            }
            _ = (screenPosition.X + (float)(screenWidth / 2)) / 16f;

            if (musicVolume == 0f)
            {
                Current = 0;
                return;
            }
            if (gameMenu)
            {
                if (netMode != 2)
                {
                    if (WorldGen.drunkWorldGen)
                    {
                        if (WorldGen.remixWorldGen)
                        {
                            Current = 70;
                        }
                        else
                        {
                            Current = 60;
                        }
                    }
                    else if (WorldGen.remixWorldGen)
                    {
                        Current = 8;
                    }
                    else if (menuMode == 3000)
                    {
                        Current = 89;
                    }
                    else if (WorldGen.tenthAnniversaryWorldGen)
                    {
                        Current = 11;
                    }
                    /* these are just menu musics anyway so wont ever be relevant
                    else if (playOldTile)
                    {
                        Current = 6;
                    }
                    else if (!_isAsyncLoadComplete)
                    {
                        Current = 50;
                    }
                    */
                    else if (!audioSystem.IsTrackPlaying(50))
                    {
                        Current = 51;
                        /*
                        if (musicNoCrossFade[51])
                        {
                            musicFade[51] = 1f;
                        }
                        */
                    }
                }
                else
                {
                    Current = 0;
                }
                return;
            }
            float num4 = (float)maxTilesX / 4200f;
            num4 *= num4;
            float num5 = (float)((double)((screenPosition.Y + (float)(screenHeight / 2)) / 16f - (65f + 10f * num4)) / (worldSurface / 5.0));
            if (CreditsRollEvent.IsEventOngoing)
            {
                Current = 89;
            }
            else if (player[myPlayer].happyFunTorchTime)
            {
                Current = 13;
            }
            else if (flag8)
            {
                Current = 38;
            }
            else if (flag17)
            {
                Current = 81;
            }
            else if (flag10)
            {
                Current = 37;
            }
            else if (flag11)
            {
                Current = 34;
            }
            else if (flag7)
            {
                Current = 24;
            }
            else if (flag15)
            {
                Current = 57;
            }
            else if (flag16)
            {
                Current = 58;
            }
            else if (flag3)
            {
                Current = 12;
            }
            else if (flag)
            {
                Current = 5;
            }
            else if (flag4)
            {
                Current = 13;
            }
            else if (flag5)
            {
                Current = 17;
            }
            else if (flag6)
            {
                Current = 25;
            }
            else if (flag14)
            {
                Current = 56;
            }
            else if (flag2)
            {
                Current = 90;
            }
            else if (flag9)
            {
                Current = 35;
            }
            else if (flag12)
            {
                Current = 39;
            }
            else if (flag13)
            {
                Current = 41;
            }
            else if (eclipse && !remixWorld && (double)player[myPlayer].position.Y < worldSurface * 16.0 + (double)(screenHeight / 2))
            {
                Current = 27;
            }
            else if (eclipse && remixWorld && (double)player[myPlayer].position.Y > rockLayer * 16.0)
            {
                Current = 27;
            }
            else if (flag19 && !player[myPlayer].ZoneGraveyard && (!bloodMoon || dayTime) && (double)player[myPlayer].position.Y < worldSurface * 16.0 + (double)(screenHeight / 2))
            {
                Current = 48;
            }
            else if (remixWorld && bloodMoon && !player[myPlayer].ZoneCrimson && !player[myPlayer].ZoneCorrupt && (double)player[myPlayer].position.Y > rockLayer * 16.0 && player[myPlayer].position.Y <= (float)(UnderworldLayer * 16))
            {
                Current = 2;
            }
            else if (remixWorld && bloodMoon && player[myPlayer].position.Y > (float)(UnderworldLayer * 16) && (double)(player[myPlayer].Center.X / 16f) > (double)maxTilesX * 0.37 + 50.0 && (double)(player[myPlayer].Center.X / 16f) < (double)maxTilesX * 0.63)
            {
                Current = 2;
            }
            else if (player[myPlayer].ZoneShimmer)
            {
                Current = 91;
            }
            else if (flag18 && dayTime && ((cloudAlpha == 0f && !_shouldUseWindyDayMusic) || (double)player[myPlayer].position.Y >= worldSurface * 16.0 + (double)(screenHeight / 2)) && !player[myPlayer].ZoneGraveyard)
            {
                Current = 46;
            }
            else if (flag18 && !dayTime && ((!bloodMoon && cloudAlpha == 0f) || (double)player[myPlayer].position.Y >= worldSurface * 16.0 + (double)(screenHeight / 2)) && !player[myPlayer].ZoneGraveyard)
            {
                Current = 47;
            }
            else if (player[myPlayer].ZoneSandstorm)
            {
                Current = 40;
            }
            else if (player[myPlayer].position.Y > (float)(UnderworldLayer * 16))
            {
                Current = 36;
            }
            else if (num5 < 1f)
            {
                Current = (dayTime ? 42 : 15);
            }
            else if (tile[(int)(player[myPlayer].Center.X / 16f), (int)(player[myPlayer].Center.Y / 16f)].WallType == 87)
            {
                Current = 26;
            }
            else if (player[myPlayer].ZoneDungeon)
            {
                Current = 23;
            }
            else if ((bgStyle == 9 && (double)player[myPlayer].position.Y < worldSurface * 16.0 + (double)(screenHeight / 2)) || undergroundBackground == 2)
            {
                Current = 29;
            }
            else if (player[myPlayer].ZoneCorrupt)
            {
                if (player[myPlayer].ZoneCrimson && Main.SceneMetrics.BloodTileCount > Main.SceneMetrics.EvilTileCount)
                {
                    if ((double)player[myPlayer].position.Y > worldSurface * 16.0 + (double)(screenHeight / 2))
                    {
                        Current = 33;
                    }
                    else
                    {
                        Current = 16;
                    }
                }
                else if ((double)player[myPlayer].position.Y > worldSurface * 16.0 + (double)(screenHeight / 2))
                {
                    Current = 10;
                }
                else
                {
                    Current = 8;
                }
            }
            else if (player[myPlayer].ZoneCrimson)
            {
                if ((double)player[myPlayer].position.Y > worldSurface * 16.0 + (double)(screenHeight / 2))
                {
                    Current = 33;
                }
                else
                {
                    Current = 16;
                }
            }
            else if (player[myPlayer].ZoneMeteor)
            {
                Current = 2;
            }
            else if (player[myPlayer].ZoneGraveyard)
            {
                Current = 53;
            }
            else if (player[myPlayer].ZoneJungle)
            {
                if (remixWorld)
                {
                    if ((double)player[myPlayer].position.Y > rockLayer * 16.0 + (double)(screenHeight / 2))
                    {
                        Current = 7;
                    }
                    else if (newMusic == 7 && (double)player[myPlayer].position.Y > (rockLayer - 50.0) * 16.0 + (double)(screenHeight / 2))
                    {
                        Current = 7;
                    }
                    else if ((double)player[myPlayer].position.Y < worldSurface * 16.0 + (double)(screenHeight / 2))
                    {
                        Current = (dayTime ? 42 : 15);
                    }
                    else
                    {
                        Current = 54;
                    }
                }
                else if ((double)player[myPlayer].position.Y > rockLayer * 16.0 + (double)(screenHeight / 2))
                {
                    Current = 54;
                }
                else if (newMusic == 54 && (double)player[myPlayer].position.Y > (rockLayer - 50.0) * 16.0 + (double)(screenHeight / 2))
                {
                    Current = 54;
                }
                else if (_shouldUseStormMusic && (double)player[myPlayer].position.Y < worldSurface * 16.0 + (double)(screenHeight / 2))
                {
                    Current = 52;
                }
                else if (dayTime)
                {
                    Current = 7;
                }
                else
                {
                    Current = 55;
                }
            }
            else if (player[myPlayer].ZoneSnow)
            {
                if ((double)player[myPlayer].position.Y > worldSurface * 16.0 + (double)(screenHeight / 2))
                {
                    if (remixWorld && (double)player[myPlayer].position.Y > rockLayer * 16.0 + (double)(screenHeight / 2))
                    {
                        Current = 14;
                    }
                    else
                    {
                        Current = 20;
                    }
                }
                else if (remixWorld)
                {
                    Current = (dayTime ? 42 : 15);
                }
                else
                {
                    Current = 14;
                }
            }
            else if ((double)player[myPlayer].position.Y >= worldSurface * 16.0 + (double)(screenHeight / 2) && (remixWorld || !WorldGen.oceanDepths((int)(screenPosition.X + (float)(screenWidth / 2)) / 16, (int)(screenPosition.Y + (float)(screenHeight / 2)) / 16)))
            {
                if (player[myPlayer].ZoneHallow)
                {
                    if (remixWorld && (double)player[myPlayer].position.Y >= rockLayer * 16.0 + (double)(screenHeight / 2))
                    {
                        Current = 9;
                    }
                    else
                    {
                        Current = 11;
                    }
                }
                else if (player[myPlayer].ZoneUndergroundDesert)
                {
                    if ((double)player[myPlayer].position.Y >= worldSurface * 16.0 + (double)(screenHeight / 2))
                    {
                        Current = 61;
                    }
                    else
                    {
                        Current = 21;
                    }
                }
                else
                {
                    int ugMusicHolder = ugMusic;
                    if (ugMusicHolder == 0)
                    {
                        ugMusicHolder = 4;
                    }
                    if (!audioSystem.IsTrackPlaying(4) && !audioSystem.IsTrackPlaying(31))
                    {
                        /*
                        if (musicFade[4] == 1f)
                        {
                            musicFade[31] = 1f;
                        }
                        if (musicFade[31] == 1f)
                        {
                            musicFade[4] = 1f;
                        }
                        */
                        switch (rand.Next(2))
                        {
                            case 0:
                                ugMusicHolder = 4;
                                //musicFade[31] = 0f;
                                break;
                            case 1:
                                ugMusicHolder = 31;
                                // musicFade[4] = 0f;
                                break;
                        }
                    }
                    Current = ugMusicHolder;
                    if (remixWorld && (double)(player[myPlayer].position.Y / 16f) > rockLayer && player[myPlayer].position.Y / 16f < (float)(maxTilesY - 350))
                    {
                        if (cloudAlpha > 0f)
                        {
                            Current = 19;
                        }
                        else if (player[myPlayer].ZoneDesert)
                        {
                            Current = 21;
                        }
                        else if (_shouldUseWindyDayMusic)
                        {
                            Current = 44;
                        }
                    }
                }
            }
            else if (dayTime && player[myPlayer].ZoneHallow)
            {
                if (_shouldUseStormMusic)
                {
                    Current = 52;
                }
                else if (cloudAlpha > 0f && !gameMenu)
                {
                    Current = 19;
                }
                else if (_shouldUseWindyDayMusic && !remixWorld)
                {
                    Current = 44;
                }
                else
                {
                    Current = 9;
                }
            }
            else if (_shouldUseStormMusic)
            {
                if (bloodMoon)
                {
                    Current = 2;
                }
                else
                {
                    Current = 52;
                }
            }
            else if (WorldGen.oceanDepths((int)(screenPosition.X + (float)(screenWidth / 2)) / 16, (int)(screenPosition.Y + (float)(screenHeight / 2)) / 16))
            {
                if (bloodMoon)
                {
                    Current = 2;
                }
                else if (flag18)
                {
                    if (dayTime)
                    {
                        Current = 46;
                    }
                    else
                    {
                        Current = 47;
                    }
                }
                else
                {
                    Current = (dayTime ? 22 : 43);
                }
            }
            else if (player[myPlayer].ZoneDesert)
            {
                if ((double)player[myPlayer].position.Y >= worldSurface * 16.0)
                {
                    int num6 = (int)(player[myPlayer].Center.X / 16f);
                    int num7 = (int)(player[myPlayer].Center.Y / 16f);
                    if (WorldGen.InWorld(num6, num7) && (WallID.Sets.Conversion.Sandstone[tile[num6, num7].WallType] || WallID.Sets.Conversion.HardenedSand[tile[num6, num7].WallType]))
                    {
                        Current = 61;
                    }
                    else
                    {
                        Current = 21;
                    }
                }
                else
                {
                    Current = 21;
                }
            }
            else if (remixWorld)
            {
                Current = (dayTime ? 42 : 15);
            }
            else if (dayTime)
            {
                if (cloudAlpha > 0f && !gameMenu)
                {
                    if (time < 10800.0)
                    {
                        Current = 59;
                    }
                    else
                    {
                        Current = 19;
                    }
                }
                else
                {
                    int dayMusicHolder = dayMusic;
                    if (dayMusic == 0)
                    {
                        dayMusicHolder = 1;
                    }
                    if (!audioSystem.IsTrackPlaying(1) && !audioSystem.IsTrackPlaying(18))
                    {
                        if (rand.Next(2) == 0)
                        {
                            dayMusicHolder = 1;
                        }
                        else
                        {
                            dayMusicHolder = 18;
                        }
                    }
                    Current = dayMusic;
                    if (_shouldUseWindyDayMusic && !remixWorld)
                    {
                        Current = 44;
                    }
                }
            }
            else if (!dayTime)
            {
                if (bloodMoon)
                {
                    Current = 2;
                }
                else if (cloudAlpha > 0f && !gameMenu)
                {
                    Current = 19;
                }
                else
                {
                    Current = 3;
                }
            }
            if (((double)(screenPosition.Y / 16f) < worldSurface + 10.0 || remixWorld) && pumpkinMoon)
            {
                Current = 30;
            }
            if (((double)(screenPosition.Y / 16f) < worldSurface + 10.0 || remixWorld) && snowMoon)
            {
                Current = 32;
            }
        }
        #endregion
    }
    /*
    public class CustomMusicSystem : ModSystem
    {
        public static int Music(string musicName) => MusicLoader.GetMusicSlot(FargoAltMusicMod.Instance, $"Music/{musicName}");
        public override void Load()
        {
            OnNewMusicHook = new(UpdateAudio_DecideOnNewMusic, DecideOnNewMusicDetour);
            OnNewMusicHook.Apply();
        }
        public override void Unload()
        {
            OnNewMusicHook.Undo();
        }
        public delegate void Orig_DecideOnNewMusic(Main self);

        private static readonly MethodInfo UpdateAudio_DecideOnNewMusic = typeof(Main).GetMethod("UpdateAudio_DecideOnNewMusic", FargoSoulsUtil.UniversalBindingFlags);

        Hook OnNewMusicHook;

        internal static void DecideOnNewMusicDetour(Orig_DecideOnNewMusic orig, Main self)
        {
            orig(self);
            VanillaMusic.Current = curMusic;
        }
    }
    */
    abstract class MusicEffect : ModSceneEffect
    {
        public abstract bool Config { get; }
        public abstract string MusicName { get; }
        public override int Music => MusicLoader.GetMusicSlot(Mod, $"Music/{MusicName}");
        public override bool IsSceneEffectActive(Player player)
        {
            return /*WorldSavingSystem.EternityMode && */Config && Active(player);
        }
        public abstract bool Active(Player player);
    }
    #region Bosses
    class LifelightP2 : MusicEffect
    {
        public override bool Config => MusicConfig.Instance.Lieflight;
        public override string MusicName => "Father";
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestSoulsBoss("LifeChallenger");
            if (npc != null && npc.active && npc.life < npc.lifeMax / 2)
            {
                return true;
            }
            return false;
        }

    }
    class LifelightP1 : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override string MusicName => "Heir";
        public override bool Config => MusicConfig.Instance.Lieflight;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestSoulsBoss("LifeChallenger");
            if (npc != null && npc.active && npc.life >= npc.lifeMax / 2)
            {
                return true;
            }
            return false;
        }
    }

    class SkeletronDecadence : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => "DecadeDance";
        public override bool Config => MusicConfig.Instance.Skeletron;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.SkeletronHead);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }

    class DungeonFahk : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => "Fahkeet";
        public override bool Config => MusicConfig.Instance.DungeonGuardian;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.DungeonGuardian);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class Cultist : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => "titan_spawn";
        public override bool Config => MusicConfig.Instance.Cultist;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.CultistBoss);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class CoffinCrash : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => "SandCastleCrashing";
        public override bool Config => MusicConfig.Instance.CursedCoffin;
        public override bool Active(Player player)
        {
            if (MusicUtils.Souls == null || MusicUtils.Souls.Version < Version.Parse("1.7"))
                return false;
            NPC npc = MusicUtils.FindClosestSoulsBoss("CursedCoffin");
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class MoonManMithrix : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override string MusicName => MusicConfig.Instance.MoonLord switch
        {
            "Bigger Guitar" => "BiggerGuitar",
            "GUARDIAN" => "titan_battle",
            _ => "",
        };
        public override bool Config => MusicConfig.Instance.MoonLord != "Default";
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.MoonLordCore);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class DarkeaterBetsy : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => "Midir";
        public override bool Config => MusicConfig.Instance.Betsy;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.DD2Betsy);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class SansUndergolem : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => "CastleVein2";
        public override bool Config => MusicConfig.Instance.Golem;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.Golem);
            if (npc != null)
            {
                return true;
            }
            return false;

        }
    }
    class TenebreRossoEridanus : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override string MusicName => "TenebreRossoSangue";
        public override bool Config => MusicConfig.Instance.Eridanus;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestSoulsBoss("CosmosChampion");
            if (npc != null)
            {
                return true;
            }
            return false;

        }
    }
    class AbomPrime : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override string MusicName => MusicConfig.Instance.Abom switch
        {
            "WAR" => "WAR",
            "Stigma" => "Stigma",
            _ => "",
        };
        public override bool Config => MusicConfig.Instance.Abom != "Default";
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestSoulsBoss("AbomBoss");
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class MutantPrime : MusicEffect
    {
        public override SceneEffectPriority Priority => (SceneEffectPriority)9;
        public override string MusicName => MusicConfig.Instance.Mutant switch
        {
            "ORDER" => "ORDER",
            "Roller Mobster" => RollerMobster(),
            _ => "",
        };
        public static bool hasEnteredSecondPhase = false;
        public static bool hasEnteredThirdPhase = false;

        public static string RollerMobster()
        {
            NPC npc = MusicUtils.FindClosestSoulsBoss("MutantBoss");
            if (npc != null)
            {
                if (!hasEnteredSecondPhase && npc.GetLifePercent() <= (2f / 3))
                    hasEnteredSecondPhase = true;

                if (!hasEnteredThirdPhase && npc.life <= 1)
                    hasEnteredThirdPhase = true;

                if (hasEnteredThirdPhase)
                    return "RollerMobsterP3";
                if (hasEnteredSecondPhase)
                    return "RollerMobsterP2";

                return "RollerMobsterPA";
                
            }
            return "";
        }
        public override bool Config => MusicConfig.Instance.Mutant != "Default";
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestSoulsBoss("MutantBoss");
            if (npc == null || player.dead || !npc.active)
            {
                hasEnteredSecondPhase = false;
                hasEnteredThirdPhase = false;
            }
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class DevianttResurrection : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override string MusicName => "ResurrectionsFast";
        public override bool Config => MusicConfig.Instance.Deviantt;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestSoulsBoss("DeviBoss");
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class FleshBrain : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => "CHAOS";
        public override bool Config => MusicConfig.Instance.BrainOfCthulhu;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.BrainofCthulhu);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class FleshEaternopticon : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => "PANDEMONIUM";
        public override bool Config => MusicConfig.Instance.EaterOfWorlds;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.EaterofWorldsHead);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class Bee22Knight : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => "HiveKnight";
        public override bool Config => MusicConfig.Instance.QueenBee;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.QueenBee);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class WallOfFlowey : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => "YourBestNightmare";
        public override bool Config => MusicConfig.Instance.WallOfFlesh;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.WallofFlesh);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class RiskofKingSlime : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => "Precipitation";
        public override bool Config => MusicConfig.Instance.KingSlime;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.KingSlime);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class ChaosCthulhu : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => "ChaosKing";
        public override bool Config => MusicConfig.Instance.EyeOfCthulhu;
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.EyeofCthulhu);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class EoLGabriel : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => MusicConfig.Instance.EmpressOfLight switch
        {
            "A Mother's Love" => Ceroba(),
            "Death of God's Will" => "DeathOfGodsWill",
            "Border of Life" => "BorderOfLife",
            _ => "",
        };
        public static string Ceroba()
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.HallowBoss);
            if (npc != null)
            {
                return npc.GetLifePercent() < 0.5f ? "Ceroba2" : "Ceroba1";
            }
            return "";
        }
        public override bool Config => MusicConfig.Instance.EmpressOfLight != "Default";
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.HallowBoss);
            if (npc != null)
            {
                return true;
            }
            return false;
        }
    }
    class Destroyer : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override string MusicName => MusicConfig.Instance.Destroyer switch
        {
            "Sync With Mech Bosses" => MechSun.WarWithoutReason(),
            "The Lost Dedicated" => "TheLostDedicated",
            "Turf" => "Turf",
            _ => "",
        };
        public override bool Config => MusicConfig.Instance.Destroyer != "Sync With Mech Bosses";
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.TheDestroyer);
            if (MusicConfig.Instance.MechBosses == "Default")
            {
                return MusicUtils.FindClosestBoss(NPCID.TheDestroyer) != null;
            }

            else if (MusicConfig.Instance.MechBosses == "War Without Reason")
            {
                return MusicUtils.FindClosestBoss(NPCID.TheDestroyer) != null;
            }

            else if (MusicConfig.Instance.MechBosses == "Red Sun")
            {
                return MusicUtils.FindClosestBoss(NPCID.TheDestroyer) != null;
            }

            else if (MusicConfig.Instance.MechBosses == "Red Sun (Instrumental)")
            {
                return MusicUtils.FindClosestBoss(NPCID.TheDestroyer) != null;
            }
            else if (MusicConfig.Instance.MechBosses == "Technoir")
            {
                return MusicUtils.FindClosestBoss(NPCID.TheDestroyer) != null;
            }
            return false;
        }
    }

    class Twins : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override string MusicName => MusicConfig.Instance.Twins switch
        {
            "Sync With Mech Bosses" => MechSun.WarWithoutReason(),
            "Sexualizer" => "Sexualizer",
            _ => "",
        };
        public override bool Config => MusicConfig.Instance.Twins != "Sync With Mech Bosses";
        public override bool Active(Player player)
        {
            if (MusicConfig.Instance.MechBosses == "Default")
            {
                return MusicUtils.FindClosestBoss(NPCID.Retinazer) != null || MusicUtils.FindClosestBoss(NPCID.Spazmatism) != null;
            }

            else if (MusicConfig.Instance.MechBosses == "War Without Reason")
            {
                return MusicUtils.FindClosestBoss(NPCID.Retinazer) != null || MusicUtils.FindClosestBoss(NPCID.Spazmatism) != null;
            }

            else if (MusicConfig.Instance.MechBosses == "Red Sun")
            {
                return MusicUtils.FindClosestBoss(NPCID.Retinazer) != null || MusicUtils.FindClosestBoss(NPCID.Spazmatism) != null;
            }

            else if (MusicConfig.Instance.MechBosses == "Red Sun (Instrumental)")
            {
                return MusicUtils.FindClosestBoss(NPCID.Retinazer) != null || MusicUtils.FindClosestBoss(NPCID.Spazmatism) != null;
            }
            else if (MusicConfig.Instance.MechBosses == "Technoir")
            {
                return MusicUtils.FindClosestBoss(NPCID.Retinazer) != null || MusicUtils.FindClosestBoss(NPCID.Spazmatism) != null;
            }
            return false;
        }
    }

    class SPrime: MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override string MusicName => MusicConfig.Instance.SkeletronPrime switch
        {
            "Sync With Mech Bosses" => MechSun.WarWithoutReason(),
            "Pursuit" => "Pursuit",
            _ => "",
        };
        public override bool Config => MusicConfig.Instance.SkeletronPrime != "Sync With Mech Bosses";
        public override bool Active(Player player)
        {
            if (MusicConfig.Instance.MechBosses == "Default")
            {
                return MusicUtils.FindClosestBoss(NPCID.SkeletronPrime) != null;
            }

            else if (MusicConfig.Instance.MechBosses == "War Without Reason")
            {
                return MusicUtils.FindClosestBoss(NPCID.SkeletronPrime) != null;
            }

            else if (MusicConfig.Instance.MechBosses == "Red Sun")
            {
                return MusicUtils.FindClosestBoss(NPCID.SkeletronPrime) != null;
            }

            else if (MusicConfig.Instance.MechBosses == "Red Sun (Instrumental)")
            {
                return MusicUtils.FindClosestBoss(NPCID.SkeletronPrime) != null;
            }
            else if (MusicConfig.Instance.MechBosses == "Technoir")
            {
                return MusicUtils.FindClosestBoss(NPCID.SkeletronPrime) != null;
            }
            return false;
        }
    }

    class MechSun : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => MusicConfig.Instance.MechBosses switch
        {
            "War Without Reason" => WarWithoutReason(),
            "Red Sun" => "RedSunVocal",
            "Red Sun (Instrumental)" => "RedSunInstrumental",
            "Technoir" => "Technoir",
            _ => "",
        };

        public static List<int> MechIDs = new()
        {
            NPCID.SkeletronPrime,
            NPCID.TheDestroyer,
            NPCID.Retinazer,
            NPCID.Spazmatism
        };
        public static string WarWithoutReason()
        {
            int mechs = NPC.downedMechBoss1 ? 1 : 0;
            mechs += NPC.downedMechBoss2 ? 1 : 0;
            mechs += NPC.downedMechBoss3 ? 1 : 0;

            static string ABSectionPerPhase(string p1, string p2)
            {
                if (Main.npc.Any(n => n != null && n.active && MechIDs.Contains(n.type) && n.life < n.lifeMax / 2))
                    return p2;
                return p1;
            }
            if (mechs <= 0)
                return ABSectionPerPhase("WarWithoutReasonA1", "WarWithoutReasonA2");
            else if (mechs <= 1)
                return ABSectionPerPhase("WarWithoutReasonB1", "WarWithoutReasonB2");
            else
                return "WarWithoutReasonC";
        }
        public override bool Config => MusicConfig.Instance.MechBosses != "Default";
        public override bool Active(Player player)
        {
            bool anyMech = Main.npc.Any(n => n != null && n.active && MechIDs.Contains(n.type));
            if (anyMech)
            {
                return true;
            }
            return false;
        }
    }
    class PlanteraP1 : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => MusicConfig.Instance.Plantera switch
        {
            "God of the Dead" => "GodOfTheDeadP1",
            "AFTERLIFE" => "SpecimenMechanical",
            "Cowboys From Hell" => "CowboysFromHell",
            _ => "",
        };
        public override bool Config => MusicConfig.Instance.Plantera != "Default";

        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.Plantera);
            if (npc != null && npc.life > npc.lifeMax / 2)
            {
                return true;
            }
            return false;
        }
    }
    class PlanteraP2 : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => MusicConfig.Instance.Plantera switch
        {
            "God of the Dead" => Hades(),
            "AFTERLIFE" => "AFTERLIFE",
            "Cowboys From Hell" => "CowboysFromHell",
            _ => "",
        };
        public static string Hades()
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.Plantera);
            return npc != null && npc.life <= npc.lifeMax / 4 ? "UnseenOnes" : "GodOfTheDeadP2";
        }
        public override bool Config => MusicConfig.Instance.Plantera != "Default";

        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.Plantera);
            if (npc != null && npc.life <= npc.lifeMax / 2)
            {
                return true;
            }
            return false;
        }
    }
    class BaronP1 : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => "DeathOdysseyAftermath";
        public override bool Config => MusicConfig.Instance.Baron;

        public override bool Active(Player player)
        {
            if (MusicUtils.Souls == null || MusicUtils.Souls.Version < Version.Parse("1.6"))
                return false;
            NPC npc = MusicUtils.FindClosestSoulsBoss("BanishedBaron");
            if (npc != null && npc.life >= npc.lifeMax * 0.66)
            {
                return true;
            }
            return false;
        }
    }
    class BaronP2 : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => "DeathOdyssey";
        public override bool Config => MusicConfig.Instance.Baron;

        public override bool Active(Player player)
        {
            if (MusicUtils.Souls == null || MusicUtils.Souls.Version < Version.Parse("1.6"))
                return false;
            int index = MusicUtils.Souls.Find<ModNPC>("BanishedBaron").Type;
            NPC npc = index >= 0 && index < Main.maxNPCs ? Main.npc[index] : null;
            if (npc != null && npc.life < npc.lifeMax * 0.66)
            {
                return true;
            }
            return false;
        }
    }
    class DeepFishron : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override string MusicName => MusicConfig.Instance.DukeFishron switch
            {
            "Deep Blue Combat" => "DeepBlueCombat",
            "Vengeance" => "Vengeance",
            _ => "",
        };
        public override bool Config => MusicConfig.Instance.DukeFishron != "Default";
        public override bool Active(Player player)
        {
            NPC npc = MusicUtils.FindClosestBoss(NPCID.DukeFishron);
            if (npc != null && npc.active)
            {
                return true;
            }
            return false;
        }
    }
    #endregion
    class MyCastleTown : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override string MusicName => "MyCastleTown";
        public override bool Config => MusicConfig.Instance.Town;
        public override bool Active(Player player)
        {
            return player.townNPCs > 2 || VanillaMusic.Current == MusicID.TownDay || VanillaMusic.Current == MusicID.TownNight;
        }
    }
    class Forest : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
        public override string MusicName => "ScarletForest";
        public override bool Config => MusicConfig.Instance.Forest;
        public override bool Active(Player player)
        {
            return player.ZoneForest || VanillaMusic.Current == MusicID.OverworldDay || VanillaMusic.Current == MusicID.AltOverworldDay || VanillaMusic.Current == MusicID.WindyDay;
        }
    }
    class Jungle : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
        public override string MusicName => "Greenpath";
        public override bool Config => MusicConfig.Instance.Jungle;
        public override bool Active(Player player)
        {
            if ((player.ZoneJungle && player.ZoneShallow()) || VanillaMusic.Current == MusicID.Jungle || VanillaMusic.Current == MusicID.JungleNight)
            {
                return true;
            }
            return false;
        }
    }
    class UndergroundJungle : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
        public override string MusicName => "QueensGardens";
        public override bool Config => MusicConfig.Instance.UndergroundJungle;
        public override bool Active(Player player)
        {
            if ((player.ZoneRockLayerHeight && player.ZoneJungle) || VanillaMusic.Current == MusicID.JungleUnderground)
            {
                return true;
            }
            return false;
        }
    }
    class Crimson : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string MusicName => "Guts";
        public override bool Config => MusicConfig.Instance.Crimson;
        public override bool Active(Player player)
        {
            if ((player.ZoneCrimson && player.ZoneOverworldHeight) || VanillaMusic.Current == MusicID.Crimson)
            {
                return true;
            }
            return false;
        }
    }
    class Corruption : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string MusicName => "TheWorldLooksWhite";
        public override bool Config => MusicConfig.Instance.Corruption;
        public override bool Active(Player player)
        {
            if ((player.ZoneCorrupt && player.ZoneOverworldHeight) || VanillaMusic.Current == MusicID.Corruption)
            {
                return true;
            }
            return false;
        }
    }
    class Space : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override string MusicName => "QuietAndFalling";
        public override bool Config => MusicConfig.Instance.Space;
        public override bool Active(Player player)
        {
            if (player.ZoneNormalSpace || VanillaMusic.Current == MusicID.Space || VanillaMusic.Current == MusicID.SpaceDay)
            {
                return true;
            }
            return false;
        }
    }
    class Desert : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
        public override string MusicName => "DancerInTheDarkness";
        public override bool Config => MusicConfig.Instance.Desert;
        public override bool Active(Player player)
        {
            if ((player.ZoneDesert && player.ZoneOverworldHeight) || VanillaMusic.Current == MusicID.Desert)
            {
                return true;
            }
            return false;
        }
    }
    class Tundra : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
        public override string MusicName => "FirstSteps";
        public override bool Config => MusicConfig.Instance.Tundra;
        public override bool Active(Player player)
        {
            if ((player.ZoneSnow && player.ZoneShallow()) || VanillaMusic.Current == MusicID.Snow)
            {
                return true;
            }
            return false;
        }
    }

    class Hallow : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
        public override string MusicName => "JoyOfRemembrance";
        public override bool Config => MusicConfig.Instance.Hallow;
        public override bool Active(Player player)
        {
            if ((player.ZoneHallow && player.ZoneShallow()) || VanillaMusic.Current == MusicID.TheHallow)
            {
                return true;
            }
            return false;
        }
    }
    class Ocean : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
        public override string MusicName => "DeepBlueCalm";
        public override bool Config => MusicConfig.Instance.Ocean;
        public override bool Active(Player player)
        {
            if (player.ZoneBeach || VanillaMusic.Current == MusicID.Ocean)
            {
                return true;
            }
            return false;
        }
    }

    class Rain : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override string MusicName => "Cyclogenesis";
        public override bool Config => MusicConfig.Instance.Rain;
        public override bool Active(Player player)
        {
            if (VanillaMusic.Current == MusicID.Rain || VanillaMusic.Current == MusicID.Monsoon)
            {
                return true;
            }
            return false;
        }
    }
    class Sandstorm : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override string MusicName => "SandsOfTideCombat";
        public override bool Config => MusicConfig.Instance.Sandstorm;
        public override bool Active(Player player)
        {
            if (VanillaMusic.Current == MusicID.Sandstorm)
            {
                return true;
            }
            return false;
        }
    }

    class Pillars1 : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => MusicConfig.Instance.LunarPillars switch
        {
            "Coalescence/con lentitud poderosa" => "Coalescence",
            "Crumbling Tower" => "titan_tower",
            _ => ""
        };
        public override bool Config => MusicConfig.Instance.LunarPillars != "Default";
        public override bool Active(Player player)
        {
            int pillars = MusicUtils.CountPillars();
            if (pillars > 2)
            {
                return true;
            }
            return false;
        }
    }
    class Pillars2 : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;
        public override string MusicName => MusicConfig.Instance.LunarPillars switch
        {
            "Coalescence/con lentitud poderosa" => "Coalescence",
            "Crumbling Tower" => "titan_tower",
            _ => ""
        };
        public override bool Config => MusicConfig.Instance.LunarPillars != "Default";
        public override bool Active(Player player)
        {
            int pillars = MusicUtils.CountPillars();
            if (pillars > 0 && pillars <= 2)
            {
                return true;
            }
            return false;
        }
    }
    class UndergroundHallow : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
        public override string MusicName => "PurpleRain";
        public override bool Config => MusicConfig.Instance.UndergroundHallow;
        public override bool Active(Player player)
        {
            if ((player.ZoneHallow && player.ZoneRockLayerHeight) || VanillaMusic.Current == MusicID.UndergroundHallow)
            {
                return true;
            }
            return false;
        }
    }
    class Mushroom : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string MusicName => "TheyMightAswellBeDead";
        public override bool Config => MusicConfig.Instance.Mushroom;
        public override bool Active(Player player)
        {
            if (player.ZoneGlowshroom || VanillaMusic.Current == MusicID.Mushrooms)
            {
                return true;
            }
            return false;
        }
    }
    class Dungeon : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string MusicName => "MirrorTempleB";
        public override bool Config => MusicConfig.Instance.Dungeon;
        public override bool Active(Player player)
        {
            if (player.ZoneDungeon || VanillaMusic.Current == MusicID.Dungeon)
            {
                return true;
            }
            return false;
        }
    }
    class Temple : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string MusicName => "CastleVein";
        public override bool Config => MusicConfig.Instance.Temple;
        public override bool Active(Player player)
        {
            if (player.ZoneLihzhardTemple || VanillaMusic.Current == MusicID.Temple)
            {
                return true;
            }
            return false;
        }
    }
    /*
    class PirateInvasion : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
        public override string MusicName => "DeathOdyssey";
        public override bool Config => MusicConfig.Instance.PirateInvasion;
        public override bool Active(Player player)
        {
            if (Main.invasionType == InvasionID.PirateInvasion || VanillaMusic.Current == MusicID.PirateInvasion)
            {
                return true;
            }
            return false;
        }
    }
    */
    class Underground : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
        public override string MusicName => "TerraPluviam";
        public override bool Config => MusicConfig.Instance.Underground;
        public override bool Active(Player player)
        {
            return ((player.ZoneNormalUnderground || player.ZoneNormalCaverns) || VanillaMusic.Current == MusicID.Underground);
        }
    }

    class UndergroundCrimson : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string MusicName => "Glory";
        public override bool Config => MusicConfig.Instance.UndergroundCrimson;
        public override bool Active(Player player)
        {
            return (Main.hardMode && player.ZoneCrimson && player.ZoneUnderground()) || VanillaMusic.Current == MusicID.UndergroundCrimson;
        }
    }
    class UndergroundCorruption : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string MusicName => "TheWorldLooksRedCalm";
        public override bool Config => MusicConfig.Instance.UndergroundCrimson;
        public override bool Active(Player player)
        {
            return (Main.hardMode && player.ZoneCorrupt && player.ZoneUnderground()) || VanillaMusic.Current == MusicID.UndergroundCorruption;
        }
    }
    class UndergroundDesert : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string MusicName => "SandsOfTideCalm";
        public override bool Config => MusicConfig.Instance.UndergroundDesert;
        public override bool Active(Player player)
        {
            return player.ZoneUndergroundDesert || VanillaMusic.Current == MusicID.UndergroundDesert;
        }
    }
    class UndergroundTundra : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
        public override string MusicName => "ScatteredAndLostCalm";
        public override bool Config => MusicConfig.Instance.UndergroundTundra;
        public override bool Active(Player player)
        {
            return (player.ZoneRockLayerHeight && player.ZoneSnow) || VanillaMusic.Current == MusicID.Snow;
        }
    }
    class ULTRAKILL : MusicEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override string MusicName => "AltarsOfApostasyCalm";
        public override bool Config => MusicConfig.Instance.Underworld;
        public override bool Active(Player player)
        {
            return player.ZoneUnderworldHeight || VanillaMusic.Current == MusicID.Hell;
        }
    }
}