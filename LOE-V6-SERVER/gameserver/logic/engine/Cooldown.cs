#region

using core.config;
using System;

#endregion

namespace gameserver.logic
{
    public struct Cooldown
    {
        public readonly int CoolDown;
        public readonly int Variance;

        public Cooldown(int cooldown, int variance)
        {
            CoolDown = cooldown;
            Variance = variance;
        }

        public Cooldown Normalize() => CoolDown == 0 ? (int)(1000 + (1000 * 2.5 / Settings.GAMESERVER.TICKETS_PER_SECOND)) : (int)(CoolDown + (CoolDown * 2.5 / Settings.GAMESERVER.TICKETS_PER_SECOND));

        public Cooldown Normalize(int def) => CoolDown == 0 ? def : this;

        public int Next(Random rand) => Variance == 0 ? CoolDown : CoolDown + rand.Next(-Variance, Variance + 1);

        public static implicit operator Cooldown(int cooldown) => new Cooldown(cooldown, 0);
    }
}