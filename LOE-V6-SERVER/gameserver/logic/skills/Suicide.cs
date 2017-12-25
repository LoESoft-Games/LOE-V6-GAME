#region

using System;
using gameserver.realm;
using gameserver.realm.entity;

#endregion

namespace gameserver.logic.behaviors
{
    public class Suicide : Behavior
    {
        //State storage: timer

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            if (!(host is Enemy))
                throw new NotSupportedException("Use Decay instead");
            (host as Enemy).Death(time);
        }
    }
}