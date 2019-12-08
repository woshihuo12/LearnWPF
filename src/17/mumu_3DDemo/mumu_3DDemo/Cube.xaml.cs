using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using _3DTools;

namespace mumu_3DDemo
{
    public partial class Cube : Page
    {
        public Cube()
        {
            InitializeComponent();
            ScreenSpaceLines3D line3D = null;
            MeshGeometry3D mesh =
               CreateCube(new Point3D(0, 0, 0), 2, ref line3D);
            mesh.Freeze();

            GeometryModel3D geomod = new GeometryModel3D();
            geomod.Geometry = mesh;

            geomod.Material = new DiffuseMaterial(Brushes.Cyan);
            geomod.BackMaterial = new DiffuseMaterial(Brushes.Red);

            ModelVisual3D modvis = new ModelVisual3D();
            modvis.Content = geomod;
            myViewport.Children.Add(modvis);

            ScreenSpaceLines3D line3D2 = null;
            MeshGeometry3D mesh2 =
               CreateCube(new Point3D(0, 0, -4), 2, ref line3D2);
            mesh2.Freeze();

            GeometryModel3D geomod2 = new GeometryModel3D();
            geomod2.Geometry = mesh2;
            geomod2.Material = new DiffuseMaterial(Brushes.Cyan);
            geomod2.BackMaterial = new DiffuseMaterial(Brushes.Red);

            ModelVisual3D modvis2 = new ModelVisual3D();
            modvis2.Content = geomod2;
            myViewport.Children.Add(modvis2);

        }

        public MeshGeometry3D CreateCube(Point3D center, double side,  ref ScreenSpaceLines3D line3D)
        {

            // 如果line3D非空，则需要绘制构建的三角网
            bool isWire = false;
            if (line3D != null)
                isWire = true;

            
            MeshGeometry3D mesh = new MeshGeometry3D();
            double a = side / 2.0;
            Point3D[] p = new Point3D[8];
            p[0] = new Point3D(-a,  a,  a);
            p[1] = new Point3D( a,  a,  a);
            p[2] = new Point3D( a,  a, -a);
            p[3] = new Point3D(-a,  a, -a);
            p[4] = new Point3D(-a, -a,  a);
            p[5] = new Point3D( a, -a,  a);
            p[6] = new Point3D( a, -a, -a);
            p[7] = new Point3D(-a, -a, -a);

            // 添加顶点
            for (int i = 0; i < 8; i++)
            {
                p[i] += (Vector3D)center;
                mesh.Positions.Add(p[i]);
            }

            // 设置点的顺序
            CreateTriangleIndices(ref mesh, 0, 1, 3);
            CreateTextureCoordinates(ref mesh, new Point(0, 1), new Point(1, 1), new Point(0, 0));
            CreateTriangleIndices(ref mesh, 1, 2, 3);
            CreateTextureCoordinates(ref mesh, new Point(1, 1), new Point(1, 0), new Point(0, 0));

            CreateTriangleIndices(ref mesh, 4, 7, 0);
            CreateTextureCoordinates(ref mesh, new Point(1, 1), new Point(0, 1), new Point(1, 0));
            CreateTriangleIndices(ref mesh, 3, 0, 7);
            CreateTextureCoordinates(ref mesh, new Point(0, 0), new Point(1, 0), new Point(0, 1));

            CreateTriangleIndices(ref mesh, 4, 7, 5);
            CreateTextureCoordinates(ref mesh, new Point(0, 1), new Point(0, 0), new Point(1, 1));
            CreateTriangleIndices(ref mesh, 5, 7, 6);
            CreateTextureCoordinates(ref mesh, new Point(1, 1), new Point(0, 0), new Point(1, 0));

            CreateTriangleIndices(ref mesh, 1, 5, 6);
            CreateTextureCoordinates(ref mesh, new Point(1, 0), new Point(1, 1), new Point(0, 1));
            CreateTriangleIndices(ref mesh, 2, 1, 6);
            CreateTextureCoordinates(ref mesh, new Point(0, 0), new Point(1, 0), new Point(0, 1));


            CreateTriangleIndices(ref mesh, 0, 4, 1);
            CreateTextureCoordinates(ref mesh, new Point(0, 0), new Point(0, 1), new Point(1, 0));
            CreateTriangleIndices(ref mesh, 4, 5, 1);
            CreateTextureCoordinates(ref mesh, new Point(0, 1), new Point(1, 1), new Point(1, 0));

            CreateTriangleIndices(ref mesh, 3, 2, 7);
            CreateTextureCoordinates(ref mesh, new Point(0, 0), new Point(1, 0), new Point(0, 1));
            CreateTriangleIndices(ref mesh, 2, 6, 7);
            CreateTextureCoordinates(ref mesh, new Point(1, 0), new Point(1, 1), new Point(0, 1));


            // 如果需要构建三角网，使用ScreenSpaceLines3D构建三角网
            if (isWire)
            {
                CreateScreenlines(ref line3D, p[0], p[1], p[3]);
                
                CreateScreenlines(ref line3D, p[1], p[2], p[3]);
                

                CreateScreenlines(ref line3D, p[4], p[7], p[0]);
                CreateScreenlines(ref line3D, p[3], p[0], p[7]);

                CreateScreenlines(ref line3D, p[4], p[7], p[5]);
                CreateScreenlines(ref line3D, p[5], p[7], p[6]);

                CreateScreenlines(ref line3D, p[1], p[5], p[6]);
                CreateScreenlines(ref line3D, p[2], p[1], p[6]);


                CreateScreenlines(ref line3D, p[0], p[4], p[1]);
                CreateScreenlines(ref line3D, p[4], p[5], p[1]);

                CreateScreenlines(ref line3D, p[3], p[2], p[7]);
                CreateScreenlines(ref line3D, p[2], p[6], p[7]);
            }
            return mesh;
        }

        private void CreateTriangleIndices(ref MeshGeometry3D mesh,int i, int j,int k)
        {
            mesh.TriangleIndices.Add(i);
            mesh.TriangleIndices.Add(j);
            mesh.TriangleIndices.Add(k);
        }

        private void CreateTextureCoordinates(ref MeshGeometry3D mesh, Point i,Point j,Point k)
        {
            mesh.TextureCoordinates.Add(i);
            mesh.TextureCoordinates.Add(j);
            mesh.TextureCoordinates.Add(k);

        }


        private void CreateScreenlines(ref ScreenSpaceLines3D line3D, Point3D pi, Point3D pj, Point3D pk)
        {
            line3D.Points.Add(pi);
            line3D.Points.Add(pj);
            line3D.Points.Add(pj);
            line3D.Points.Add(pk);
            line3D.Points.Add(pk);
            line3D.Points.Add(pi);

        }


        public MeshGeometry3D Create36Cube(Point3D center, double side, ref ScreenSpaceLines3D line3D)
        {

            // 如果line3D非空，则需要绘制构建的三角网
            bool isWire = false;
            if (line3D != null)
                isWire = true;


            MeshGeometry3D mesh = new MeshGeometry3D();
            double a = side / 2.0;
            Point3D[] p = new Point3D[36];
            // 上面
            p[0] = new Point3D(-a, a, a);
            p[1] = new Point3D(a, a, a);
            p[2] = new Point3D(a, a, -a);

            p[3] = new Point3D(a, a, -a);
            p[4] = new Point3D(-a, a, -a);
            p[5] = new Point3D(-a, a, a);

            // 下面
            p[6] = new Point3D(-a, -a, a);
            p[7] = new Point3D(-a, -a, -a);
            p[8] = new Point3D(a, -a, -a);

            p[9] = new Point3D(a, -a, -a);
            p[10] = new Point3D(a, -a, a);
            p[11] = new Point3D(-a, -a, a);


            // 前面
            p[12] = new Point3D(-a, a, a);
            p[13] = new Point3D(-a, -a, a);
            p[14] = new Point3D(a, -a, a);

            
            p[15] = new Point3D(a, -a, a);
            p[16] = new Point3D(a, a, a);
            p[17] = new Point3D(-a, a, a);

            // 后面
            p[18] = new Point3D(-a, a, -a);
            p[19] = new Point3D(a, a, -a);
            p[20] = new Point3D(a, -a, -a);


            p[21] = new Point3D(a, -a, -a);
            p[22] = new Point3D(-a, -a, -a);
            p[23] = new Point3D(-a, a, -a);

            // 左面
            p[24] = new Point3D(-a, a, a);
            p[25] = new Point3D(-a, a, -a);
            p[26] = new Point3D(-a, -a, -a);

            p[27] = new Point3D(-a, -a, -a);
            p[28] = new Point3D(-a, -a, a);
            p[29] = new Point3D(-a, a, a);

            // 右面
            p[30] = new Point3D(a, a, -a);
            p[31] = new Point3D(a, a, a);
            p[32] = new Point3D(a, -a, a);

            p[33] = new Point3D(a, -a, a);
            p[34] = new Point3D(a, -a, -a);
            p[35] = new Point3D(a, a, -a);

            // 添加顶点
            for (int i = 0; i < 36; i++)
            {
                p[i] += (Vector3D)center;
                mesh.Positions.Add(p[i]);
            }

            // 设置点的顺序
            for (int i = 0; i < 36; )
            {
                CreateTriangleIndices(ref mesh,i,i+1,i+2);
                i = i + 3;
            }
            
            return mesh;
        }
    }
}
