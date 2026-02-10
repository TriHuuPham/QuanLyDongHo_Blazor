namespace DongHoBlazorApp.Model.Models
{
    public class BaseReponseModel
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
