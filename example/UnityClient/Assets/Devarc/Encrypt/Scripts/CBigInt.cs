using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Devarc
{
    public struct CBigInt : IComparable, IComparable<CBigInt>
    {
        static string[] symbol_0 = new string[] { "", "K", "M", "G", "T"};
        static string[] symbol_1 = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        public SFloat mBase; // < 10
        public SInt mPow;

        public CBigInt(float _base, int _pow)
        {
            mBase = 1f;
            mPow = 0;
            for (int i = 0; i < _pow; i++)
            {
                mBase *= _base;
                while (mBase > 10f)
                {
                    mBase /= 10f;
                    mPow++;
                }
                while (mBase < 1f)
                {
                    mBase *= 10f;
                    mPow--;
                }
            }
        }

        public CBigInt(double value)
        {
            mPow = 0;
            var temp = value;
            while (temp > 10f)
            {
                temp /= 10f;
                mPow++;
            }
            while (temp < 1f)
            {
                temp *= 10f;
                mPow--;
            }
            mBase = (float)temp;
        }

        public override string ToString()
        {
            if (mPow < 3)
            {
                var fValue = mBase.Value * Mathf.Pow(10f, mPow.Value);
                var iValue = Mathf.RoundToInt(fValue);
                return iValue.ToString();
            }
            else
            {
                var mode = mPow % 3;
                var display = mBase.Value * Mathf.Pow(10f, mode);
                var symbol = getSymbol();
                var remain = Mathf.RoundToInt(display * 100f) % 100;
                if (remain == 0)
                {
                    return string.Format("{0:N0} {1}", display, symbol);
                }
                else if ((remain % 10) == 0)
                {
                    return string.Format("{0:N1} {1}", display, symbol);
                }
                else
                {
                    return string.Format("{0:N2} {1}", display, symbol);
                }
            }
        }

        string getSymbol()
        {
            int i = Mathf.Max(0, mPow / 3);
            if (i < symbol_0.Length)
                return symbol_0[i];

            i = Mathf.Max(0, (mPow / 3) - symbol_0.Length);
            if (i < symbol_1.Length)
                return symbol_1[i];

            List<char> list = new List<char>(4);
            do
            {
                list.Insert(0, symbol_1[i % symbol_1.Length][0]);
                i /= symbol_1.Length;
                i--;
            } while (i >= 0);

            return new string(list.ToArray());
        }

        public int CompareTo(object obj)
        {
            if (obj.GetType() == typeof(int))
            {
                var value = (int)obj;
                return CompareTo(new CBigInt(value));
            }
            if (obj.GetType() == typeof(float))
            {
                var value = (float)obj;
                return CompareTo(new CBigInt(value));
            }
            if (obj.GetType() == typeof(double))
            {
                var value = (double)obj;
                return CompareTo(new CBigInt(value));
            }
            Debug.LogError($"[BigInt::CompareTo] Not implemented: type={obj.GetType()}");
            return 0;
        }

        public int CompareTo(CBigInt other)
        {
            if (this.mPow > other.mPow)
                return 1;
            if (this.mPow < other.mPow)
                return -1;
            if (this.mBase > other.mBase)
                return 1;
            if (this.mBase < other.mBase)
                return -1;
            return 0;
        }

        public static CBigInt operator +(CBigInt p1, CBigInt p2)
        {
            var value = new CBigInt();
            value.mPow = Mathf.Max(p1.mPow, p2.mPow);
            value.mBase += Mathf.Pow(0.1f, value.mPow - p1.mPow) * p1.mBase;
            value.mBase += Mathf.Pow(0.1f, value.mPow - p2.mPow) * p2.mBase;
            return value;
        }

        public static CBigInt operator *(CBigInt p1, CBigInt p2)
        {
            var value = new CBigInt();
            value.mPow = p1.mPow + p2.mPow;
            value.mBase = p1.mBase * p2.mBase;
            while (value.mBase > 10f)
            {
                value.mBase /= 10f;
                value.mPow++;
            }
            return value;
        }

        public static CBigInt operator *(CBigInt p1, double p2)
        {
            return p1 * new CBigInt(p2);
        }

        public static CBigInt operator /(CBigInt p1, CBigInt p2)
        {
            var value = new CBigInt();
            value.mPow = p1.mPow - p2.mPow;
            value.mBase = p1.mBase / p2.mBase;
            while (value.mBase < 1f)
            {
                value.mBase *= 10f;
                value.mPow--;
            }
            return value;
        }

        public static CBigInt operator /(CBigInt p1, double p2)
        {
            return p1 / new CBigInt(p2);
        }


        public static bool operator <(CBigInt p1, CBigInt p2)
        {
            return p1.CompareTo(p2) < 0;
        }

        public static bool operator >(CBigInt p1, CBigInt p2)
        {
            return p1.CompareTo(p2) > 0;
        }
    }
}