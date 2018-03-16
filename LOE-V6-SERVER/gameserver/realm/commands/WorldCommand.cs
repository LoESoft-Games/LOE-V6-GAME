#region

using System.Collections.Generic;
using System.Linq;
using System.Text;
using gameserver.networking.incoming;
using gameserver.networking.outgoing;
using gameserver.realm.entity;
using gameserver.realm.entity.player;
using core.config;
using gameserver.realm.commands.mreyeball;

#endregion

namespace gameserver.realm.commands
{
    internal class TutorialCommand : Command
    {
        public TutorialCommand() : base("tutorial") { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            player.Client.Reconnect(new RECONNECT
            {
                Host = "",
                Port = Settings.GAMESERVER.PORT,
                GameId = World.TUT_ID,
                Name = "Tutorial",
                Key = Empty<byte>.Array,
            });
            return true;
        }
    }

    internal class TradeCommand : Command
    {
        public TradeCommand() : base("trade") { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (string.IsNullOrWhiteSpace(args[0]))
            {
                player.SendInfo("Usage: /trade <player name>");
                return false;
            }
            player.RequestTrade(time, new REQUESTTRADE
            {
                Name = args[0]
            });
            return true;
        }
    }

    internal class WhoCommand : Command
    {
        public WhoCommand() : base("who") { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            StringBuilder sb = new StringBuilder("Players online: ");
            Player[] copy = player.Owner.Players.Values.ToArray();
            for (int i = 0; i < copy.Length; i++)
            {
                if (i != 0) sb.Append(", ");
                sb.Append(copy[i].Name);
            }

            player.SendInfo(sb.ToString());
            return true;
        }
    }

    internal class ServerCommand : Command
    {
        public ServerCommand() : base("server") { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            player.SendInfo(player.Owner.Name);
            return true;
        }
    }

    internal class PauseCommand : Command
    {
        public PauseCommand() : base("pause") { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (player.HasConditionEffect(ConditionEffectIndex.Paused))
            {
                player.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Paused,
                    DurationMS = 0
                });
                player.SendInfo("Game resumed.");
            }
            else
            {
                foreach (Enemy i in player.Owner.EnemiesCollision.HitTest(player.X, player.Y, 8).OfType<Enemy>())
                {
                    if (i.ObjectDesc.Enemy)
                    {
                        player.SendInfo("Not safe to pause.");
                        return false;
                    }
                }
                player.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Paused,
                    DurationMS = -1
                });
                player.SendInfo("Game paused.");
            }
            return true;
        }
    }

    internal class TeleportCommand : Command
    {
        public TeleportCommand() : base("teleport") { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            try
            {
                if (string.Equals(player.Name.ToLower(), args[0].ToLower()))
                {
                    player.SendInfo("You are already at yourself, and always will be!");
                    return false;
                }

                foreach (KeyValuePair<int, Player> i in player.Owner.Players)
                {
                    if (i.Value.Name.ToLower() == args[0].ToLower().Trim())
                    {
                        player.Teleport(time, new TELEPORT
                        {
                            ObjectId = i.Value.Id
                        });
                        return true;
                    }
                }
                player.SendInfo(string.Format("Cannot teleport, {0} not found!", args[0].Trim()));
            }
            catch
            {
                player.SendHelp("Usage: /teleport <player name>");
            }
            return false;
        }
    }

    internal class TellCommand : Command
    {
        public TellCommand() : base("tell") { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (!player.NameChosen)
            {
                player.SendError("Choose a name!");
                return false;
            }

            if (args.Length < 2)
            {
                player.SendHelp("Usage: /tell <player name> <text>");
                return false;
            }

            string playername = args[0].Trim();
            string msg = string.Join(" ", args, 1, args.Length - 1);

            if (string.Equals(player.Name.ToLower(), playername.ToLower()))
            {
                player.SendInfo("Quit telling yourself!");
                return false;
            }

            if (playername.ToLower() == MrEyeball_Dictionary.PacketID[MrEyeball_Dictionary.MrEyeball_PacketID.MREYEBALL])
            {
                string cmd = msg.ToLower();
                MrEyeball_Dictionary.HandleCommands(cmd, player);
                return true;
            }

            foreach (var i in player.Manager.Clients.Values)
            {
                if (i.Account.NameChosen && i.Account.Name.EqualsIgnoreCase(playername))
                {
                    player.Client.SendMessage(new TEXT()
                    {
                        ObjectId = player.Id,
                        BubbleTime = 10,
                        Stars = player.Stars,
                        Name = player.Name,
                        Admin = 0,
                        Recipient = i.Account.Name,
                        Text = msg.ToSafeText(),
                        CleanText = "",
                        TextColor = 0x123456,
                        NameColor = 0x123456
                    });

                    i.SendMessage(new TEXT()
                    {
                        ObjectId = i.Player.Owner.Id,
                        BubbleTime = 10,
                        Stars = player.Stars,
                        Name = player.Name,
                        Admin = 0,
                        Recipient = i.Account.Name,
                        Text = msg.ToSafeText(),
                        CleanText = "",
                        TextColor = 0x123456,
                        NameColor = 0x123456
                    });
                    return true;
                }
            }
            player.SendInfo($"{playername} not found.");
            return false;
        }
    }
}