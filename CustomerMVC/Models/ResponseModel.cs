namespace CustomerMgt.MVC.Models
{
    public class ResponseModel<T> 
    {

        public bool RequestSuccessful { get; set; }
        public T? ResponseData { get; set; }
        public string? Message { get; set; }
        public string? ResponseCode { get; set; }


    }

    public class Page<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalSize { get; set; }
        public List<T>? Items { get; set; }
    }

}

