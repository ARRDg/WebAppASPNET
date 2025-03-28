using System.ComponentModel.DataAnnotations;

namespace WebAppASPNET.Data
{
    public class Friendship
    {
        [Key]
        public int Id {  get; set; }
        public int RequesterId { get; set; }
        public int ReceiverId { get; set; }
        public FriendshipStatus Status { get; set; }
        public DateTime CreatesAt { get; set; }

        public enum FriendshipStatus
        {
            Pending,
            Accepted,
            Declined,
            Blocked
        }
    }
}
