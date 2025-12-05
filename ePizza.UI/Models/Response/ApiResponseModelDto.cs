namespace ePizza.UI.Models.Response
{
    public class ApiResponseModelDto<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = default!;

        public T Data { get; set; }


        public ApiResponseModelDto(bool success, T data, string message)
        {
            Success = success;
            Data = data;
            Message = message;
        }
    }
}
