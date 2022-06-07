using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RentaAPI.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class GoodsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Identity", "Invalid identificator");

            using (WorkWith goods = new WorkWith())
            {
                await Task.Run(() => goods.Goods.Delete(id));

                return new ObjectResult(id);
            }
        }
    }
}
