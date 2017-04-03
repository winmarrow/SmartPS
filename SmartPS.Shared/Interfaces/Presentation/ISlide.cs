namespace SmartPS.Shared.Interfaces.Presentation
{
    public interface ISlide
    {
        int Number { get; set; }

        string ImagePath { get; set; }
        string AnatationPath { get; set; }

        string Description { get; set; }
    }
}