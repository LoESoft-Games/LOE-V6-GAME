#region

using System;
using gameserver.realm;
using gameserver.realm.entity;

#endregion

namespace gameserver.logic.behaviors
{
    public class SetLootState : Behavior
    {
        private readonly string lootState;

        public SetLootState(string lootState = "")
        {
            this.lootState = lootState;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            if (host is Enemy)
                (host as Enemy).LootState = lootState;
            else throw new InvalidOperationException("Not an Enemy.");
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state) { }
    }
}
