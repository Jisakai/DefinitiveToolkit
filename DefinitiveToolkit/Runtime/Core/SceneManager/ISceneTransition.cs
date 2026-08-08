using System.Collections;

namespace DTK.Core.SceneManagement
{
    public interface ISceneTransition
    {
        IEnumerator PlayOut();
        IEnumerator PlayIn();
    }
}