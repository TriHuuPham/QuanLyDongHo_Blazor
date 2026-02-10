using System.ComponentModel.DataAnnotations;

namespace DongHoBlazorApp.Model.Entities
{
    public class PhanLoaiModel
    {
        [Key]
        public int MaPL { get; set; }
        public string TenPL { get; set; }
        public bool TrangThai { get; set; }
    }
}
