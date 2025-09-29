public class RequestModel
{
    public string RequestId { get; set; }
    public RequestTypeEnum RequestType { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string BranchId { get; set; }
    public string UserId { get; set; }
    public RequestTypeEnum RequestState { get; set; }
}