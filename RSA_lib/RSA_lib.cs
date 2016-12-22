using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace RSA_lib
{
    public interface RSAinterface
    {
        void DataToRSA(MemoryStream source, MemoryStream output, key publicKey);
        void RSAToData(MemoryStream source, MemoryStream output, key privateKey);
    }

    public class RSAclass : RSAinterface
    {
        public void DataToRSA(MemoryStream source, MemoryStream output, key publicKey)
        {
            RSA RSA = new RSA();
            int sourceByte;
            while ((sourceByte = source.ReadByte()) != -1)
            {
                output.WriteByte(RSA.RSACrypt((byte)sourceByte, publicKey));
            }

        }
        public void RSAToData(MemoryStream source, MemoryStream output, key privateKey)
        {
            RSA RSA = new RSA();
            int sourceByte;
            while ((sourceByte = source.ReadByte()) != -1)
            {
                output.WriteByte(RSA.RSACrypt((byte)sourceByte, privateKey));
            }
        }
    }

    public class RSA
    {
        public byte RSACrypt(byte source, key publicKey)
        {
            BigInteger result = BigInteger.Pow(source, (int)publicKey.a) % (BigInteger)publicKey.n;
            return (byte)result;
        }
    }


    public struct key
    {
        public int a;
        public int n;
        public key(int A, int N)
        {
            a = A;
            n = N;
        }
    }
}
