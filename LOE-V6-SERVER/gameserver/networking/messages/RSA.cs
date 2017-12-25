#region

using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;

#endregion

namespace gameserver
{
    public class RSA
    {
        public static readonly RSA Instance = new RSA(@"
-----BEGIN RSA PRIVATE KEY-----
MIIBOAIBAAJAbbhQZn5+RkpoKXpoStdVLFwndjGi8ilqzgXr16znacj9mz6BCV/O
91hdnB9cSvwfUHsFz2xEQBw4dH8dTtUWGwIDAQABAkAkj3xLCu9s4LJgz+ccuTAq
ffKwUc3oP6DVUefKkFT0TJIoOYW4+gzemTa6qC7R7DwmjZHUqZZHFaSnCu5yzxsx
AiEAwyN4aCGAdFG+vLyI7PT7d+rgdOIYZ1+soCatP9uxjD0CIQCP8Lt0JjdkrIP5
B40iGwxsMCbP1AlvqQkSSD6iUtYZNwIgB1xHJmZdGgYbU7Mo1wdGlPdfEAmXMg8B
y+ipkEcRI2ECID+urxClM867sKvF1oAnXWikKRe75Ozc6WGISwXABm8jAiBPRnCM
WDhCglDCg/rgOdTgi5cA22mcab/wjkWXkrYW6g==
-----END RSA PRIVATE KEY-----");

        private readonly RsaEngine engine;
        private readonly AsymmetricKeyParameter key;

        private RSA(string privPem)
        {
            key = (new PemReader(new StringReader(privPem.Trim())).ReadObject() as AsymmetricCipherKeyPair).Private;
            engine = new RsaEngine();
            engine.Init(true, key);
        }

        public string Decrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            byte[] dat = Convert.FromBase64String(str);
            Pkcs1Encoding encoding = new Pkcs1Encoding(engine);
            encoding.Init(false, key);
            return Encoding.UTF8.GetString(encoding.ProcessBlock(dat, 0, dat.Length));
        }

        public string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            byte[] dat = Encoding.UTF8.GetBytes(str);
            Pkcs1Encoding encoding = new Pkcs1Encoding(engine);
            encoding.Init(true, key);
            return Convert.ToBase64String(encoding.ProcessBlock(dat, 0, dat.Length));
        }
    }
}