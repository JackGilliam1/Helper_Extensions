using System;
using Extensions.Core.Numeric;

namespace Extensions.Core.MathFunctions
{
    /*
     * Author: Jack Gilliam
     * Date Created: 4/8/2012
     */
    public static class ShapeExtensions
    {
        #region Constants
        private const int HALF_RIGHT_ANGLE_DEG = 45;
        private const int RIGHT_ANGLE_DEG = 90;
        private const int HALF_CIRCLE_DEG = 180;
        private const int FULL_CIRCLE_DEG = 360;

        private const double HALF_RIGHT_ANGLE_RAD = Math.PI / 4;
        private const double RIGHT_ANGLE_RAD = Math.PI / 2;
        private const double HALF_CIRCLE_RAD = Math.PI;
        private const double FULL_CIRCLE_RAD = 2 * Math.PI;
        #endregion

        /// <summary>
        /// Indicates whether two sides make a right triangle
        /// </summary>
        /// <param name="sideOne">Opposite Side or Adjacent</param>
        /// <param name="sideTwo">Adjacent or Hypotenuse</param>
        /// <param name="neitherAreHyp">Mark as true if one of the specified sides is the hypotenuse</param>
        /// <returns>True if the two sides make a right triangle</returns>
        public static bool IsRightTriangle(double sideOne, double sideTwo, bool neitherAreHyp = false)
        {
            bool isTriangle = false;
            if (!NumericExtensions.IsZero(sideTwo) && !sideOne.IsZero())
            {
                if (neitherAreHyp)
                {
                    double A = Math.Pow(sideOne, 2);
                    double B = Math.Pow(sideTwo, 2);
                    double hypotenuse = Math.Sqrt(A + B);
                    double angleResult = sideOne/ hypotenuse;
                    double angle = Math.Asin(angleResult) + Math.Acos(angleResult);

                    isTriangle = angle == (RIGHT_ANGLE_RAD);
                }
                else
                {
                    double bigger = Math.Max(sideOne, sideTwo);
                    double smaller = Math.Min(sideOne, sideTwo);

                    double oppositeAngle = Math.Asin(smaller / bigger);
                    double adjacentAngle = Math.Acos(smaller / bigger);

                    double bondedAngle = oppositeAngle + adjacentAngle;
                    isTriangle = bondedAngle == (RIGHT_ANGLE_RAD);
                }
            }
            return isTriangle;
        }
    }
}