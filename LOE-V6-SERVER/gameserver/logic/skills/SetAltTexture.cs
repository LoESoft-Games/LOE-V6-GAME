#region

using gameserver.realm;
using gameserver.realm.entity;

#endregion

namespace gameserver.logic.behaviors
{
    public class SetAltTexture : Behavior
    {
        //State storage: none

        private readonly int index;

        public SetAltTexture(int index)
        {
            this.index = index;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            if ((host as Enemy).AltTextureIndex != index)
            {
                (host as Enemy).AltTextureIndex = index;
                host.UpdateCount++;
            }
        }
    }
}