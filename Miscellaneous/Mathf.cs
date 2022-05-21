using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.Miscellaneous
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
            if(p1 == p2) throw new System.Exception("Incorrect arguments (p1 should not be equal to p2).");

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

        public static bool LineSegmentsIntersect(int p1x, int p1y, int p2x, int p2y)
        {
            if (p1x > p1y) throw new Exception("Incorrect arguments (The y1 should be greater than x1).");
            if (p2x > p2y) throw new Exception("Incorrect arguments (The y2 should be greater than x2).");

            return (
                Mathf.Between(p1x, p2x, p2y) ||
                Mathf.Between(p1x + p1y, p2x, p2y) ||
                Mathf.Between(p2x, p1x, p1y) ||
                Mathf.Between(p2x + p2y, p1x, p1y)
            );
        }

        public static (int x, int y) LineSegmentsConjunction(int p1x, int p1y, int p2x, int p2y)
        {
            if (LineSegmentsIntersect(p1x, p1y, p2x, p2y) == false) throw new Exception("Incorrect arguments (Lines don't intersect).");

            return (Math.Max(p1x, p2x), Math.Min(p1y, p2y));
        }
    }
}
