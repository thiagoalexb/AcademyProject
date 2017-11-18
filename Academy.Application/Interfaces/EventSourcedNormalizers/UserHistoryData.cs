namespace Academy.Application.EventSourcedNormalizers
{
    public class IUserHistoryData
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }

        public string Action { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}
