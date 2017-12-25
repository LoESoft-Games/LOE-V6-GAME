#region

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

#endregion

namespace gameserver
{
    public class RC4
    {
        private readonly RC4Engine rc4;

        public RC4(byte[] rc4_key)
        {
            rc4 = new RC4Engine();
            rc4.Init(true, new KeyParameter(rc4_key));
        }

        public void EncryptRC4Cipher(byte[] buf, int offset, int len) => rc4.ProcessBytes(buf, offset, len, buf, offset);
    }
}