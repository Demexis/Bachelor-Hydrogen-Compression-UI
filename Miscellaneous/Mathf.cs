﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms.Miscellaneous
{
    public static class Mathf
    {
        public static int Clamp(int x, int min, int max)
        {
            if (min > max) throw new System.Exception("Incorrect arguments (min can't be greater than max).");

            if (x < min) x = min;
            if (x > max) x = max;

            return x;
        }

        public static float Clamp(float x, float min, float max)
        {
            if (min > max) throw new System.Exception("Incorrect arguments (min can't be greater than max).");

            if (x < min) x = min;
            if (x > max) x = max;

            return x;
        }

        public static bool Between(int x, int min, int max)
        {
            if (min > max) throw new System.Exception("Incorrect arguments (min can't be greater than max).");

            return (x >= min && x <= max);
        }

        public static bool Between(float x, float min, float max)
        {
            if (min > max) throw new System.Exception("Incorrect arguments (min can't be greater than max).");

            return (x >= min && x <= max);
        }

        public static float NormalizedRelationBetween(int x, int p1, int p2)
        {
            if((x > p1 && x > p2) || (x < p1 && x < p2)) throw new System.Exception("Incorrect arguments (The x value is outside the range of points p1 and p2).");

            if (p1 > p2)
            {
                int temp = p1;
                p1 = p2;
                p2 = temp;
            }

            int dp = p2 - p1;
            x -= p1;

            return (float)x / dp;
        }
    }
}