using _3_StockControl_ServiceLayer.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockControl_EntityLayer;
using StockControl_EntityLayer.Entities.Concrete;
using StockControl_EntityLayer.Enums;

namespace _4_StockControl_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGenericService<Order> _service;
        private readonly IGenericService<OrderDetail> _detailService;
        private readonly IGenericService<Product> _productService;

        public OrderController(IGenericService<Order> service, IGenericService<OrderDetail> detailService, IGenericService<Product> productService)
        {
            _service = service;
            _detailService = detailService;
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_service.GetAll(a=>a.OrderDetails, b=>b.User));
        }

        [HttpGet]
        public IActionResult GetAllActiveOrders()
        {
            return Ok(_service.GetActive());
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            if (id == 0) return NotFound();
            else return Ok(_service.GetById(id, a => a.OrderDetails, b => b.User));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetailById(int id)
        {
            return Ok(_detailService.GetAll(x=>x.OrderID==id, a=>a.Product));
        }

        [HttpGet]
        public IActionResult GetOrdersByStatus()
        {
            return Ok(_service.GetDefault(x=>x.Status==Status.Pending));
        }


        [HttpPost]
        public IActionResult AddOrder(int userID, [FromQuery] int[] productID, [FromQuery] int[] quantites)
        {
            Order order = new();
            order.UserID = userID;
            order.Status = Status.Pending;
            order.IsActive = true;
            _service.Add(order);

            for (int i = 0; i < quantites.Length; i++)
            {
                OrderDetail od = new();
                
                od.OrderID = order.ID;
                od.ProductID = productID[i];
                od.IsActive = true;
                od.UnitPrice = _productService.GetById(productID[i]).UnitPrice*od.Quantity;
                _detailService.Add(od);
            }

            return Ok(order);
        }

        [HttpGet("{id}")]
        public IActionResult ConfirmOrder(int id)
        {
            var order=_service.GetById(id);
            if (order is null)
            {

                return NotFound();
            }
            else
            {
                List<OrderDetail> details = _detailService.GetDefault(x => x.OrderID == id);
                foreach (OrderDetail od in details)
                {
                    Product productInOrder = _productService.GetById(od.ProductID);
                    if (productInOrder.Stock>=od.Quantity)
                    {
                        productInOrder.Stock -= od.Quantity;
                        _productService.Update(productInOrder);
                    }
                   else return BadRequest();
                    //Transaction'da kullanabilirdim burada. Sonra bak!!!
                }
                order.Status = Status.Confirmed;
                order.IsActive = false;
                _service.Update(order);
                return Ok("Sipariş başarıyla onaylandı");
             
            }
        }

        [HttpGet("{id}")]
        public IActionResult CancelOrder(int id)
        {
            var order= _service.GetById(id);
            if(order is null) return NotFound();
            else
            {
                order.IsActive = false;
                order.Status = Status.Canceled;
                _service.Update(order);
                return Ok(order);
            }
        }



    }
}
