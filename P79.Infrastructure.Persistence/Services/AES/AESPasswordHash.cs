using System;
using System.Collections.Generic;
using System.Text;

namespace P79.Infrastructure.Persistence.Services.AES
{
    /// <summary>
    /// AES Password Hash: set "None" for no hashing.
    /// </summary>
    public enum AESPasswordHash : int
    {
        None = 0,
        MD5 = 1,
        SHA1 = 2
    }
    
}
