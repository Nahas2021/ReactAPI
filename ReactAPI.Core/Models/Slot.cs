using System.ComponentModel.DataAnnotations;

namespace ReactAPI.Core.Models
{
    public class Slot
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SlotNumber { get; set; }

        public bool IsAvailable { get; set; } = true; // By default, the slot is available
    }
}
