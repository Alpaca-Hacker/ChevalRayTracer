using Cheval.Models.Shapes;
using Cheval.Services;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;

namespace ChevalTests
{
    public class ObjServiceTests
    {
        /*
         * Scenario: Ignoring unrecognized lines
           Given gibberish ← a file containing:
           """
           There was a young lady named Bright
           who traveled much faster than light.
           She set out one day
           in a relative way,
           and came back the previous night.
           """
           When parser ← parse_obj_file(gibberish)
           Then parser should have ignored 5 lines
         */
        [Test]
        public void Ignore_unrecognized_lines()
        {
            //Assign
            var parser = new ObjService();
            var gibberish = @"
There was a young lady named Bright
who traveled much faster than light.
She set out one day
in a relative way,
and came back the previous night.";
            //Act
            parser.ParseString(gibberish);
            //Assert
            parser.Ignored.Should().Be(5);
        }

        /*
         * Scenario: Vertex records
           Given file ← a file containing:
           """
           v -1 1 0
           v -1.0000 0.5000 0.0000
           v 1 0 0
           v 1 1 0
           """
           When parser ← parse_obj_file(file)
           Then parser.vertices[1] = point(-1, 1, 0)
           And parser.vertices[2] = point(-1, 0.5, 0)
           And parser.vertices[3] = point(1, 0, 0)
           And parser.vertices[4] = point(1, 1, 0)
         */
        [Test]
        public void Vertex_recorded_correctly()
        {
            //Assign
            var parser = new ObjService();
            var data = @"
v -1 1 0
v -1.0000 0.5000 0.0000
v 1 0 0
v 1 1 0
";
            //Act
            parser.ParseString(data);
            //Assert
            parser.Vertices[1].Should().BeEquivalentTo(Point(-1, 1, 0));
            parser.Vertices[2].Should().BeEquivalentTo(Point(-1, 0.5f, 0));
            parser.Vertices[3].Should().BeEquivalentTo(Point(1, 0, 0));
            parser.Vertices[4].Should().BeEquivalentTo(Point(1, 1, 0));
        }

        /*
         * Scenario: Parsing triangle faces
           Given file ← a file containing:
           """
           v -1 1 0
           v -1 0 0
           v 1 0 0
           v 1 1 0
           f 1 2 3
           f 1 3 4
           """
           When parser ← parse_obj_file(file)
           And g ← parser.default_group
           And t1 ← first child of g
           And t2 ← second child of g
           Then t1.p1 = parser.vertices[1]
           And t1.p2 = parser.vertices[2]
           And t1.p3 = parser.vertices[3]
           And t2.p1 = parser.vertices[1]
           And t2.p2 = parser.vertices[3]
           And t2.p3 = parser.vertices[4]
         */
        [Test]
        public void Parsing_triangle_faces()
        {
            //Assign
            var parser = new ObjService();
            var data = @"
v -1 1 0
v -1 0 0
v 1 0 0
v 1 1 0
f 1 2 3
f 1 3 4
";
            //Act
            parser.ParseString(data);

            var group = parser.Groups["default"];
            var t1 = (Triangle) group[0];
            var t2 = (Triangle) group[1];
            //Assert
            t1.Point1.Should().BeEquivalentTo(parser.Vertices[1]);
            t1.Point2.Should().BeEquivalentTo(parser.Vertices[2]);
            t1.Point3.Should().BeEquivalentTo(parser.Vertices[3]);

            t2.Point1.Should().BeEquivalentTo(parser.Vertices[1]);
            t2.Point2.Should().BeEquivalentTo(parser.Vertices[3]);
            t2.Point3.Should().BeEquivalentTo(parser.Vertices[4]);

        }

        /*
         *Scenario: Triangulating polygons
           Given file ← a file containing:
           """
           v -1 1 0
           v -1 0 0
           v 1 0 0
           v 1 1 0
           v 0 2 0
           f 1 2 3 4 5
           """
           When parser ← parse_obj_file(file)
           And g ← parser.default_group
           And t1 ← first child of g
           And t2 ← second child of g
           And t3 ← third child of g
           Then t1.p1 = parser.vertices[1]
           And t1.p2 = parser.vertices[2]
           And t1.p3 = parser.vertices[3]
           And t2.p1 = parser.vertices[1]
           And t2.p2 = parser.vertices[3]
           And t2.p3 = parser.vertices[4]
           And t3.p1 = parser.vertices[1]
           And t3.p2 = parser.vertices[4]
           And t3.p3 = parser.vertices[5]
         */

        [Test]
        public void Triangulating_triangles()
        {
            //Assign
            var parser = new ObjService();
            var data = @"
v -1 1 0
v -1 0 0
v 1 0 0
v 1 1 0
v 0 2 0
f 1 2 3 4 5
";
            //Act
            parser.ParseString(data);

            var group = parser.Groups["default"];
            var t1 = (Triangle) group[0];
            var t2 = (Triangle) group[1];
            var t3 = (Triangle) group[2];

            //Assert
            t1.Point1.Should().BeEquivalentTo(parser.Vertices[1]);
            t1.Point2.Should().BeEquivalentTo(parser.Vertices[2]);
            t1.Point3.Should().BeEquivalentTo(parser.Vertices[3]);

            t2.Point1.Should().BeEquivalentTo(parser.Vertices[1]);
            t2.Point2.Should().BeEquivalentTo(parser.Vertices[3]);
            t2.Point3.Should().BeEquivalentTo(parser.Vertices[4]);

            t3.Point1.Should().BeEquivalentTo(parser.Vertices[1]);
            t3.Point2.Should().BeEquivalentTo(parser.Vertices[4]);
            t3.Point3.Should().BeEquivalentTo(parser.Vertices[5]);

        }

        /*
         * Scenario: Triangles in groups
           Given file ← the file "triangles.obj"
           When parser ← parse_obj_file(file)
           And g1 ← "FirstGroup" from parser
           And g2 ← "SecondGroup" from parser
           And t1 ← first child of g1
           And t2 ← first child of g2
           Then t1.p1 = parser.vertices[1]
           And t1.p2 = parser.vertices[2]
           And t1.p3 = parser.vertices[3]
           And t2.p1 = parser.vertices[1]
           And t2.p2 = parser.vertices[3]
           And t2.p3 = parser.vertices[4]
         */
        [Test]
        public void Adding_groups_from_obj()
        {
            //Assign
            var parser = new ObjService();
            var data = @"
v -1 1 0
v -1 0 0
v 1 0 0
v 1 1 0

g FirstGroup
f 1 2 3
g SecondGroup
f 1 3 4
";
            //Act
            parser.ParseString(data);

            var group1 = parser.Groups["FirstGroup"];
            var group2 = parser.Groups["SecondGroup"];
            var t1 = (Triangle) group1[0];
            var t2 = (Triangle) group2[0];

            //Assert
            t1.Point1.Should().BeEquivalentTo(parser.Vertices[1]);
            t1.Point2.Should().BeEquivalentTo(parser.Vertices[2]);
            t1.Point3.Should().BeEquivalentTo(parser.Vertices[3]);

            t2.Point1.Should().BeEquivalentTo(parser.Vertices[1]);
            t2.Point2.Should().BeEquivalentTo(parser.Vertices[3]);
            t2.Point3.Should().BeEquivalentTo(parser.Vertices[4]);

        }

        public void Getting_all_groups_from_obj()
        {
            //Assign
            var parser = new ObjService();
            var data = @"
v -1 1 0
v -1 0 0
v 1 0 0
v 1 1 0

g FirstGroup
f 1 2 3
g SecondGroup
f 1 3 4
";
            //Act
            parser.ParseString(data);
            var result = parser.GetAllGroups;
            //Assert
            result.Should().HaveCount(2);

        }
    }
}
