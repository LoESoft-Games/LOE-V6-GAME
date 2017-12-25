namespace appengine.credits
{
    internal class getoffers : RequestHandler
    {
        // guid, password
        // <Error>Unable to find account</Error>
        protected override void HandleRequest()
        {
            WriteLine("<Offers><Tok>WUT</Tok><Exp>STH</Exp><Offer><Id>0</Id><Price>0</Price><RealmGold>1000</RealmGold><CheckoutJWT>1000</CheckoutJWT><Data>YO</Data><Currency>HKD</Currency></Offer></Offers>");
        }
    }
}