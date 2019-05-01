using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cheval.DataStructure;

namespace Cheval.Models.Shapes
{
    public class Group : Shape, IList<Shape>

    {
        private readonly List<Shape> _children = new List<Shape>();

        public Shape this[int index]
        {
            get => _children[index];
            set => _children[index] = value;
        }

        protected override List<Intersection> LocalIntersect(Ray localRay)
        {
            var xs = new List<Intersection>();

            var bounds = BoundingBox;
            if (!bounds.Intersect(localRay))
            {
                return xs;
            }
    
            foreach (var shape in _children)
            {
                xs.AddRange(shape.Intersect(localRay));
            }

            return xs.OrderBy(x => x.T).ToList();
        }

        protected override ChevalTuple LocalNormalAt(ChevalTuple localPoint)
        {
            throw new System.NotImplementedException();
        }

        public override BoundingBox Bounds()
        {
            var box = BoundingBox.Empty;
            foreach (var child in _children)
            {
                box += child.ParentSpaceBounds();
            }

            return box;
        }

        public int IndexOf(Shape item) => _children.IndexOf(item);


        public void Insert(int index, Shape item)
        {
            _children.Insert(index,item);
            item.Parent = this;
        }

        public void RemoveAt(int index)
        {
            _children[index].Parent = null; 
            _children.RemoveAt(index);
        }

        public void Add(Shape shape)
        {
            _children.Add(shape);
            shape.Parent = this;
        }

        public void Clear()
        {
            foreach (var child in _children)
            {
                child.Parent = null;
            }
            _children.Clear();
        }

        public bool Contains(Shape item) => _children.Contains(item);

        public void CopyTo(Shape[] array, int arrayIndex)
        {
            _children.CopyTo(array, arrayIndex);
        }

        public bool Remove(Shape item)
        {
            var result = _children.Remove(item);
            if (result)
            {
                item.Parent = null;
            }
            return result;
        }

        public int Count => _children.Count;
        public bool IsReadOnly => false;
        public IEnumerator<Shape> GetEnumerator() => _children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(List<Shape> shapes)
        {
            _children.AddRange(shapes);
            foreach (var shape in shapes)
            {
                shape.Parent = this;
            }
        }
    }
}
