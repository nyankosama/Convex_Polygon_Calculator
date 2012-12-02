using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Poly_Rect_Calc
{
    class Calculator
    {
        private List<Point> pointList;
        private const int Y_BASE=0;
        
        public Calculator(){
            pointList = new List<Point>();
        }

        public double getArea()
        {
            //判断输入合理性,能否构成凸多边形
            if (!isConvexPolygon())
            {
                return -1;
            }
            //找出x轴上最左边的点和最右边的点
            List<Point> left = find_left();
            List<Point> right = find_right();
            //按顺序找出left和right之间left和right直线之上和之下的点
            List<Point> overPoints = getSortedPointsOverLine(left[0], right[0]);
            List<Point> belowPoints = getSortedPointsBelowLine(left[1], right[1]);
            double up_area = getAccArea(overPoints);
            double down_area = getAccArea(belowPoints);
            double area = up_area - down_area;
            return area;
        }

        private double getAccArea(List<Point> points)
        {
            double area = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                double height = points[i + 1].x - points[i].x;
                double edge1 = points[i].y - Y_BASE;
                double edge2 = points[i + 1].y - Y_BASE;
                double s_area = (edge1 + edge2) * height / 2;
                area += s_area;
            }
            return area;
        }


        private List<Point> getSortedPointsOverLine(Point left, Point right)
        {
            List<Point> overPoints = new List<Point>();
            foreach (Point point in pointList)
            {
                if (isOverBySomeLine(left, right, point) >= 0)
                    overPoints.Add(point);
            }
            overPoints.Sort();
            return overPoints;
        }

        private List<Point> getSortedPointsBelowLine(Point left, Point right)
        {
            List<Point> belowPoints = new List<Point>();
            foreach (Point point in pointList)
            {
                if (isOverBySomeLine(left, right, point) <= 0)
                    belowPoints.Add(point);

            }
            belowPoints.Sort();
            return belowPoints;
        }

        private List<Point> find_left()
        {
            List<Point> result = new List<Point>();
            Point left = pointList[0];
            foreach (Point point in pointList)
            {
                if (point.x <= left.x)
                {
                    left = point;
                }
            }
            //再次遍历看看有木有和left点x坐标相同的点
            foreach (Point point in pointList)
            {
                if (left.x == point.x)
                {
                    result.Add(point);
                }
            }
            if (result.Count == 1)
                result.Add(result[0]);
            if (result[0].y < result[1].y)
            {
                double tmp = result[0].y;
                result[0].y = result[1].y;
                result[1].y = tmp;
            }
            return result;
        }

        private List<Point> find_right()
        {
            List<Point> result = new List<Point>();
            Point right = pointList[0];
            foreach (Point point in pointList)
            {
                if (point.x > right.x)
                {
                    right = point;
                }
            }
            foreach (Point point in pointList)
            {
                if (right.x == point.x)
                {
                    result.Add(point);
                }
            }
            if (result.Count == 1)
                result.Add(result[0]);
            if (result[0].y < result[1].y)
            {
                double tmp = result[0].y;
                result[0].y = result[1].y;
                result[1].y = tmp;
            }
            return result;
        }

        /************************************************************************/
        /* 对于给定两点确定的直线，判断某点是否在直线只上                                                                     */
        /************************************************************************/
        private int isOverBySomeLine(Point po1,Point po2,Point p)
        {
            double[] k_b = getK_b(po1, po2);
            double k = 0;
            double b = 0;
            double y0 = 0;
            if (po1.x == po2.x)
            {
                if (p.x > po1.x)
                    return 1;
                else if (p.x < po1.x)
                    return -1;
                else
                    return 0;
            }
            else
            {
                k = k_b[0];
                b = k_b[1];
                y0 = k * p.x + b;
            }
            
            if (p.y > y0)
                return 1;
            else if (p.y < y0)
                return -1;
            else
                return 0;
        }

        /************************************************************************/
        /* 获得给定两点的直线的K和B                                                                     */
        /************************************************************************/
        private double[] getK_b(Point po1,Point po2)
        {
            double k = (po1.y - po2.y) / (po1.x - po2.x);
            double b = po1.y - (po1.y - po2.y) / (po1.x - po2.x) * po1.x;
            double[] result = new double[2];
            result[0] = k;
            result[1] = b;
            return result;
        }

        /************************************************************************/
        /* 判断所给点是否能构成凸多边形                                                                     */
        /************************************************************************/
        private Boolean isConvexPolygon()
        {
            if (pointList.Count <= 2)
                return false;
            for (int i = 0; i < pointList.Count; i++)
            {
                Boolean p = false;//是否需要继续判断在下面
                int size = pointList.Count;
                //判断是否在上面
                for (int k = 0; k < size - 2; k++)
                {
                    if (isOverBySomeLine(pointList[i % size], pointList[(i + 1) % size], pointList[(i + 2 + k) % size])<=0)
                    {
                        p = true;
                        break;
                    }
                }
                if (!p)
                    return true;

                if (p)
                {
                    //判断是否全在下面
                    for (int k = 0; k < size - 2; k++)
                    {
                        if (isOverBySomeLine(pointList[i % size], pointList[(i + 1) % size], pointList[(i + 2 + k) % size])>=0)
                        {
                            p = false;
                            break;
                        }
                        
                    }
                }
                return p;
                
            }
            return true;
        }

        

        public void addPoint(Point point)
        {
            pointList.Add(point);
        }

        public void addAll(ArrayList points)
        {
            foreach (Point point in points)
            {
                pointList.Add(point);
            }
        }
        
        public void deleteByIndex(int index)
        {
            pointList.RemoveAt(index);
        }

        public void clearAll()
        {
            pointList.Clear();
        }

        public void print()
        {
            foreach (Point point in pointList)
            {
                Console.WriteLine("("+point.x+","+point.y+")");
            }
        }
    }
}
