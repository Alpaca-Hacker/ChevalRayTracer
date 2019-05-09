using Cheval.Models;

namespace Cheval.Integrators
{
    public interface IIntegrator
    {
        ChevalColour ColourAt(Ray ray, int remaining, Scene scene);
    }
}
