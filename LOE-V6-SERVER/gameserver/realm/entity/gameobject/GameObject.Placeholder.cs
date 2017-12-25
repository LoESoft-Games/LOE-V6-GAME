namespace gameserver.realm.entity
{
    internal class Placeholder : GameObject
    {
        public Placeholder(RealmManager manager, int life)
            : base(manager, 0x070f, life, true, true, false)
        {
            Size = 0;
        }
    }
}