using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;
using Cheval.DataStructure;
using Cheval.Models;
using Cheval.Models.Shapes;
using static System.Single;
using static Cheval.DataStructure.ChevalTuple;
using Group = Cheval.Models.Shapes.Group;

namespace Cheval.Services
{
    public class ObjService
    {
        public int Ignored{ get; set; }
        public  List<ChevalTuple> Vertices = new List<ChevalTuple>{new ChevalTuple(0,0,0, 0)};
        public List<ChevalTuple> Normals = new List<ChevalTuple> { new ChevalTuple(0, 0, 0, 0) };
        public Dictionary<string,Group> Groups { get; set; } = new Dictionary<string, Group>();
        public Group GetAllGroups
        {
            get
            {
                var newGroup = new Group();
                var allGroups = Groups.Values.ToList();
                foreach (var allGroup in allGroups)
                {
                    newGroup.Add(allGroup);
                }

                return newGroup;
            }
        }

        public void ParseString(string data)
        {
            data = Regex.Replace(data, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            data = data.Replace("\r", string.Empty);
            var lines = data.Split('\n').Select(l => l.Trim()).ToList();
            var groupName = "default";
            var bbox = new BoundingBox(Point(-1, -1, -1), Point(1, 1, 1));

            var sx = bbox.Max.X - bbox.Min.X;
            var sy = bbox.Max.Y - bbox.Min.Y;
            var sz = bbox.Max.Z - bbox.Min.Z;
            var scale = MathF.Max(MathF.Max(sx, sy), sz) / 2;

            foreach (var line in lines)
            {
                var args = line.Split(" ");

                switch (args[0])
                {
                    case "v":
                        var x = Parse(args[1]);
                        var y = Parse(args[2]);
                        var z = Parse(args[3]);
                        x = (x - (bbox.Min.X + sx / 2)) / scale;
                        y = (y - (bbox.Min.Y + sy / 2)) / scale;
                        z = (z - (bbox.Min.Z + sz / 2)) / scale;
                        Vertices.Add(Point(x,y,z));
                        break;
                    case "f":
                    {
                        var arguments = args.ToList();
                        arguments.RemoveAt(0);
                        var vertces = new List<int>();
                        var normals = new List<int>();
                        foreach (var vert in arguments)
                        {
                            var verts=vert.Split('/');
                            vertces.Add(int.Parse(verts[0]));
                            if (verts.Length == 3)
                            {
                                normals.Add(int.Parse(verts[2]));
                            }
                        }
                        
                        
                        var triangles = FanTriangulation(vertces, normals);
                        AddToGroup(groupName, triangles);
                        break;
                    }
                    case "g":
                    {
                        groupName = args[1];
                        break;
                    }
                    case "vn":
                    {
                        Normals.Add(Point(Parse(args[1]), Parse(args[2]), Parse(args[3])));
                            break;
                    }
                    default:
                        Ignored++;
                        break;
                }
            }
        }

        private List<Shape> FanTriangulation(List<int> vertices, List<int> normals)
        {
            var triangles = new List<Shape>();
            //var bbox = new BoundingBox(ChevalTuple.Point(-1, -1, -1), ChevalTuple.Point(1, 1, 1));

            //var sx = bbox.Max.X - bbox.Min.X;
            //var sy = bbox.Max.Y - bbox.Min.Y;
            //var sz = bbox.Max.Z - bbox.Min.Z;

            //var scale = MathF.Max(MathF.Max(sx, sy), sz) / 2;

            //foreach (var v in Vertices)
            //{
            //    v.X = (v.X - (bbox.Min.X + sx / 2)) / scale;
            //    v.Y = (v.Y - (bbox.Min.Y + sy / 2)) / scale;
            //    v.Z = (v.Z - (bbox.Min.Z + sz / 2)) / scale;
            //}

            for (var i = 1; i < vertices.Count-1; i++)
            {
                if (normals.Count == 0)
                {
                    triangles.Add(new Triangle(
                        Vertices[vertices[0]],
                        Vertices[vertices[i]],
                        Vertices[vertices[i + 1]]));
                }
                else
                {
                    triangles.Add(new SmoothTriangle(
                        Vertices[vertices[0]],
                        Vertices[vertices[i]],
                        Vertices[vertices[i + 1]],
                        Normals[normals[0]],
                        Normals[normals[i]],
                        Normals[normals[i + 1]]
                        ));
                }

            }

            return triangles;
        }
        private void AddToGroup(string groupName, List<Shape> triangles)
        {
            if (!Groups.ContainsKey(groupName))
            {
                var group = new Group();
                group.AddRange(triangles);
                
                Groups.Add(groupName, group);
            }
            else
            {
                Groups[groupName].AddRange(triangles);
            }
        }
    }
}
