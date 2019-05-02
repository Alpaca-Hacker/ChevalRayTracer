using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;
using Cheval.DataStructure;
using Cheval.Models.Shapes;
using static System.Single;
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
            foreach (var line in lines)
            {
                var args = line.Split(" ");

                switch (args[0])
                {
                    case "v":
                        Vertices.Add(ChevalTuple.Point(Parse(args[1]), Parse(args[2]), Parse(args[3])));
                        break;
                    case "f":
                    {
                        var verts = args.ToList();
                        verts.RemoveAt(0);
                        var vertcies = verts.Select(int.Parse).ToList();
                        
                        var triangles = FanTriangulation(vertcies);
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
                        Normals.Add(ChevalTuple.Point(Parse(args[1]), Parse(args[2]), Parse(args[3])));
                            break;
                    }
                    default:
                        Ignored++;
                        break;
                }
            }
        }

        private List<Shape> FanTriangulation(List<int> vertices)
        {
            var triangles = new List<Shape>();
            for (var i = 1; i < vertices.Count-1; i++)
            {
                triangles.Add(new Triangle(Vertices[vertices[0]], 
                                            Vertices[vertices[i]], 
                                            Vertices[vertices[i+1]]));

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
