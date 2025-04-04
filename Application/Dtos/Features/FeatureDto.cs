namespace Application.Dtos.Features;

public class FeatureDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Priority { get; set; }
    public List<FeatureDetailDto> Details { get; set; } = [];
}