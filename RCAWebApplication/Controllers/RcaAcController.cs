using Microsoft.AspNetCore.Mvc;
using RCAWebApplication.Models;
using IRcaAcRepository = RCAWebApplication.Repository.IRcaAcRepository;
using System.Text;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace RCAWebApplication.Controllers
{
    public class RcaAcController : Controller
    {
        private readonly IRcaAcRepository _rcaAcRepository;
        public RcaAcController(IRcaAcRepository rcaAcRepository)
        {
            _rcaAcRepository = rcaAcRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Dropdowns()
        {
            var (cats, denials, rcas, actions) = await _rcaAcRepository.GetDropdownsAsync();
            return Json(new { categories = cats, denials, rcas, actions });
        }
        [HttpPost]
        public async Task<IActionResult> AddNew(string type, string name)
        {
            await _rcaAcRepository.AddNewItemAsync(type, name);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] RcaAcSetupDto dto)
        {
            await _rcaAcRepository.AddSetupAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var rows = await _rcaAcRepository.GetSetupListAsync();
            return Json(rows);
        }

        [HttpGet]
        public async Task<IActionResult> ExportToCsv()
        {
            var data = await _rcaAcRepository.ExporttoCSVRACSetupListAsync(); // fetch all rows, not paged
            var csv = new StringBuilder();

            // Header row
            csv.AppendLine("CategoryName,DenialDescription,RcaName,ActionDescription");

            // Data rows
            foreach (var row in data)
            {
                csv.AppendLine($"{row.CategoryName},{row.DenialText},{row.RCAName},{row.ActionCodeName}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "ExportedData.csv");
        }
        public async Task<IActionResult> GetPagedData([FromForm] DataTableRequest request)
        {
            var query = await _rcaAcRepository.GetSetupListAsync(); // fetch all rows, not paged

            // Filtering (if search is applied)
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var search = request.Search.Value.ToLower();
                query = query.Where(x =>
                    x.CategoryName.ToLower().Contains(search) ||
                    x.DenialText.ToLower().Contains(search) ||
                    x.RcaName.ToLower().Contains(search) ||
                    x.ActionCodeName.ToLower().Contains(search));
            }

            var totalRecords = query.Count();
            // Paging
            var data = query
                .Skip(request.Start)
                .Take(request.Length)
                .ToList();

            // Return response in DataTables format
            return Json(new
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = data
            });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                var rcalist = await _rcaAcRepository.DeleteAsync(id);

                return RedirectToAction("Index");
            }
            return BadRequest();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCatagory(int id)
        {
            if (id != null)
            {
                var catagorylist = await _rcaAcRepository.DeleteCategoryAsync(id);

                return RedirectToAction("Index");
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDenial(int id)
        {
            if (id != null)
            {
                var deniallist = await _rcaAcRepository.DeleteDenialAsync(id);

                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRCA(int id)
        {
            if (id != null)
            {
                var rcalist = await _rcaAcRepository.DeleteRCAAsync(id);

                return RedirectToAction("Index");
            }
            return BadRequest();
        }
        public async Task<IActionResult> DeleteAction(int id)
        {
            if (id != null)
            {
                var ActionCodelist = await _rcaAcRepository.DeleteActionCodeAsync(id);
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
    }
}
