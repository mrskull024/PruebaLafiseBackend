namespace PruebaLafise.API.Models
{
    public class GenericResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }

    public class GenericResponse : GenericResponse<object>
    {
    }
}
