using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace gameserver.networking.error
{
    internal static class JSONErrorIDHandler
    {
        internal static string FormatedJSONError(ErrorIDs errorID, string[] labels, string[] arguments)
        {
            string s = null;
            List<JSONError> toSerialize;
            if (labels != null && arguments != null)
                toSerialize = GetJSONError(errorID, labels, arguments);
            else
                toSerialize = GetJSONError(errorID);
            int lenght = toSerialize.Count;
            for (int i = 0; i < lenght; i++)
            {
                if (toSerialize.Count > 1)
                    s += JsonConvert.SerializeObject(toSerialize[0]) + ",";
                else
                    s += JsonConvert.SerializeObject(toSerialize[0]);
                toSerialize.RemoveAt(0);
            }
            return s;
        }

        private static List<JSONError> GetJSONError(ErrorIDs errorID)
        {
            using (StreamReader rdr = new StreamReader($"networking/error/e{(int)errorID}.json"))
            {
                string JSONData = rdr.ReadToEnd();
                List<JSONError> error = JsonConvert.DeserializeObject<List<JSONError>>(JSONData);
                return error;
            }
        }

        private static List<JSONError> GetJSONError(ErrorIDs errorID, string[] labels, string[] arguments)
        {
            using (StreamReader rdr = new StreamReader($"networking/error/e{(int)errorID}.json"))
            {
                string JSONData = rdr.ReadToEnd();

                List<string> parseError = new List<string>();

                int j;
                int lastValid = labels.Length - 1;

                for (int i = 0; i < labels.Length; i++)
                {
                    if (i == 0)
                        parseError.Add(JSONData.Replace(labels[i], arguments[i]));
                    else
                    {
                        j = i - 1;
                        parseError.Add(parseError[j].Replace(labels[i], arguments[i]));
                    }
                }

                List<JSONError> error = JsonConvert.DeserializeObject<List<JSONError>>(parseError[lastValid]);

                return error;
            }
        }

        private struct JSONError
        {
            public string title;
            public uint titleColor;
            public string description;
        }
    }
}
