namespace gameserver.networking.outgoing
{
    public abstract class OutgoingMessage : Message
    {
        public override void Crypt(Client client, byte[] dat, int offset, int len)
        {
            client.OutgoingCipher.EncryptRC4Cipher(dat, offset, len);
        }
    }
}