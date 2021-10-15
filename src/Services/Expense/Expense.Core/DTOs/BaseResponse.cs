namespace Expense.Core.DTOs
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Status = true;
        }

        public bool Status { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}
