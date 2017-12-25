#region


#endregion

namespace gameserver.networking
{
    public enum ErrorIDs : int
    {
        OUTDATED_CLIENT = 0,
        DISABLE_GUEST_ACCOUNT = 1,
        SERVER_FULL = 2,
        ACCOUNT_BANNED = 3,
        INVALID_DISCONNECT_KEY = 4,
        LOST_CONNECTION = 5,
        UNKNOWN = 6,
        OUTDATED_INTERNAL_CLIENT = 7
    }
}
