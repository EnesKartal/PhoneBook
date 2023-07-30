namespace PhoneBook.Common.Models.Extra.RabbitMQ
{
    public class ReportResponseModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public int PeopleCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
