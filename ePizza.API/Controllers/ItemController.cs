
using ePizza.Application.Contracts;
using ePizza.Application.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMemoryCache _memoryCache;

        public ItemController(
            IItemService itemService,
            IMemoryCache memoryCache)
        {
            this._itemService = itemService;
            this._memoryCache = memoryCache;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemResponseDto>>> Get()
        {
            IEnumerable<ItemResponseDto> response;

            _memoryCache.TryGetValue("Items", out response);

            if (response == null)
            {
                response = await _itemService.GetItemsAsync();

                _memoryCache.Set("Items", response,TimeSpan.FromMinutes(30));
            }

            return Ok(response);
        }

        [HttpGet("{id:int:min(0)}")]
        public async Task<ActionResult<ItemResponseDto>> GetById(int id)
        {
            var response = await _itemService.GetItemsByIdAsync(id);

            return Ok(response);
        }
    }
}
