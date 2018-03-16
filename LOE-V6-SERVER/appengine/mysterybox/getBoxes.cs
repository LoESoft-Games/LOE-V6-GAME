#region

using log4net;
using System;
using System.IO;
using System.Xml;

#endregion

namespace appengine.mysterybox
{
    //scope
    internal static class CONSTANTS
    {
        internal const int GOLD = 0;
        internal const int FAME = 1;
        internal const int FORTUNETOKENS = 2;
        internal const byte MYSTERY_BOX = 0x01;
        internal const byte FORTUNE_GAME = 0x02;
    }

    internal class getBoxes : RequestHandler
    {
        protected override void HandleRequest()
        {
            string response = SerializeMiniGames.Serialize();
            using (StreamWriter wtr = new StreamWriter(Context.Response.OutputStream))
                wtr.Write(response);
        }
    }

    internal class SerializeMiniGames
    {
        internal static ILog log = LogManager.GetLogger(nameof(SerializeMiniGames));

        internal int BoxId { get; set; }
        internal string Title { get; set; }
        internal int Weight { get; set; }
        internal string Description { get; set; }
        internal string Contents { get; set; }
        internal MysteryBoxPrice PriceMB { get; set; }
        internal FortuneGamePrice PriceFG { get; set; }
        internal string Image { get; set; }
        internal string Icon { get; set; }
        internal Sale Sale { get; set; }
        internal DateTime StartTime { get; set; }
        internal int Left { get; set; }
        internal int Total { get; set; }

        internal static SerializeMiniGames GetBox(int id, byte type)
        {
            XmlDocument doc = new XmlDocument();

            string response = File.ReadAllText("mysterybox/miniGames.xml");

            if (response == null)
                return null;

            doc.LoadXml(response);

            XmlNodeList miniGames = type == CONSTANTS.MYSTERY_BOX ? doc.GetElementsByTagName("MysteryBox") : doc.GetElementsByTagName("FortuneGame");

            switch (type)
            {
                case CONSTANTS.MYSTERY_BOX:
                    {
                        if (miniGames.Count > 0)
                        {
                            for (int i = 0; i < miniGames.Count; i++)
                            {
                                if (miniGames[i].Attributes["id"].Value == $"{id}")
                                {
                                    int boxid_ = id;
                                    int weight_ = Convert.ToInt32(miniGames[i].Attributes["weight"].Value);
                                    string title_ = miniGames[i].Attributes["title"].Value;
                                    string description_ = null;
                                    string contents_ = miniGames[i]["Contents"].InnerText;
                                    int priceamount_ = Convert.ToInt32(miniGames[i]["Price"].Attributes["amount"].Value);
                                    int pricecurrency_ = Convert.ToInt32(miniGames[i]["Price"].Attributes["currency"].Value);
                                    string image_ = miniGames[i]["Image"].InnerText;
                                    string icon_ = null;
                                    DateTime starttime_ = Convert.ToDateTime(miniGames[i]["StartTime"].Value);
                                    DateTime saleend_ = DateTime.MinValue;
                                    int salecurrency_ = 0;
                                    int saleprice_ = 0;
                                    int left_ = Convert.ToInt32(miniGames[i]["Left"].InnerText);
                                    int total_ = Convert.ToInt32(miniGames[i]["Total"].InnerText);
                                    if (miniGames[i]["Sale"] != null)
                                    {
                                        saleend_ = Convert.ToDateTime(miniGames[i]["Sale"]["End"].Value);
                                        salecurrency_ = Convert.ToInt32(miniGames[i]["Sale"].Attributes["currency"].Value);
                                        saleprice_ = Convert.ToInt32(miniGames[i]["Sale"].Attributes["price"].Value);
                                    }

                                    return new SerializeMiniGames
                                    {
                                        BoxId = boxid_,
                                        Weight = weight_,
                                        Title = title_,
                                        Description = description_,
                                        Contents = contents_,
                                        PriceMB = new MysteryBoxPrice
                                        {
                                            Amount = priceamount_,
                                            Currency = pricecurrency_
                                        },
                                        Image = image_,
                                        Icon = icon_,
                                        StartTime = starttime_,
                                        Sale = saleend_ == DateTime.MinValue ? null : new Sale
                                        {
                                            SaleEnd = saleend_,
                                            Currency = salecurrency_,
                                            Price = saleprice_
                                        },
                                        Left = left_,
                                        Total = total_
                                    };
                                }
                            }
                        }
                    }
                    return null;
                case CONSTANTS.FORTUNE_GAME:
                    {
                        if (miniGames.Count > 0)
                        {
                            for (int i = 0; i < miniGames.Count; i++)
                            {
                                if (miniGames[i].Attributes["id"].Value == $"{id}")
                                {
                                    int boxid_ = id;
                                    string title_ = miniGames[i].Attributes["title"].Value;
                                    string description_ = null;
                                    string contents_ = miniGames[i]["Contents"].InnerText;
                                    int firstInGold_ = Convert.ToInt32(miniGames[i]["Price"].Attributes["firstInGold"].Value);
                                    int firstInToken_ = Convert.ToInt32(miniGames[i]["Price"].Attributes["firstInToken"].Value);
                                    int secondInGold_ = Convert.ToInt32(miniGames[i]["Price"].Attributes["secondInGold"].Value);
                                    string image_ = miniGames[i]["Image"].InnerText;
                                    string icon_ = null;
                                    DateTime starttime_ = Convert.ToDateTime(miniGames[i]["StartTime"].Value);
                                    DateTime endtime_ = Convert.ToDateTime(miniGames[i]["EndTime"].Value);

                                    return new SerializeMiniGames
                                    {
                                        BoxId = boxid_,
                                        Title = title_,
                                        Description = description_,
                                        Contents = contents_,
                                        PriceFG = new FortuneGamePrice
                                        {
                                            firstInGold = firstInGold_,
                                            firstInToken = firstInToken_,
                                            secondInGold = secondInGold_
                                        },
                                        Icon = icon_,
                                        Image = image_,
                                        StartTime = starttime_
                                    };
                                }
                            }
                        }
                    }
                    return null;
            }
            return null;
        }

        internal static string Serialize()
        {
            XmlDocument doc = new XmlDocument();

            string response = File.ReadAllText("mysterybox/miniGames.xml");

            if (response == null)
                return null;

            doc.LoadXml(response);

            try
            {
                StringWriter wtr = new StringWriter();
                doc.Save(wtr);
                return wtr.ToString();
            }
            catch (Exception error)
            {
                log.Error($"Unhandle exception: {error}.");
                return null;
            }
        }
    }

    internal class MysteryBoxPrice
    {
        internal int Amount { get; set; }
        internal int Currency { get; set; }
    }

    internal class FortuneGamePrice
    {
        internal int firstInGold { get; set; }
        internal int firstInToken { get; set; }
        internal int secondInGold { get; set; }
    }

    internal class Sale
    {
        internal int Price { get; set; }
        internal int Currency { get; set; }
        internal DateTime SaleEnd { get; set; }
    }
}