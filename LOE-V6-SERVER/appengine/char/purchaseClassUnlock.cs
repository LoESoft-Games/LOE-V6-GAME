#region

using core;
using Newtonsoft.Json;

#endregion

namespace appengine.@char
{
    internal class purchaseClassUnlock : RequestHandler
    {
        // guid, password, classType
        // <Error>Bad input to character unlock</Error>
        // <Error><Text>Not enough Gold.</Text></Error>
        protected override void HandleRequest()
        {
            DbAccount acc;
            var status = Database.Verify(Query["guid"], Query["password"], out acc);
            if (status == LoginStatus.OK)
            {
                ushort classType;
                int price;
                if (ushort.TryParse(Query["classType"], out classType))
                    if (acc.Credits < (price = GameData.ObjectDescs[classType].UnlockCost))
                        WriteErrorLine("<Text>Not enough Gold.</Text>");
                    else
                    {
                        if (Database.Hashes.GetString(0, $"classAvailability.{acc.AccountId}", classType.ToString()).Exec()
                            == JsonConvert.SerializeObject(new DbClassAvailabilityEntry
                            {
                                Id = GameData.ObjectTypeToId[classType],
                                Restricted = "unrestricted"
                            }))
                            WriteErrorLine("Class already unlocked");
                        else
                        {
                            Database.Hashes.Set(0, $"classAvailability.{acc.AccountId}", classType.ToString(),
                            JsonConvert.SerializeObject(new DbClassAvailabilityEntry
                            {
                                Id = GameData.ObjectTypeToId[classType],
                                Restricted = "unrestricted"
                            }));
                            Database.UpdateCredit(acc, -price);
                            acc.Flush();
                            acc.Reload();
                            WriteLine("<Success />");
                        }
                    }
                else
                    WriteErrorLine("Bad input to character unlock");
            }
            else
                WriteErrorLine("Bad input to character unlock");
        }
    }
}