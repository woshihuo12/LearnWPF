using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.Windows.Media.Media3D;
using _3DTools;

namespace mumu_3DDemo
{
    /// <summary>
    /// Interaction logic for Sphere.xaml
    /// </summary>
    public partial class Sphere : Page
    {
        public Sphere()
        {
            InitializeComponent();

            // 创建Viewport3D
            Viewport3D viewport = new Viewport3D();
            Content = viewport;

            // 创建第一个球，将绘制三角网
            ScreenSpaceLines3D line3D = new ScreenSpaceLines3D();
            line3D.Color = Color.FromRgb(0, 0, 0);
            line3D.Thickness = 1;

            MeshGeometry3D mesh =
                GenerateSphere(new Point3D(0, 0, 1.2), 1.2, 36, 18, ref line3D);
            mesh.Freeze();
           
            GeometryModel3D geomod = new GeometryModel3D();
            geomod.Geometry = mesh;
            geomod.Material = new DiffuseMaterial(Brushes.Cyan);
            geomod.BackMaterial = new DiffuseMaterial(Brushes.Red);

            
            ModelVisual3D modvis = new ModelVisual3D();
            modvis.Content = geomod;
            viewport.Children.Add(modvis);
            viewport.Children.Add(line3D);

            // 创建第二个球 不绘制三角网

            ScreenSpaceLines3D line3D2 = null;           
            MeshGeometry3D mesh2 =
               GenerateSphere(new Point3D(0, 0, -1), 0.8, 36, 18, ref line3D2);
            mesh2.Freeze();

            GeometryModel3D geomod2 = new GeometryModel3D();
            geomod2.Geometry = mesh2;
            geomod2.Material = new DiffuseMaterial(Brushes.Cyan);
            geomod2.BackMaterial = new DiffuseMaterial(Brushes.Red);
            ModelVisual3D modvis2 = new ModelVisual3D();
            modvis2.Content = geomod2;
            viewport.Children.Add(modvis2);
            

            // 创建坐标系
/*
            ScreenSpaceLines3D coordinatelineX = new ScreenSpaceLines3D();
            coordinatelineX.Color = Colors.Red;
            coordinatelineX.Thickness = 2;
            coordinatelineX.Points.Add(new Point3D(-3, 0, 0));
            coordinatelineX.Points.Add(new Point3D(3, 0, 0));

            ScreenSpaceLines3D coordinatelineY = new ScreenSpaceLines3D();
            coordinatelineY.Color = Colors.Green;
            coordinatelineY.Thickness = 2;
            coordinatelineY.Points.Add(new Point3D(0, -3, 0));
            coordinatelineY.Points.Add(new Point3D(0, 3, 0));

            ScreenSpaceLines3D coordinatelineZ = new ScreenSpaceLines3D();
            coordinatelineZ.Color = Colors.Blue;
            coordinatelineZ.Thickness = 2;
            coordinatelineZ.Points.Add(new Point3D(0, 0, -3));
            coordinatelineZ.Points.Add(new Point3D(0, 0, 3));
 */ 
            //viewport.Children.Add(coordinatelineX);
            //viewport.Children.Add(coordinatelineY);
            //viewport.Children.Add(coordinatelineZ);


            // 创建光源
            Model3DGroup modgrp = new Model3DGroup();
            modgrp.Children.Add(new AmbientLight(Color.FromRgb(128, 128, 128)));
            //modgrp.Children.Add(
            //    new DirectionalLight(Color.FromRgb(128, 128, 128),
            //                         new Vector3D(2, -3, -1)));
            modgrp.Children.Add(
                  new DirectionalLight(Color.FromRgb(128, 128, 128),
                         new Vector3D(0, 0, -1)));

            modvis = new ModelVisual3D();
            modvis.Content = modgrp;
            viewport.Children.Add(modvis);

            // 创建照相机
            //PerspectiveCamera cam = new PerspectiveCamera(new Point3D(0, 0, 8),
            //                new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), 45);
            PerspectiveCamera cam = new PerspectiveCamera(new Point3D(4, 5, 4),
                            new Vector3D(-4, -5, -4), new Vector3D(0, 1, 0), 45);
            viewport.Camera = cam;



        }

        MeshGeometry3D GenerateSphere(Point3D center, double radius,
                                      int slices, int stacks, ref ScreenSpaceLines3D line3D)
        {
            // 如果line3D非空，则需要绘制构建的三角网
            bool isWire = false;
            if (line3D != null)
                isWire = true;

            Point3DCollection points = new Point3DCollection();
            MeshGeometry3D mesh = new MeshGeometry3D();

            // 计算出来的点，然后加入到MeshGeometry3D的Positions属性
            for (int stack = 0; stack <= stacks; stack++)
            {
                double phi = Math.PI / 2 - stack * Math.PI / stacks;
                double y = radius * Math.Sin(phi);
                double scale = -radius * Math.Cos(phi);

                for (int slice = 0; slice <= slices; slice++)
                {
                    double theta = slice * 2 * Math.PI / slices;
                    double x = scale * Math.Sin(theta);
                    double z = scale * Math.Cos(theta);

                    Vector3D normal = new Vector3D(x, y, z);
                    mesh.Normals.Add(normal);
                    mesh.Positions.Add(normal + center);

                    Point3D point3D = normal + center;
                    points.Add(point3D);

                    mesh.TextureCoordinates.Add(
                            new Point((double)slice / slices,
                                      (double)stack / stacks));
                }
            }

            // 将点的顺序加入到MeshGeometry3D的TriangleIndices属性
            for (int stack = 0; stack < stacks; stack++)
                for (int slice = 0; slice < slices; slice++)
                {
                    int n = slices + 1; 

                    if (stack != 0)
                    {
                        mesh.TriangleIndices.Add((stack + 0) * n + slice);
                        mesh.TriangleIndices.Add((stack + 1) * n + slice);
                        mesh.TriangleIndices.Add((stack + 0) * n + slice + 1);
                        if (isWire)
                        {
                            line3D.Points.Add(points[(stack + 0) * n + slice]);
                            line3D.Points.Add(points[(stack + 1) * n + slice]);
                            line3D.Points.Add(points[(stack + 1) * n + slice]);
                            line3D.Points.Add(points[(stack + 0) * n + slice + 1]);
                            line3D.Points.Add(points[(stack + 0) * n + slice + 1]);
                            line3D.Points.Add(points[(stack + 0) * n + slice]);
                        }
                    }
                    if (stack != stacks - 1)
                    {
                        mesh.TriangleIndices.Add((stack + 0) * n + slice + 1);
                        mesh.TriangleIndices.Add((stack + 1) * n + slice);
                        mesh.TriangleIndices.Add((stack + 1) * n + slice + 1);
                        if (isWire)
                        {
                            line3D.Points.Add(points[(stack + 0) * n + slice + 1]);
                            line3D.Points.Add(points[(stack + 1) * n + slice]);
                            line3D.Points.Add(points[(stack + 1) * n + slice]);
                            line3D.Points.Add(points[(stack + 1) * n + slice + 1]);
                            line3D.Points.Add(points[(stack + 1) * n + slice + 1]);
                            line3D.Points.Add(points[(stack + 0) * n + slice + 1]);
                        }
                    }
                }
            return mesh;
        }
    }
}
