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
namespace mumu_3DDemo
{
    /// <summary>
    /// Interaction logic for PerspectiveByMatrixCamera.xaml
    /// </summary>
    public partial class PerspectiveByMatrixCamera : Page
    {
        public PerspectiveByMatrixCamera()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            /*
                        // 透视投影的若干参数
                        Vector3D LookDirection = new Vector3D(0,0,-1);
                        Vector3D Position = new Vector3D(-2.5, 1,6);
                        Vector3D UpDirection = new Vector3D(0, 1, 0);
                        double FieldOfView= 90;
                        // 透视投影这两个值没有设置，取默认值
                        double zFar = Double.PositiveInfinity;
                        double zNear = 0.125;


                        // 计算M坐标系下的三个轴单位向量 Mx,My,Mz
                        Vector3D Mz = -LookDirection;
                        Mz.Normalize();
                        Vector3D Mx = Vector3D.CrossProduct(UpDirection, Mz);
                        Mx.Normalize();
                        Vector3D My = Vector3D.CrossProduct(Mz, Mx);

                        // 计算平移矩阵的dx,dy,dz
                        double dx = -Vector3D.DotProduct(Position, Mx);
                        double dy = -Vector3D.DotProduct(Position, My);
                        double dz = -Vector3D.DotProduct(Position, Mz);

                        // 构建观察矩阵
                        Matrix3D viewmatrix = new Matrix3D(Mx.X,My.X,Mz.X,0,
                                                           Mx.Y,My.Y,Mz.Y,0,
                                                           Mx.Z,My.Z,Mz.Z,0,
                                                           dx, dy, dz,1);
                        cam.ViewMatrix = viewmatrix;


                        // 计算投影矩阵的Sx,Sy,Sz,dz
                        double FieldOfViewRadian = FieldOfView / 180 * Math.PI;
                        double Sx = 1 / Math.Tan(0.5 * FieldOfViewRadian);
                        double Sy = Sx * viewport.Width / viewport.Height;
                        double Sz = -1;
                        double Dz = Sz * zNear;

                        Matrix3D projectionmatrix = new Matrix3D(Sx,0,0,0,
                                                                0,Sy,0,0,
                                                                0,0,Sz,-1,
                                                                0, 0, Dz, 1);
                        cam.ProjectionMatrix = projectionmatrix;
             * 
             * 
             * 
             * <OrthographicCamera x:Name="camera" Position="-2 1.5 4"
                                           LookDirection="2 -1 -4" UpDirection="0 1 0" Width="5"/>

             */
            

            // 正射投影的若干参数
            Vector3D LookDirection = new Vector3D(2, -1, -4);
            Vector3D Position = new Vector3D(-2, 1.5, 4);
            Vector3D UpDirection = new Vector3D(0, 1, 0);
            double Width = 5;
            // 正射投影这两个值没有设置，取默认值
            // 为了避免运算失败，没有将zFar取Double.PositiveInfinity，而是取一个相对大的数值
            double zFar = 1e10;
            double zNear = 0.125;


            // 计算M坐标系下的三个轴单位向量 Mx,My,Mz
            Vector3D Mz = -LookDirection;
            Mz.Normalize();
            Vector3D Mx = Vector3D.CrossProduct(UpDirection, Mz);
            Mx.Normalize();
            Vector3D My = Vector3D.CrossProduct(Mz, Mx);

            // 计算平移矩阵的dx,dy,dz
            double dx = -Vector3D.DotProduct(Position, Mx);
            double dy = -Vector3D.DotProduct(Position, My);
            double dz = -Vector3D.DotProduct(Position, Mz);

            // 构建观察矩阵
            Matrix3D viewmatrix = new Matrix3D(Mx.X, My.X, Mz.X, 0,
                                               Mx.Y, My.Y, Mz.Y, 0,
                                               Mx.Z, My.Z, Mz.Z, 0,
                                               dx, dy, dz, 1);
            cam.ViewMatrix = viewmatrix;


            // 计算正射投影矩阵的Sx,Sy,Sz,dz
            
            double Sx = 2 / Width;
            double Sy = Sx * viewport.Width / viewport.Height;
            double Sz = 1/(zNear-zFar);
            double Dz = zNear / (zNear - zFar);

            Matrix3D projectionmatrix = new Matrix3D(Sx, 0, 0, 0,
                                                    0, Sy, 0, 0,
                                                    0, 0, Sz, 0,
                                                    0, 0, Dz, 1);
            cam.ProjectionMatrix = projectionmatrix;
        }
    }
}
