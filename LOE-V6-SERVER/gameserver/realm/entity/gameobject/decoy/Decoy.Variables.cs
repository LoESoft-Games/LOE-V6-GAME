#region

using Mono.Game;
using System;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.realm.entity
{
    partial class Decoy
    {
        private static readonly Random rand = new Random();
        private readonly int duration;
        private readonly Player player;
        private readonly float speed;
        private Vector2 direction;
        private bool exploded;
    }
}
