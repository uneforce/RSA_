using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

namespace RSA
{
    class RSA
    {
        static Encoding enc8 = Encoding.UTF8;
        BigInteger p;
        BigInteger q;
        BigInteger n;
        BigInteger func;
        BigInteger e;
        BigInteger d;
        public RSA(int P, int Q)
        {
            p = P;
            q = Q;
            n = p * q;
            func = (p - 1) * (q - 1);
            Random rand = new Random();

            while (true)
            {
                e = rand.Next(1, Convert.ToInt32(func.ToString())); d = 7;
                if (!CheckMutual(Convert.ToInt32(d.ToString()), Convert.ToInt32(func.ToString())))
                    continue;
                break;
            }
            d = Inverse(d, func);
        }
        public static int NOD(int a, int b)
        {
            if (a < b)
            {
                int temp = a;
                a = b;
                b = temp;
            }

            while (b != 0)
            {
                int t = a % b;
                a = b;
                b = t;
            }

            return a;
        }
        public BigInteger Inverse(BigInteger c, BigInteger m)
        {
            BigInteger x, y, NOD;
            Euclid(m, c, out x, out y, out NOD);

            if (y < 0)
                y += m;
            return y;

        }
        public void Euclid(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y, out BigInteger NOD)
        {
            if (a < b)
            {
                BigInteger temp = a;
                a = b;
                b = temp;
            }

            BigInteger[] U = { a, 1, 0 };
            BigInteger[] V = { b, 0, 1 };
            BigInteger[] T = new BigInteger[3];

            while (V[0] != 0)
            {
                BigInteger q = U[0] / V[0];
                T[0] = U[0] % V[0];
                T[1] = U[1] - q * V[1];
                T[2] = U[2] - q * V[2];
                V.CopyTo(U, 0);
                T.CopyTo(V, 0);
            }

            NOD = U[0];
            x = U[1];
            y = U[2];

            return;
        }
        public static bool CheckMutual(int a, int b)
        {
            if (NOD(a, b) == 1)
                return true;
            return false;
        }
        public void RSA_encryption(string message)
        {
            BigInteger[] encryption = new BigInteger[message.Length];
            for (int i = 0; i < message.Length; ++i)
            {
                encryption[i] = Power(message[i], e, n);
            }
            BigInteger[] decryption = new BigInteger[message.Length];
            for (int i = 0; i < message.Length; ++i)
            {
                decryption[i] = Power(encryption[i], d, n);
            }
            string encripted = encryption.ToString();
            string decripted = decryption.ToString();
            Console.WriteLine(decryption[0]);
            int k = 0;
        }
        private BigInteger Power(BigInteger a, BigInteger b, BigInteger m) // a^b mod m
        {
            BigInteger tmp = a;
            BigInteger sum = tmp;
            for (int i = 1; i < b; i++)
            {
                for (int j = 1; j < a; j++)
                {
                    sum += tmp;
                    if (sum >= m)
                    {
                        sum -= m;
                    }
                }
                tmp = sum;
            }
            return tmp;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            int exit = 0, p, q;
            string message, encryption, decryption;
            do
            {
                Console.WriteLine("Input p:      ");
                p = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input 1:      ");
                q = Convert.ToInt32(Console.ReadLine());
                RSA rsa = new RSA(p, q);
                Console.WriteLine("Input message:   ");
                message = Console.ReadLine();
                rsa.RSA_encryption(message);
                Console.WriteLine("Input 1 to exit:      ");
                exit = Convert.ToInt32(Console.ReadLine());
            }
            while (exit != 1);

        }
    }
}
