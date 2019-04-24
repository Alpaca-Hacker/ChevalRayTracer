
using Cheval.Models;

namespace Cheval.Samplers
{
    public interface ISampler
    {
        ChevalColour Sample(int x, int y);
    }
}
