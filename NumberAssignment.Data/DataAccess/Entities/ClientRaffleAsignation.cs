using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("ClientRaffleAsignation")]
    public class ClientRaffleAsignation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AssignationId { get; set; }
        public int AssignedNumber { get; set; }

        //[ForeignKey("ClientId")]
        public int ClientId { get; set; }
        
        //[ForeignKey("RaffleId")]
        public int RaffleId { get; set; }
        
        //[ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
