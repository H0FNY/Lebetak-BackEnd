namespace Lebetak.DTOs.Worker
{
    public class UpdateWorkerInfoDTO
    {
            public string? Description { get; set; }
            public int? ExperienceYears { get; set; }
            public int? HourlyPrice { get; set; }
            public int? CategoryId { get; set; }
            public List<int>? Skills { get; set; }
    }
}
