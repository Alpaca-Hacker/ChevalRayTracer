
using Cheval.Models;

namespace Cheval.Scenes
{
    public interface ISceneBuilder
    {
        World Build(float size =1);
    }
}
