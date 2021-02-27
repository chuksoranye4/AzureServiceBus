using System.Threading.Tasks;
using AzureServiceBus.Services;
using Microsoft.AspNetCore.Mvc;
using SharedCode;

namespace AzureServiceBus.Controllers
{
    public class PersonDatasController : Controller
    {
        public IQueueServices _queue { get; }
        public PersonDatasController(IQueueServices queue)
        {
            _queue = queue;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("id,FirstName,LastName")] PersonData personData)
        {
            await _queue.SendMessageAsyn(personData, "personqueue");
            return View();
        }

    }
}
