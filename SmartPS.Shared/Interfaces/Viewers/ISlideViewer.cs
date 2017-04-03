using SmartPS.Shared.Interfaces.Presentation;

namespace SmartPS.Shared.Interfaces.Viewers
{
    public interface ISlideViewer
    {
        void Load(IPresentation presentation);

        void GoToSlide(int number);
        void PreviousSlide();
        void NextSlide();
        
    }
}