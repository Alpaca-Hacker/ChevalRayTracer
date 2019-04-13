using System;
using Cheval.Models;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Helper.Transform;

namespace ChevalTests.ModelTests
{
    public class CameraTests
    {
        /*Scenario: Constructing a camera
           Given hsize ← 160
           And vsize ← 120
           And field_of_view ← π/2
           When c ← camera(hsize, vsize, field_of_view)
           Then c.hsize = 160
           And c.vsize = 120
           And c.field_of_view = π/2
           And c.transform = identity_matrix
           */
        [Test]
        public void Camera_works_as_expected()
        {
            //Assign
            var hsize = 160;
            var vsize = 120;
            var fov = Math.PI / 2;
            //Act
            var cam = new Camera(hsize, vsize, fov);
            //Assert
            cam.HSize.Should().Be(160);
            cam.VSize.Should().Be(120);
            cam.FOV.Should().Be(Math.PI / 2);
        }
        /*
         * Scenario: The pixel size for a horizontal canvas
           Given c ← camera(200, 125, π/2)
           Then c.pixel_size = 0.01
           */
        [Test]
        public void Pixel_size_for_horizontal_canvas()
        {
            //Assign
            var cam = new Camera(200,125, Math.PI / 2);
            //Assert
            Math.Round(cam.PixelSize,5).Should().Be(0.01);
        }
        /*
           Scenario: The pixel size for a vertical canvas
           Given c ← camera(125, 200, π/2)
           Then c.pixel_size = 0.01
         */
        [Test]
        public void Pixel_size_for_vertical_canvas()
        {
            //Assign
            var cam = new Camera(125, 200, Math.PI / 2);
            //Assert
            Math.Round(cam.PixelSize, 5).Should().Be(0.01);
        }

        /*
         * Scenario: Constructing a ray through the center of the canvas
           Given c ← camera(201, 101, π/2)
           When r ← ray_for_pixel(c, 100, 50)
           Then r.origin = point(0, 0, 0)
           And r.direction = vector(0, 0, -1)
           */
        [Test]
        public void Constructing_ray_through_centre_of_canvas()
        {
            //Assign
            var cam = new Camera(201,101,Math.PI /2);
            //Act
            var result = cam.RayForPixel(100, 50);
            //Assert
            result.Origin.Should().BeEquivalentTo(Point(0,0,0));
            result.Direction.Should().BeEquivalentTo(Vector(0,0,-1));
        }
        /*
           Scenario: Constructing a ray through a corner of the canvas
           Given c ← camera(201, 101, π/2)
           When r ← ray_for_pixel(c, 0, 0)
           Then r.origin = point(0, 0, 0)
           And r.direction = vector(0.66519, 0.33259, -0.66851)
           */
        [Test]
        public void Constructing_ray_through_corner_of_canvas()
        {
            //Assign
            var cam = new Camera(201, 101, Math.PI / 2);
            //Act
            var result = cam.RayForPixel(0, 0);
            //Assert
            result.Origin.Should().BeEquivalentTo(Point(0, 0, 0));
            result.Direction.Should().BeEquivalentTo(Vector(0.66519, 0.33259, -0.66851));
        }
        /*
           Scenario: Constructing a ray when the camera is transformed
           Given c ← camera(201, 101, π/2)
           When c.transform ← rotation_y(π/4) * translation(0, -2, 5)
           And r ← ray_for_pixel(c, 100, 50)
           Then r.origin = point(0, 2, -5)
           And r.direction = vector(√2/2, 0, -√2/2)
         */
        [Test]
        public void Constructing_ray_when_camera_is_transformed()
        {
            //Assign
            var cam = new Camera(201, 101, Math.PI / 2);
            cam.Transform = RotationY(Math.PI / 4) * Translation(0, -2, 5);
            //Act
            var result = cam.RayForPixel(100, 50);
            //Assert
            result.Origin.Should().BeEquivalentTo(Point(0, 2, -5));
            result.Direction.Should().BeEquivalentTo(Vector(Math.Sqrt(2)/2, 0, -Math.Sqrt(2) / 2));
        }
        /*
         * Scenario: Rendering a world with a camera
           Given w ← default_world()
           And c ← camera(11, 11, π/2)
           And from ← point(0, 0, -5)
           And to ← point(0, 0, 0)
           And up ← vector(0, 1, 0)
           And c.transform ← view_transform(from, to, up)
           When image ← render(c, w)
           Then pixel_at(image, 5, 5) = color(0.38066, 0.47583, 0.2855)
         */
        [Test]
        public void Rendering_scene_test()
        {
            //Assign
            var scene = Scene.Default();
            var cam = new Camera(11,11,Math.PI/2);
            var from = Point(0, 0, -5);
            var to = Point(0, 0, 0);
            var up = Vector(0, 1, 0);
            cam.Transform = ViewTransform(from, to, up);
            //Act
            Canvas image = cam.Render(scene);
            var result = image.GetPixel(5, 5);
            var expected = new ChevalColour(0.38066, 0.47583, 0.2855);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }

    }
}
