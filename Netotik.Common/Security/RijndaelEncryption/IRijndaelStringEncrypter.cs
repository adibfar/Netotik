using System;

namespace Netotik.Common.Security.RijndaelEncryption
{
    public interface IRijndaelStringEncrypter : IDisposable
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
