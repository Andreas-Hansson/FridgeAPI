using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Fridge;
using Microsoft.AspNetCore.Mvc;
using Item = Fridge.Item;
using Quantity = Fridge.Quantity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FridgeAPI.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly IItemRepository _repository;
        private readonly FridgeService _fridgeService;
        public ItemController(IItemRepository repository)
        {
            _repository = repository;

            var flourItem = new Item
            {
                Name = "Flour",
                Quantity = new Quantity() { Unit = "L", Total = 1 }
            };
            _repository.Add(flourItem);
            var eggItem = new Item
            {
                Name = "Egg",
                Quantity = new Quantity() { Unit = "P", Total = 8 }
            };
            _repository.Add(eggItem);

            _fridgeService = new FridgeService(_repository);


        }

        // GET: api/values
        [HttpGet]
        public List<Item> Get()
        {
            return _fridgeService.GetAllItems();
        }

        [HttpPost]
        public Quantity Create([FromBody]Item item)
        {
            return  _fridgeService.AddItem(item);
        }


        [HttpDelete]
        public Quantity Remove([FromBody]Item item)
        {
            return _fridgeService.RemoveItem(item);
        }

        [HttpGet("{itemName}", Name = "GetItem")]
        public bool IsAvailable(string itemName)
        {
            return _fridgeService.IsAvailable(itemName);
        }


    }
}
