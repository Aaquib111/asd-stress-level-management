using System;
using System.Security.Cryptography;
using WindesHeartSDK.Helpers;

namespace WindesHeartSDK.Devices.MiBand4Device.Helpers
{
    public static class MiBand4ConversionHelper
    {

        public static byte[] CreateKey(byte[] value)
        {
            byte[] bytes = { 0x03, 0x00 };
            //byte[] secretKey = { 0x62, 0xd9, 0x71, 0x94, 0xe2, 0xaf, 0x5c, 0x96, 0xfd, 0xad, 0x5a, 0x29, 0x3b, 0xca, 0xd9, 0xf2 }; //DEVICE 4
            //byte[] secretKey = { 0x97, 0x2c, 0xdb, 0xdb, 0x9f, 0x2d, 0x51, 0x5b, 0x96, 0xdf, 0xe4, 0x64, 0x65, 0xd3, 0x78, 0xf2 }; //DEVICE 3
            //byte[] secretKey = { 0xeb, 0xf7, 0xed, 0x83, 0x4e, 0x44, 0x81, 0x15, 0xb5, 0x94, 0x5a, 0x80, 0xc7, 0xec, 0xbf, 0xc6 }; //DEVICE 2
            //byte[] secretKey = { 0x53, 0x2b, 0x1e, 0x51, 0x09, 0xd8, 0x3e, 0xa5, 0x10, 0x0f, 0x08, 0xa3, 0x87, 0xe0, 0xd3, 0x20 }; //DEVICE 1
            byte[] secretKey = { 0x52, 0x17, 0xdd, 0x63, 0x06, 0x9a, 0x84, 0x30, 0xfa, 0x49, 0xc8, 0x1a, 0x9d, 0xff, 0xac, 0x92 }; // Your key here, See docs for more info DEVICE 5

            value = ConversionHelper.CopyOfRange(value, 3, 19);
            byte[] buffer = EncryptBuff(secretKey, value);
            byte[] endBytes = new byte[18];
            Buffer.BlockCopy(bytes, 0, endBytes, 0, 2);
            Buffer.BlockCopy(buffer, 0, endBytes, 2, 16);
            return endBytes;
        }

        public static byte[] EncryptBuff(byte[] sessionKey, byte[] buffer)
        {
            AesManaged myAes = new AesManaged();

            myAes.Mode = CipherMode.ECB;
            myAes.Key = sessionKey;
            myAes.Padding = PaddingMode.None;

            ICryptoTransform encryptor = myAes.CreateEncryptor();
            return encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
        }


    }
}
