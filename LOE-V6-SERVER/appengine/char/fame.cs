#region

using core;

#endregion

namespace appengine.@char
{
    internal class fame : RequestHandler
    {
        // Account credentials not valid
        protected override void HandleRequest()
        {
            var character = Database.LoadCharacter(int.Parse(Query["accountId"]), int.Parse(Query["charId"]));
            if (character == null)
            {
                WriteErrorLine("Invalid character");
                return;
            }

            var fame = Fame.FromDb(character);
            if (fame == null)
            {
                WriteErrorLine("Character not dead");
                return;
            }
            WriteLine(fame.ToXml());
        }
    }
}