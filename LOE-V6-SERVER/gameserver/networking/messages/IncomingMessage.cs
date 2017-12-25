namespace gameserver.networking.incoming
{
    public abstract class IncomingMessage : Message
    {
        public override void Crypt(Client client, byte[] dat, int offset, int len)
        {
            client.IncomingCipher.EncryptRC4Cipher(dat, offset, len);
        }
    }
}