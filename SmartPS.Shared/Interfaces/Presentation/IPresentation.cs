using System.Collections.Generic;

namespace SmartPS.Shared.Interfaces.Presentation
{
    public interface IPresentation
    {
        string Title { get; set; }

        IPresentationDetail Detail { get; set; }

        ICollection<ISlide> Slides { get; set; }
        ICollection<IAttachment> Attachments { get; set; }
    }
}