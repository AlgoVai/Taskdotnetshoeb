using System.ComponentModel.DataAnnotations;

namespace TeamsCrudOperation.Models
{
    public class TeamInfo
    {
        [Key]
        public long TeamId { get; set; }
        [Required]
        public string TeamName { get; set; }
        public string Description { get; set; }
        public int ApprovedByManager { get; set; }
        public int ApprovedByDirector { get; set; }


    }
}
