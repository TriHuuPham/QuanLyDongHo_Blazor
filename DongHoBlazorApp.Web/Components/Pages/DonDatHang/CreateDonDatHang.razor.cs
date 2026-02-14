using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DongHoBlazorApp.Web.Components.Pages.DonDatHang
{
    public partial class CreateDonDatHang
    {
        [Inject]
        public HttpClient ApiClient { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private DonDatHangModel Order { get; set; } = new()
        {
            NgayDat = DateTime.Now,
            TinhTrang = "Đang xử lý",
            NgayGiao = DateTime.Now.AddDays(3),
            DaThanhToan = false,
            PhuongThucThanhToan = "Tiền mặt",
            PhuongThucGiaoHang = "Giao hàng nhanh",
            TrangThai = true
        };

        private KhachHangModel? SelectedCustomer { get; set; }
        private string CustomerSearchTerm { get; set; } = string.Empty;
        private List<KhachHangModel> CustomerSearchResults { get; set; } = new();
        private bool ShowCustomerResults { get; set; } = false;

        private List<OrderDetailRow> OrderItems { get; set; } = new() { new OrderDetailRow() };
        private List<DongHoModel> ProductSearchResults { get; set; } = new();
        private int ActiveRowIndex { get; set; } = -1;

        private decimal TotalAmount => OrderItems.Sum(i => i.Quantity * i.UnitPrice);
        private int TotalQuantity => OrderItems.Sum(i => i.Quantity);

        private async Task HandleCustomerSearch(ChangeEventArgs e)
        {
            CustomerSearchTerm = e.Value?.ToString() ?? string.Empty;
            if (CustomerSearchTerm.Length >= 2)
            {
                var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/KhachHang/search?searchTerm={Uri.EscapeDataString(CustomerSearchTerm)}");
                if (res != null && res.Success)
                {
                    CustomerSearchResults = JsonConvert.DeserializeObject<List<KhachHangModel>>(res.Data.ToString()) ?? new();
                    ShowCustomerResults = CustomerSearchResults.Any();
                }
            }
            else
            {
                CustomerSearchResults.Clear();
                ShowCustomerResults = false;
            }
        }

        private void SelectCustomer(KhachHangModel customer)
        {
            SelectedCustomer = customer;
            Order.MaKH = customer.MaKH;
            CustomerSearchTerm = customer.TenKH;
            ShowCustomerResults = false;
        }

        private async Task HandleProductSearch(int index, ChangeEventArgs e)
        {
            ActiveRowIndex = index;
            var term = e.Value?.ToString() ?? string.Empty;
            OrderItems[index].ProductName = term;

            if (term.Length >= 2)
            {
                var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/DongHo/search?searchTerm={Uri.EscapeDataString(term)}");
                if (res != null && res.Success)
                {
                    ProductSearchResults = JsonConvert.DeserializeObject<List<DongHoModel>>(res.Data.ToString()) ?? new();
                }
            }
            else
            {
                ProductSearchResults.Clear();
            }
        }

        private void SelectProduct(int index, DongHoModel product)
        {
            OrderItems[index].ProductId = product.MaDongHo;
            OrderItems[index].ProductName = product.TenDongHo;
            OrderItems[index].UnitPrice = product.GiaBan;
            OrderItems[index].Quantity = 1;
            ProductSearchResults.Clear();
            ActiveRowIndex = -1;
        }

        private void AddRow()
        {
            OrderItems.Add(new OrderDetailRow());
        }

        private void RemoveRow(int index)
        {
            if (OrderItems.Count > 1)
            {
                OrderItems.RemoveAt(index);
            }
            else
            {
                OrderItems[0] = new OrderDetailRow();
            }
        }

        private async Task HandleSubmit()
        {
            if (SelectedCustomer == null)
            {
                Console.WriteLine("Please select a customer.");
                return;
            }

            if (!OrderItems.Any(i => i.ProductId > 0))
            {
                Console.WriteLine("Please add at least one product.");
                return;
            }

            try
            {
                var request = new CreateOrderRequestDTO
                {
                    Order = Order,
                    Details = OrderItems
                        .Where(i => i.ProductId > 0)
                        .Select(i => new ChiTietDonDHModel
                        {
                            MaDongHo = i.ProductId,
                            TenDongHo = i.ProductName,
                            SoLuong = i.Quantity,
                            DonGia = i.UnitPrice
                        }).ToList()
                };

                var res = await ApiClient.PostAsJsonAsync("/api/DonDatHang/complex", request);
                var baseRes = await res.Content.ReadFromJsonAsync<BaseReponseModel>();

                if (baseRes != null && baseRes.Success)
                {
                    int maDonDH = Convert.ToInt32(baseRes.Data);
                    NavigationManager.NavigateTo($"/ChiTietDonHang/{maDonDH}");
                }
                else
                {
                    Console.WriteLine($"Error: {baseRes?.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating order: {ex.Message}");
            }
        }

        public class OrderDetailRow
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public int Quantity { get; set; } = 1;
            public decimal UnitPrice { get; set; }
            public decimal Subtotal => Quantity * UnitPrice;
        }
    }
}
