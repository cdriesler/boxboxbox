using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Box.Core.Geometry.Create
{
    public static class Curves
    {
        public static Curve LineSDL(Point2d startPoint, Vector2d direction, double length)
        {
            double startX = startPoint.X;
            double startY = startPoint.Y;

            double scaleFactor = length / direction.Length;
            Vector2d placementVector = direction * scaleFactor;

            double endX = placementVector.X;
            double endY = placementVector.Y;
            Point2d endPoint = new Point2d(endX, endY);

            Curve resultCurve = new LineCurve(startPoint, endPoint);

            return resultCurve;
        }

        public static Curve UnitXCurve()
        {
            Point3d startPoint = new Point3d(0, 0, 0);
            Point3d endPoint = new Point3d(1, 0, 0);

            Curve curve = new LineCurve(startPoint, endPoint);

            return curve;
        }

        public static Curve UnitXCurve(double length)
        {
            Point3d startPoint = new Point3d(0, 0, 0);
            Point3d endPoint = new Point3d(length, 0, 0);

            Curve curve = new LineCurve(startPoint, endPoint);

            return curve;
        }

        public static Curve CenteredXCurve()
        {
            Point3d startPoint = new Point3d(-0.5, 0, 0);
            Point3d endPoint = new Point3d(0.5, 0, 0);

            Curve curve = new LineCurve(startPoint, endPoint);

            return curve;
        }

        public static Curve CenteredXCurve(double length)
        {
            double dist = length / 2;

            Point3d startPoint = new Point3d(-dist, 0, 0);
            Point3d endPoint = new Point3d(dist, 0, 0);

            Curve curve = new LineCurve(startPoint, endPoint);

            return curve;
        }

        public static Curve UnitYCurve()
        {
            Point3d startPoint = new Point3d(0, 0, 0);
            Point3d endPoint = new Point3d(0, 1, 0);

            Curve curve = new LineCurve(startPoint, endPoint);

            return curve;
        }

        public static Curve UnitYCurve(double length)
        {
            Point3d startPoint = new Point3d(0, 0, 0);
            Point3d endPoint = new Point3d(0, length, 0);

            Curve curve = new LineCurve(startPoint, endPoint);

            return curve;
        }

        public static Curve CenteredYCurve()
        {
            Point3d startPoint = new Point3d(0, -0.5, 0);
            Point3d endPoint = new Point3d(0, 0.5, 0);

            Curve curve = new LineCurve(startPoint, endPoint);

            return curve;
        }

        public static Curve CenteredYCurve(double length)
        {
            double dist = length / 2;

            Point3d startPoint = new Point3d(0, -dist, 0);
            Point3d endPoint = new Point3d(0, dist, 0);

            Curve curve = new LineCurve(startPoint, endPoint);

            return curve;
        }

        public static Curve UnitZCurve()
        {
            Point3d startPoint = new Point3d(0, 0, 0);
            Point3d endPoint = new Point3d(0, 0, 1);

            Curve curve = new LineCurve(startPoint, endPoint);

            return curve;
        }

        public static Curve RectangleCWH(Point3d center, double width, double height)
        {
            double frcW = width / 2;
            double frcH = height / 2;

            Curve rectangle = new Rectangle3d(Plane.WorldXY, new Interval(-frcW, frcW), new Interval(-frcH, frcH)).ToNurbsCurve();

            return rectangle;
        }
    }
}
