namespace gameserver.realm.entity
{
    partial class Sign : GameObject
    {
        public Sign(RealmManager manager, ushort objType)
            : base(manager, objType, null, true, false, false)
        {
        }

        public override bool HitByProjectile(Projectile projectile, RealmTime time)
        {
            return false;
        }
    }
}