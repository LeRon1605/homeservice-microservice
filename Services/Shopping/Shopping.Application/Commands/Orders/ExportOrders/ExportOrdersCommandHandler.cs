using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using OfficeOpenXml;
using Shopping.Application.Dtos.Orders;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.OrderAggregate.Specifications;

namespace Shopping.Application.Commands.Orders.ExportOrders;

public class ExportOrdersCommandHandler : ICommandHandler<ExportOrdersCommand, byte[]>
{
    private readonly IReadOnlyRepository<Order> _orderRepository;
    private readonly IMapper _mapper;
    
    public ExportOrdersCommandHandler(IReadOnlyRepository<Order> orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    
    public async Task<byte[]> Handle(ExportOrdersCommand request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.FindListAsync(new ListOrderDetailSpecification());
        var orderDetailDtos = _mapper.Map<List<OrderDetailsDto>>(orders);

        return await GenerateExcelFileAsync(orderDetailDtos);
    }

    private async Task<byte[]> GenerateExcelFileAsync(List<OrderDetailsDto> orders)
    {
        using var excelPackage = new ExcelPackage();
        
        var worksheet = excelPackage.Workbook.Worksheets.Add("Orders");
        var columnNames = new string[]
        {
            "Order Id", "OrderNo", "Placed Date", "Customer Name", "Customer Contact Name", "Customer Email", "Customer  Phone", "Customer Address", "Customer City", "Customer State", "Customer Postal Code",
            "Product Id", "Product Name", "Product Unit", "Product Color", "Product Quantity", "Product Cost", "Product Total Cost",
            "TotalCost", "Status"
        };
        
        for (var i = 0; i < columnNames.Length; i++)
        {
            worksheet.Cells[1, i + 1].Value = columnNames[i];
        }
        
        var orderDateColumn = worksheet.Column(3);
        orderDateColumn.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";

        var row = 2;
        foreach (var order in orders)
        {
            var startRow = row;
            
            worksheet.Cells[row, 1].Value = order.Id;
            worksheet.Cells[row, 2].Value = order.OrderNo;
            worksheet.Cells[row, 3].Value = order.PlacedDate;
            worksheet.Cells[row, 4].Value = order.CustomerName;
            worksheet.Cells[row, 5].Value = order.ContactName;
            worksheet.Cells[row, 6].Value = order.Email;
            worksheet.Cells[row, 7].Value = order.Phone;
            worksheet.Cells[row, 8].Value = order.Address;
            worksheet.Cells[row, 9].Value = order.City;
            worksheet.Cells[row, 10].Value = order.State;
            worksheet.Cells[row, 11].Value = order.PostalCode;
            worksheet.Cells[row, 19].Value = order.Items.Sum(x => x.TotalCost);
            worksheet.Cells[row, 20].Value = order.OrderStatus;
            
            foreach (var orderLine in order.Items)
            {
                worksheet.Cells[row, 12].Value = orderLine.Product.Id;
                worksheet.Cells[row, 13].Value = orderLine.Product.Name;
                worksheet.Cells[row, 14].Value = orderLine.ProductUnit.Name;
                worksheet.Cells[row, 15].Value = orderLine.Color;
                worksheet.Cells[row, 16].Value = orderLine.Quantity;
                worksheet.Cells[row, 17].Value = orderLine.Cost;
                worksheet.Cells[row, 18].Value = orderLine.TotalCost;
                row++;
            }
            
            var endRow = row - 1;
            for (var i = 1; i <= 11; i++)
            {
                worksheet.Cells[startRow, i, endRow, i].Merge = true;
                worksheet.Cells[startRow, i, endRow, i].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[startRow, i, endRow, i].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            }
            
            for (var i = 19; i <= 20; i++)
            {
                worksheet.Cells[startRow, i, endRow, i].Merge = true;
                worksheet.Cells[startRow, i, endRow, i].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[startRow, i, endRow, i].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            }
        }
        
        var tableRange = worksheet.Cells[1, 1, row - 1, columnNames.Length];

        // Add border to the entire table
        tableRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        tableRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        tableRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        tableRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        
        worksheet.Cells.AutoFitColumns();
        return await excelPackage.GetAsByteArrayAsync();
    }
}