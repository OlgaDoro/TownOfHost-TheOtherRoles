using System.Linq;
using System.Collections.Generic;
using HarmonyLib;
using InnerNet;
using UnhollowerBaseLib.Runtime.VersionSpecific.FieldInfo;

namespace TownOfHost
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameJoined))]
    class OnGameJoinedPatch
    {
        public static void Postfix(AmongUsClient __instance)
        {
            Logger.Info($"{__instance.GameId}に参加", "OnGameJoined");
            Main.playerVersion = new Dictionary<byte, PlayerVersion>();
            Main.devNames = new Dictionary<byte, string>();
            RPC.RpcVersionCheck();

            NameColorManager.Begin();
            Options.Load();
            if (AmongUsClient.Instance.AmHost) //以下、ホストのみ実行
            {
                if (PlayerControl.GameOptions.killCooldown == 0.1f)
                    PlayerControl.GameOptions.killCooldown = Main.LastKillCooldown.Value;
                new LateTask(() =>
                {
                    string rname = PlayerControl.LocalPlayer.Data.PlayerName;
                    string fontSize = "1.5";
                    string dev = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.TheGlitch), "Dev")}</size>";
                    string name = dev + "\r\n" + rname;
                    if (PlayerControl.LocalPlayer.Data.FriendCode is "nullrelish#9615" or "tillhoppy#6167" or "gnuedaphic#7196" or "pingrating#9371")
                    {
                        PlayerControl.LocalPlayer.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.TheGlitch), name)}");
                        Main.devNames.Add(PlayerControl.LocalPlayer.Data.PlayerId, rname);
                    }
                }, 3f, "Name Check for Host");
            }
        }
    }
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnPlayerJoined))]
    class OnPlayerJoinedPatch
    {
        public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)] ClientData client)
        {
            Logger.Info($"{client.PlayerName}(ClientID:{client.Id}) (FreindCode:{client.FriendCode}) joined the game.", "Session");
            if (DestroyableSingleton<FriendsListManager>.Instance.IsPlayerBlockedUsername(client.FriendCode) && AmongUsClient.Instance.AmHost)
            {
                AmongUsClient.Instance.KickPlayer(client.Id, true);
                Logger.Info($"This is a blocked player. {client?.PlayerName}({client.FriendCode}) was banned.", "BAN");
            }
            Main.playerVersion = new Dictionary<byte, PlayerVersion>();
            RPC.RpcVersionCheck();
            if (AmongUsClient.Instance.AmHost)
            {
                new LateTask(() =>
                {
                    if (client.Character != null) ChatCommands.SendTemplate("welcome", client.Character.PlayerId, true);
                    string rname = client.Character.Data.PlayerName;
                    if (client.Character != null) //TITLE FOR EVERYONE  //TITLE FOR EVERYONE
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title

                        string dsctit = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug6), "/t discord")}</size>"; ;
                        string dscjoin = dsctit + "\r\n" + rname;
                        client.Character.RpcSetName($"<size={fontSize}>{dscjoin}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "nullrelish#9615" or "vastblaze#8009" or "ironbling#3600" or "tillhoppy#6167" or "gnuedaphic#7196" or "pingrating#9371")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title

                        //DEVELOPER TAG
                        string dev = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.TheGlitch), "Developer")}</size>";
                        string name = dev + "\r\n" + rname; //DEVS

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.nd2), name)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "mossmodel#2348")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts
                        string fontSize5 = "1";//name hearts

                        //AUGUST TITLE
                        string aug1 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug1), "♡")}</size>";
                        string augc = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug2), "C")}</size>";
                        string augu = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug3), "u")}</size>";
                        string augt = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug4), "t")}</size>";
                        string augi = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug5), "i")}</size>";
                        string auge = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug6), "e")}</size>";
                        string aug2 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug7), "♡")}</size>";
                        //AUGUST NAME
                        string a1 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug7), "♡")}</size>";
                        string a2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug7), "A")}</size>";
                        string a3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug6), "u")}</size>";
                        string a4 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug5), "g")}</size>";
                        string a5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug4), "u")}</size>";
                        string a6 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug3), "s")}</size>";
                        string a7 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug2), "t")}</size>";
                        string a8 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug2), "♡")}</size>";


                        string nameaugust = aug1 + augc + augu + augt + augi + auge + aug2 + "\r\n" + a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8;

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.nd2), nameaugust)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "envy3kindly#7034")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts
                        string fontSize5 = "1"; //name hearts

                        //ROSE  TITLE START
                        string sns1 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns1), "♡")}</size>";
                        string sns2 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns2), "H")}</size>";
                        string sns3 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns3), "o")}</size>";
                        string sns4 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns4), "t")}</size>";
                        string sns5 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns5), "t")}</size>";
                        string sns6 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns6), "i")}</size>";
                        string sns7 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns7), "e")}</size>";
                        string sns8 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns8), "♡")}</size>";
                        //ROSE NAME START
                        string sns91 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns1), "♡")}</size>";
                        string sns9 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns1), "shi")}</size>";
                        string sns0 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns3), "ft")}</size>";
                        string sns01 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns5), "yr")}</size>";
                        string sns02 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns6), "os")}</size>";
                        string sns03 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns6), "e")}</size>";
                        string sns92 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.sns7), "♡")}</size>";

                        string snsname = sns1 + sns2 + sns3 + sns4 + sns5 + sns6 + sns7 + sns8 + "\r\n" + sns91 + sns9 + sns0 + sns01 + sns02 + sns03 + sns92; //ROSE NAME & TITLE

                        client.Character.RpcSetColor(13);
                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.rosecolor), snsname)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "sunlitmoon#2472") //AVIATOR LILY
                    {
                        string fontSize4 = "0.75"; // nothing

                        string lily = $"<size={fontSize4}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.Amnesiac), "Aviator")}</size>";
                        string nameb = lily + "\r\n" + rname; //LILY TITLE
                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.rosecolor), nameb)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "testfly#6512") //pushin p
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts
                        string fontSize5 = "1"; //name hearts

                        //PUSHIN P TITLE START
                        string p1 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh1), "♡")}</size>";
                        string p2 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh2), "S")}</size>";
                        string p3 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh3), "w")}</size>";
                        string p4 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh4), "e")}</size>";
                        string p5 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh5), "e")}</size>";
                        string p6 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh6), "t")}</size>";
                        string p7 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh7), "i")}</size>";
                        string p8 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh8), "e")}</size>";
                        string p9 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh9), "♡")}</size>";
                        //PUSHIN P NAME START
                        string ps1 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh1), "♡")}</size>";
                        string ps2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh1), "p")}</size>";
                        string ps3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh3), "us")}</size>";
                        string ps4 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh5), "hi")}</size>";
                        string ps5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh7), "nP")}</size>";
                        string ps6 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh9), "♡")}</size>";

                        string pushinp = p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9 + "\r\n" + ps1 + ps2 + ps3 + ps4 + ps5 + ps6; //PUSHINP NAME & TITLE

                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.Demolitionist), pushinp)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }

                    if (client.FriendCode is "riskylatte#0409" or "shotnote#2620" or "furrycoin#0508")
                    {
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts

                        //SILENCE TITLE
                        string slnc = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.Amnesiac), "Icyyy")}</size>";
                        string snf = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.Amnesiac), "米")}</size>";
                        //SILENCE NAME
                        string namesilence = slnc + snf + "\r\n" + rname;

                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.rosecolor), namesilence)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "waterpupal#6193")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts
                        string fontSize5 = "1"; //name hearts

                        //FEEFEE TITLE START
                        string fee1 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh9), "♡")}</size>";
                        string fee2 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh8), "Kr")}</size>";
                        string fee3 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh7), "am")}</size>";
                        string fee4 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh6), "p'")}</size>";
                        string fee5 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh5), "s B")}</size>";
                        string fee6 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh4), "es")}</size>";
                        string fee7 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh3), "ti")}</size>";
                        string fee8 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh2), "e")}</size>";
                        string fee9 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh1), "♡")}</size>";
                        //FEEFEE NAME START
                        string fe1 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug1), "♡")}</size>";
                        string fe2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug2), "F")}</size>";
                        string fe3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug2), "e")}</size>";
                        string fe4 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug3), "e")}</size>";
                        string fe5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug3), "F")}</size>";
                        string fe6 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug4), "e")}</size>";
                        string fe7 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug5), "e")}</size>";
                        string fe8 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug6), "♡")}</size>";

                        string feename = fee1 + fee2 + fee3 + fee4 + fee5 + fee6 + fee7 + fee8 + fee9 + "\r\n" + fe1 + fe2 + fe3 + fe4 + fe5 + fe6 + fe7 + fe8; //FEEFEE NAME & TITLE

                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.rosecolor), feename)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "bankpurple#9983") //LEXI NAME
                    {
                        string fontSize = "1.5";
                        string fontSize1 = "1";
                        string fontSize2 = "0.8";
                        string fontSize3 = "0.5";
                        //LEXI TITLE START
                        string lxs0 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs1), "♡")}</size>";
                        string lxs1 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs1), "s")}</size>";
                        string lxs2 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs2), "p")}</size>";
                        string lxs3 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs3), "i")}</size>";
                        string lxs4 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs4), "c")}</size>";
                        string lxs5 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs5), "y")}</size>";
                        string lxs6 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs6), " b")}</size>";
                        string lxs7 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs7), "e")}</size>";
                        string lxs8 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs7), "a")}</size>";
                        string lxs9 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs8), "n")}</size>";
                        string lxs10 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs9), "♡")}</size>";
                        //LEXI NAME START
                        string lx0 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs9), "♡")}</size>";
                        string lx1 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs8), "L")}</size>";
                        string lx2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs7), "e")}</size>";
                        string lx3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs6), "x")}</size>";
                        string lx4 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs5), "♦")}</size>";
                        string lx5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs5), "S")}</size>";
                        string lx6 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs4), "t")}</size>";
                        string lx7 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs3), "a")}</size>";
                        string lx8 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs2), "c")}</size>";
                        string lx9 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs1), "i")}</size>";
                        string lx10 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.lxs0), "♡")}</size>";

                        string lxname = lxs0 + lxs1 + lxs2 + lxs3 + lxs4 + lxs5 + lxs6 + lxs7 + lxs8 + lxs9 + lxs10 + "\r\n" + lx0 + lx1 + lx2 + lx3 + lx4 + lx5 + lx6 + lx7 + lx8 + lx9 + lx10;

                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss8), lxname)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "pupfrizzy#0420") //NURSE CHIA NAME
                    {
                        string fontSize = "1.5";
                        string fontSize1 = "1";
                        string fontSize2 = "0.8";
                        string fontSize3 = "0.5";
                        string fontSize4 = "1.2";
                        //NURSE CHIA TITLE START
                        string ncs0 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs0), "♡")}</size>";
                        string ncs1 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs0), "B")}</size>";
                        string ncs2 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs1), "ã")}</size>";
                        string ncs3 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs2), "d")}</size>";
                        string ncs4 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs3), "ä")}</size>";
                        string ncs5 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs4), "ss")}</size>";
                        string ncs6 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs5), " ß")}</size>";
                        string ncss = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs6), "i")}</size>";
                        string ncs7 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs7), "t")}</size>";
                        string ncs8 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs8), "c")}</size>";
                        string ncs9 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs9), "h")}</size>";
                        string ncs10 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs9), "♡")}</size>";
                        //NURSE CHIA NAME START
                        string nc0 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs9), "♡")}</size>";
                        string nc1 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs9), "N")}</size>";
                        string nc2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs8), "u")}</size>";
                        string nc3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs7), "r")}</size>";
                        string nc4 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs6), "s")}</size>";
                        string nc5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs5), "e")}</size>";
                        string ncx = $"<size={fontSize4}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.Impostor), "乂")}</size>";
                        string nc6 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs4), "C")}</size>";
                        string nc7 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs3), "h")}</size>";
                        string nc8 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs2), "i")}</size>";
                        string nc9 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs1), "a")}</size>";
                        string nc10 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ncs0), "♡")}</size>";

                        string ncname = ncs0 + ncs1 + ncs2 + ncs3 + ncs4 + ncs5 + ncs6 + ncss + ncs7 + ncs8 + ncs9 + ncs10 + "\n" + nc0 + nc1 + nc2 + nc3 + nc4 + nc5 + ncx + nc6 + nc7 + nc8 + nc9 + nc10;

                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss8), ncname)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "epicflower#1116") //JESSI O
                    {
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts

                        //JESSI TITLE START
                        string js0 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss1), "♡")}</size>";
                        string js1 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss1), "R")}</size>";
                        string js2 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss2), "e")}</size>";
                        string js3 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss3), "d")}</size>";
                        string js4 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss4), " ")}</size>";
                        string js5 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss5), "R")}</size>";
                        string js6 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss6), "i")}</size>";
                        string js7 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss7), "s")}</size>";
                        string js8 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss7), "i")}</size>";
                        string js9 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss8), "n")}</size>";
                        string js10 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss9), "g")}</size>";
                        string js11 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss9), "♡")}</size>";

                        string jssname = js0 + js1 + js2 + js3 + js4 + js5 + js6 + js7 + js8 + js9 + js10 + js11 + "\r\n" + rname;

                        client.Character.RpcSetColor(14); //banana color on join
                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss8), jssname)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "supbay#9710") //mitski title
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts
                        string fontSize5 = "1"; //name hearts

                        //MITSKI TITLE START
                        string cn0 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd9), "♡")}</size>";
                        string cn1 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd9), "A")}</size>";
                        string cn2 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd8), "l")}</size>";
                        string cn3 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd7), "w")}</size>";
                        string cn4 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd6), "a")}</size>";
                        string cn5 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd5), "y")}</size>";
                        string cn6 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd4), "s")}</size>";
                        string cn7 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd4), " ")}</size>";
                        string cn8 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd3), "S")}</size>";
                        string cn9 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd2), "u")}</size>";
                        string cn10 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd1), "S")}</size>";
                        string cn11 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd1), "♡")}</size>";
                        //MITSKI NAME START
                        string cnd1 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd1), "♡")}</size>";
                        string cnd2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd1), "m")}</size>";
                        string cnd3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd3), "i")}</size>";
                        string cnd4 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd3), "t")}</size>";
                        string cnd5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd5), "s")}</size>";
                        string cnd6 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd7), "k")}</size>";
                        string cnd7 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd7), "i")}</size>";
                        string cnd8 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.cnd9), "♡")}</size>";

                        string cndname = cn0 + cn1 + cn2 + cn3 + cn4 + cn5 + cn6 + cn7 + cn8 + cn9 + cn10 + cn11 + "\r\n" + cnd1 + cnd2 + cnd3 + cnd4 + cnd5 + cnd6 + cnd7 + cnd8;

                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.rosecolor), cndname)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "luckyplus#8283") //candy
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts
                        string fontSize5 = "1"; //name hearts

                        //CANDY TITLE START
                        string kr0 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh1), "♡")}</size>";
                        string kr1 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh1), "T")}</size>";
                        string kr2 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh2), "r")}</size>";
                        string kr3 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh3), "u")}</size>";
                        string kr4 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh4), "s")}</size>";
                        string kr5 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh5), "t")}</size>";
                        string kr6 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh6), " ")}</size>";
                        string kr7 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh7), "H")}</size>";
                        string kr8 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh7), "o")}</size>";
                        string kr9 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh8), "s")}</size>";
                        string kr10 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh9), "t")}</size>";
                        string kr11 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh9), "♡")}</size>";
                        //CANDY NAME START
                        string krz1 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh1), "♡")}</size>";
                        string krz2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh1), "c")}</size>";
                        string krz3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh3), "a")}</size>";
                        string krz4 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh5), "n")}</size>";
                        string krz5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh7), "d")}</size>";
                        string krz6 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh7), "y")}</size>";
                        string krz7 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.psh9), "♡")}</size>";

                        string krzname = kr1 + kr2 + kr3 + kr4 + kr5 + kr6 + kr7 + kr8 + kr9 + kr10 + kr11 + "\r\n" + krz1 + krz2 + krz3 + krz4 + krz5 + krz6 + krz7;//KRZ NAME

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.rosecolor), krzname)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "waryclaw#7449")//NOODLES NAME
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts

                        string nd1 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.nd2), "♡")}</size>";
                        string nd2 = $"<size={fontSize1}1>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.nd1), "Virgin")}</size>";
                        string ndsname = nd1 + nd2 + nd1 + "\r\n" + rname;

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.nd2), ndsname)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "moonside#5200")//MY NAME
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "1"; //name hearts
                        string fontSize2 = "0.8"; //title hearts
                        string fontSize3 = "0.5"; //title hearts


                        string sph = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.yellowcolor), "♡")}</size>";//title
                        string spbe = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.yellowcolor), "Space Bean")}</size>";//title

                        string sp1 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.yellowcolor), "♡")}</size>";//name
                        string sp2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.yellowcolor), "Allie")}</size>";//name
                        string spbename = sph + spbe + sph + "\r\n" + sp1 + sp2 + sp1;
                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug7), spbename)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    //bsteele95 name
                    if (client.FriendCode is "raggedsofa#2041")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title

                        string dscfr = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor2), "Hosts Favorite Kiss")}</size>"; //GENERAL TITLE
                        string named = dscfr + "\r\n" + rname; //DISCORD FRIENDS

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug5), named)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    //Cinnamoroll name
                    if (client.FriendCode is "sphinxchic#9616")//Cinnamoroll name
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title
                        string fontSize2 = "0.5"; //title hearts

                        string cn1 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.pastelcoral), "♡")}</size>";//name
                        string cn2 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.pastelpink), "Discord Family Member")}</size>";//name
                        string cn3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.pastelblue), "cinnamoroll")}</size>"; //GENERAL TITLE
                        string named = cn1 + cn2 + cn1 + "\r\n" + cn3;

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug5), named)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "farealike#0862") //WaW name start
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title
                        string fontSize3 = "0.5"; //title hearts
                        string fontSize5 = "1"; //name hearts

                        //WaW  TITLE START
                        string waw1 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor3), "☀")}</size>";
                        string waw2 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor3), "F")}</size>";
                        string waw3 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor3), "a")}</size>";
                        string waw4 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor3), "t ")}</size>";
                        string waw5 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor3), "C")}</size>";
                        string waw6 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor3), "a")}</size>";
                        string waw7 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor3), "t")}</size>";
                        string waw8 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor3), "☀")}</size>";
                        //WaW NAME START
                        string waw91 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor), "☀")}</size>";
                        string waw01 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor2), "W")}</size>";
                        string waw02 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor3), "a")}</size>";
                        string waw03 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor2), "W")}</size>";
                        string waw92 = $"<size={fontSize5}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor), "☀")}</size>";

                        string snsname = waw1 + waw2 + waw3 + waw4 + waw5 + waw6 + waw7 + waw8 + "\r\n" + waw91 + waw01 + waw02 + waw03 + waw92;

                        client.Character.RpcSetColor(16);
                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.rosecolor), snsname)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "toypoet#5984") //YeetusSkeetus
                    {
                        string fontSize = "1.5";
                        string fontSize1 = "1";
                        string fontSize2 = "0.8";
                        string fontSize3 = "0.5";
                        //YEETUS TITLE START
                        string ysk0 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk0), "☀")}</size>";
                        string ysk1 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk1), "S")}</size>";
                        string ysk2 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk2), "k")}</size>";
                        string ysk3 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk3), "e")}</size>";
                        string ysk4 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk4), "e")}</size>";
                        string ysk5 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk5), "t")}</size>";
                        string ysk6 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk6), "u")}</size>";
                        string ysk7 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk7), "s")}</size>";
                        string ysk8 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk8), "☀")}</size>";
                        //YEETUS NAME START
                        string sk0 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk7), "☀")}</size>";
                        string sk1 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk6), "Y")}</size>";
                        string sk2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk5), "e")}</size>";
                        string sk3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk4), "e")}</size>";
                        string sk4 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk3), "t")}</size>";
                        string sk5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk2), "u")}</size>";
                        string sk6 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk1), "s")}</size>";
                        string sk7 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ysk0), "☀")}</size>";

                        string skname = ysk0 + ysk1 + ysk2 + ysk3 + ysk4 + ysk5 + ysk6 + ysk7 + ysk8 + "\r\n" + sk0 + sk1 + sk2 + sk3 + sk4 + sk5 + sk6 + sk7;

                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss8), skname)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "fancybadge#5253" or "dizzyalpha#4043") //SHY LYSOL NAME
                    {
                        string fontSize = "1.5";
                        string fontSize1 = "1";
                        string fontSize2 = "0.8"; //HELP HELP HELP
                        string fontSize3 = "0.5";
                        //LYSOL TITLE START
                        string shl0 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl1), "☀")}</size>";
                        string shl1 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl1), "S")}</size>";
                        string shl2 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl2), "l")}</size>";
                        string shl3 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl3), "e")}</size>";
                        string shl4 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl4), "e")}</size>";
                        string shl5 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl5), "p")}</size>";
                        string shl6 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl6), "y ")}</size>";
                        string shl7 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl7), "b")}</size>";
                        string shl8 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl8), "e")}</size>";
                        string shl9 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl9), "a")}</size>";
                        string shl10 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl0), "n")}</size>";
                        string shl11 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl0), "☀")}</size>";
                        //LYSOL NAME START
                        string sh0 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl9), "☀")}</size>";
                        string sh1 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl8), "S")}</size>";
                        string sh2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl7), "h")}</size>";
                        string sh3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl6), "y")}</size>";
                        string sh4 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.Hitman), "♡")}</size>";
                        string sh5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl6), "L")}</size>";
                        string sh6 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl5), "y")}</size>";
                        string sh7 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl4), "s")}</size>";
                        string sh8 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl3), "o")}</size>";
                        string sh9 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl2), "l")}</size>";
                        string sh10 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.shl1), "☀")}</size>";

                        string shname = shl0 + shl1 + shl2 + shl3 + shl4 + shl5 + shl6 + shl7 + shl8 + shl9 + shl10 + shl11 + "\r\n" + sh0 + sh1 + sh2 + sh3 + sh4 + sh5 + sh6 + sh7 + sh8 + sh9 + sh10;

                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss8), shname)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    if (client.FriendCode is "deepstag#0210") //BLOODANGELSIX
                    {
                        string fontSize = "1.5";
                        string fontSize1 = "1";
                        string fontSize2 = "0.8";
                        string fontSize3 = "0.5";
                        //BLOODANGELSIX TITLE START
                        string ban0 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban1), "☀")}</size>";
                        string ban1 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban1), "W")}</size>";
                        string ban2 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban2), "a")}</size>";
                        string ban3 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban3), "r")}</size>";
                        string ban4 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban4), "h")}</size>";
                        string ban5 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban5), "a")}</size>";
                        string ban6 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban6), "m ")}</size>";
                        string ban7 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban7), "m")}</size>";
                        string ban8 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban8), "e")}</size>";
                        string ban9 = $"<size={fontSize2}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban9), "r")}</size>";
                        string ban10 = $"<size={fontSize3}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban0), "☀")}</size>";
                        //BLOODANGELSIX NAME START
                        string ba0 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban9), "☀")}</size>";
                        string ba1 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban9), "B")}</size>";
                        string ba2 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban8), "l0")}</size>";
                        string ba3 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban7), "0d")}</size>";
                        string ba4 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban6), "A")}</size>";
                        string ba5 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban5), "n")}</size>";
                        string ba6 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban4), "ge")}</size>";
                        string ba7 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban3), "l")}</size>";
                        string ba8 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban2), "s")}</size>";
                        string ba9 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban1), "I")}</size>";
                        string ba10 = $"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban0), "X")}</size>";
                        string ba11 = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ban0), "☀")}</size>";

                        string banme = ban0 + ban1 + ban2 + ban3 + ban4 + ban5 + ban6 + ban7 + ban8 + ban9 + ban10 +"\r\n" + ba0 + ba1 + ba2 + ba3 + ba4 + ba5 + ba6 + ba7 + ba8 + ba9 + ba10 + ba11;

                        client.Character.RpcSetName($"{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.jss8), banme)}");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    //friendcodes 1 (closed)
                    if (client.FriendCode is "leafywinch#2382" or "jailtoy#0133" or "walkingdice#5285" or "scoopgooey#9820" or "innfancy#2127" or "artfulcod#9001" or "frostmolar#1359" or "everyswap#7877" or "iconicoar#2342" or "steamquits#4906" or "ruffseated#8388" or "nicestone#7505" or "ravencalyx#2196" or "iconicpun#5624" or "flathomey#1351" or "talentsalt#4516" or "namebasic#9510" or "waterpupal#6193" or "privyeater#0729" or "honeytired#7330" or "waryclaw#7449" or "basicstork#6394" or "mobileswap#4514" or "artfulcod#9001")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title

                        string dscfr = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor2), "ToH:ToR Discord Member")}</size>"; //GENERAL TITLE
                        string named = dscfr + "\r\n" + rname; //DISCORD FRIENDS

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug5), named)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    //friendcodes 2 (closed)
                    if (client.FriendCode is "sunnysolid#5221"  or "beansimple#8487"  or "fuzzytub#9375" or "earthygale#6105" or "bestgalaxy#3894" or "earthlycat#1182" or "Cornolive#0328" or "justlotto#9472" or "numbmoss#9309" or "mistygulf#7381" or "singlequay#0547" or "dupletoad#0685" or "raggedsofa#2041" or "wavealpha#6327" or "easybling#2701" or "livemice#2626" or "losermore#5811" or "toadcomic#5559" or "publictick#2626" or "secularjam#2662" or "planset#7735" or "newsconic#2387" or "onlycaret#1986" or "unseenkelp#2225")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title

                        string dscfr = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor2), "ToH:ToR Discord Member")}</size>"; //GENERAL TITLE
                        string named = dscfr + "\r\n" + rname; //DISCORD FRIENDS

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug5), named)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    //friendcodes 3 (open)
                    if (client.FriendCode is "barelybusy#6628" or "loftystool#2997" or "shotsocket#8722" or "staremetal#5307" or "sphinxchic#9616" or "jibtabular#7970" or "risingyawn#1027" or "spicysword#8701" or "sageday#8929" or "deepcookie#3432" or "sunkenmoon#7397" or "fallardent#0828")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title

                        string dscfr = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.tancolor2), "ToH:ToR Discord Member")}</size>"; //GENERAL TITLE
                        string named = dscfr + "\r\n" + rname; //DISCORD FRIENDS

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.aug5), named)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    //allie's disc 1 (open)
                    if (client.FriendCode is "beefystaff#5945" or "uncutminer#6550" or "enoughset#6278" or "voicespicy#4974" or "retroozone#9714" or "beespotty#5432" or "dovebliss#9271" or "vestruby#9127" or "enoughset#6278" or "available3#2356" or "sharpdrone#0857" or "deepstag#0210" or "acuteload#3485" or "goldcable#4166" or "tidymall#0483" or "vanvinous#8649" or "hugeglobe#9125" or "beespotty#5432" or "onionaway#3328")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title

                        string dscfr = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ukr10), "Among Sus Discord Member")}</size>"; //GENERAL TITLE
                        string named = dscfr + "\r\n" + rname; //DISCORD FRIENDS

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.ukr7), named)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                    //Family disc 1 (open)
                    if (client.FriendCode is "alphabye#3999" or "snuglife#4373" or "sorrymoon#7693" or "sageday#8929" or "tigerbitty#4312" or "coppermoor#5288" or "sparebank#8022" or "mightydoor#6974")
                    {
                        string fontSize = "1.5"; //name
                        string fontSize1 = "0.8"; //title

                        string dscfr = $"<size={fontSize1}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.fam1), "Discord Family Member")}</size>"; //GENERAL TITLE
                        string named = dscfr + "\r\n" + rname; //DISCORD FRIENDS

                        client.Character.RpcSetName($"<size={fontSize}>{Helpers.ColorString(Utils.GetRoleColor(CustomRoles.fam2), named)}</size>");
                        Main.devNames.Add(client.Character.PlayerId, rname);
                    }
                }, 3f, "Welcome Message & Name Check");
            }
        }
    }
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnPlayerLeft))]
    class OnPlayerLeftPatch
    {
        public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)] ClientData data, [HarmonyArgument(1)] DisconnectReasons reason)
        {
            //            Logger.info($"RealNames[{data.Character.PlayerId}]を削除");
            //            main.RealNames.Remove(data.Character.PlayerId);
            if (!AmongUsClient.Instance.AmHost) return;
            if (GameStates.IsInGame)
            {
                Utils.CountAliveImpostors();
                if (data.Character.Is(CustomRoles.TimeThief))
                    data.Character.ResetVotingTime();
                if (data.Character.GetCustomSubRole() == CustomRoles.LoversRecode && !data.Character.Data.IsDead)
                    foreach (var lovers in Main.LoversPlayers.ToArray())
                    {
                        Main.isLoversDead = true;
                        Main.LoversPlayers.Remove(lovers);
                        Main.HasModifier.Remove(lovers.PlayerId);
                        Main.AllPlayerCustomSubRoles[lovers.PlayerId] = CustomRoles.NoSubRoleAssigned;
                    }
                if (data.Character.Is(CustomRoles.Executioner) | data.Character.Is(CustomRoles.Swapper) && Main.ExecutionerTarget.ContainsKey(data.Character.PlayerId) && Main.ExeCanChangeRoles)
                {
                    data.Character.RpcSetCustomRole(Options.CRoleExecutionerChangeRoles[Options.ExecutionerChangeRolesAfterTargetKilled.GetSelection()]);
                    Main.ExecutionerTarget.Remove(data.Character.PlayerId);
                    RPC.RemoveExecutionerKey(data.Character.PlayerId);
                }
                if (data.Character.Is(CustomRoles.GuardianAngelTOU) && Main.GuardianAngelTarget.ContainsKey(data.Character.PlayerId))
                {
                    data.Character.RpcSetCustomRole(Options.CRoleGuardianAngelChangeRoles[Options.WhenGaTargetDies.GetSelection()]);
                    if (data.Character.IsModClient())
                        data.Character.RpcSetCustomRole(Options.CRoleGuardianAngelChangeRoles[Options.WhenGaTargetDies.GetSelection()]); //対象がキルされたらオプションで設定した役職にする
                    else
                    {
                        if (Options.CRoleGuardianAngelChangeRoles[Options.WhenGaTargetDies.GetSelection()] != CustomRoles.Amnesiac)
                            data.Character.RpcSetCustomRole(Options.CRoleGuardianAngelChangeRoles[Options.WhenGaTargetDies.GetSelection()]); //対象がキルされたらオプションで設定した役職にする
                        else
                            data.Character.RpcSetCustomRole(Options.CRoleGuardianAngelChangeRoles[2]);
                    }
                    Main.GuardianAngelTarget.Remove(data.Character.PlayerId);
                    RPC.RemoveGAKey(data.Character.PlayerId);
                }
                if (data.Character.Is(CustomRoles.Jackal))
                {
                    Main.JackalDied = true;
                    if (Options.SidekickGetsPromoted.GetBool())
                    {
                        foreach (var pc in PlayerControl.AllPlayerControls)
                        {
                            if (pc.Is(CustomRoles.Sidekick))
                                pc.RpcSetCustomRole(CustomRoles.Jackal);
                        }
                    }
                }
                if (Main.ColliderPlayers.Contains(data.Character) && CustomRoles.YingYanger.IsEnable() && Options.ResetToYinYang.GetBool())
                {
                    Main.DoingYingYang = false;
                }
                if (Main.ColliderPlayers.Contains(data.Character))
                    Main.ColliderPlayers.Remove(data.Character);
                if (data.Character.LastImpostor() || data.Character.Is(CustomRoles.Egoist))
                {
                    //Main.currentWinner = CustomWinner.None;
                    /*bool egoist = false;
                    foreach (var pc in PlayerControl.AllPlayerControls)
                    {
                        if (pc.Data.Disconnected || pc == null) continue;
                        CustomRoles pc_role = pc.GetCustomRole();
                        if (pc_role == CustomRoles.Egoist && !pc.Data.IsDead) egoist = true;
                    }
                    if (data.Character.Is(CustomRoles.Egoist) && egoist)
                    {
                        if (Main.AliveImpostorCount != 1) egoist = false;
                    }
                    if (!egoist)*/
                    ShipStatus.Instance.enabled = false;
                    ShipStatus.RpcEndGame(GameOverReason.ImpostorDisconnect, false);
                }
                if (Main.ExecutionerTarget.ContainsValue(data.Character.PlayerId) && Main.ExeCanChangeRoles)
                {
                    byte Executioner = 0x73;
                    Main.ExecutionerTarget.Do(x =>
                    {
                        if (x.Value == data.Character.PlayerId)
                            Executioner = x.Key;
                    });
                    if (!Utils.GetPlayerById(Executioner).Is(CustomRoles.Swapper))
                    {
                        Utils.GetPlayerById(Executioner).RpcSetCustomRole(Options.CRoleExecutionerChangeRoles[Options.ExecutionerChangeRolesAfterTargetKilled.GetSelection()]);
                        Main.ExecutionerTarget.Remove(Executioner);
                        RPC.RemoveExecutionerKey(Executioner);
                        Utils.NotifyRoles();
                    }
                }

                if (data.Character.Is(CustomRoles.Camouflager) && Main.CheckShapeshift[data.Character.PlayerId])
                {
                    Logger.Info($"Camouflager Revert ShapeShift", "Camouflager");
                    foreach (PlayerControl revert in PlayerControl.AllPlayerControls)
                    {
                        if (revert.Is(CustomRoles.Phantom) || revert == null || revert.Data.IsDead || revert.Data.Disconnected || revert == data.Character) continue;
                        revert.RpcRevertShapeshift(true);
                    }
                    Camouflager.DidCamo = false;
                }
                if (Main.GuardianAngelTarget.ContainsValue(data.Character.PlayerId))
                {
                    byte GA = 0x73;
                    Main.ExecutionerTarget.Do(x =>
                    {
                        if (x.Value == data.Character.PlayerId)
                            GA = x.Key;
                    });
                    // Utils.GetPlayerById(GA).RpcSetCustomRole(Options.CRoleGuardianAngelChangeRoles[Options.WhenGaTargetDies.GetSelection()]);
                    if (Utils.GetPlayerById(GA).IsModClient())
                        Utils.GetPlayerById(GA).RpcSetCustomRole(Options.CRoleGuardianAngelChangeRoles[Options.WhenGaTargetDies.GetSelection()]); //対象がキルされたらオプションで設定した役職にする
                    else
                    {
                        if (Options.CRoleGuardianAngelChangeRoles[Options.WhenGaTargetDies.GetSelection()] != CustomRoles.Amnesiac)
                            Utils.GetPlayerById(GA).RpcSetCustomRole(Options.CRoleGuardianAngelChangeRoles[Options.WhenGaTargetDies.GetSelection()]); //対象がキルされたらオプションで設定した役職にする
                        else
                            Utils.GetPlayerById(GA).RpcSetCustomRole(Options.CRoleGuardianAngelChangeRoles[2]);
                    }
                    Main.GuardianAngelTarget.Remove(GA);
                    RPC.RemoveGAKey(GA);
                    Utils.NotifyRoles();
                }
                if (PlayerState.GetDeathReason(data.Character.PlayerId) == PlayerState.DeathReason.etc) //死因が設定されていなかったら
                {
                    PlayerState.SetDeathReason(data.Character.PlayerId, PlayerState.DeathReason.Disconnected);
                    PlayerState.SetDead(data.Character.PlayerId);
                }
                if (AmongUsClient.Instance.AmHost && GameStates.IsLobby)
                {
                    _ = new LateTask(() =>
                    {
                        foreach (var pc in PlayerControl.AllPlayerControls)
                        {
                            pc.RpcSetNameEx(pc.GetRealName(isMeeting: true));
                        }
                    }, 1f, "SetName To Chat");
                }
            }
            if (Main.devNames.ContainsKey(data.Character.Data.PlayerId))
                Main.devNames.Remove(data.Character.Data.PlayerId);
            Logger.Info($"{data.PlayerName}(ClientID:{data.Id})が切断(理由:{reason})", "Session");
        }
    }
}
