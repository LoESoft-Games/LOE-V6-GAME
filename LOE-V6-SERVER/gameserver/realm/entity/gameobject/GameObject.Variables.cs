namespace gameserver.realm.entity
{
    partial class GameObject
    {
        public bool Vulnerable { get; private set; }
        public bool Static { get; private set; }
        public bool Hittestable { get; private set; }
        public int HP { get; set; }
        public bool Dying { get; private set; }
    }
}
