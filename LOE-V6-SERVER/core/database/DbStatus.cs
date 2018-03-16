#region

using System;

#endregion

namespace core
{
    public enum LoginStatus
    {
        OK,
        AccountNotExists,
        InvalidCredentials
    }

    public enum RegisterStatus
    {
        OK,
        UsedName
    }

    public enum GuildCreateStatus
    {
        OK,
        UsedName,
        InvalidName,
    }

    public enum AddGuildMemberStatus
    {
        OK,
        NameNotChosen,
        AlreadyInGuild,
        InAnotherGuild,
        IsAMember,
        GuildFull,
        Error
    }

    public enum CreateStatus
    {
        OK,
        ReachCharLimit
    }

    public static class StatusInfo
    {
        public static string GetInfo(this LoginStatus status)
        {
            switch (status)
            {
                case LoginStatus.InvalidCredentials:
                    return "Error.incorrectEmailOrPassword";
                case LoginStatus.AccountNotExists:
                    return "Error.accountNotFound";
                case LoginStatus.OK:
                    return "OK";
            }
            throw new ArgumentException("status");
        }

        public static string GetInfo(this RegisterStatus status)
        {
            switch (status)
            {
                case RegisterStatus.UsedName:
                    return "Error.nameAlreadyInUse";
                case RegisterStatus.OK:
                    return "OK";
            }
            throw new ArgumentException("status");
        }

        public static string GetInfo(this CreateStatus status)
        {
            switch (status)
            {
                case CreateStatus.ReachCharLimit:
                    return "Too many characters";
                case CreateStatus.OK:
                    return "OK";
            }
            throw new ArgumentException("status");
        }
    }
}