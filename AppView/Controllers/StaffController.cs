using AppView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    // Controllers/StaffController.cs
    public class StaffController : Controller
    {
        private readonly exam_distribution_testContext _context;

        public StaffController(exam_distribution_testContext context)
        {
            _context = context;
        }


        public IActionResult Index(string searchText, byte? searchStatus)
        {
            // Thực hiện lọc dữ liệu theo searchText và searchStatus
            var staffList = _context.Staff
                .Where(s => (string.IsNullOrEmpty(searchText) || s.Name.Contains(searchText) || s.AccountFe.Contains(searchText) || s.StaffCode.Contains(searchText)) &&
                            (!searchStatus.HasValue || s.Status == searchStatus))
                .ToList();

            return View(staffList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Staff staff)
        {
            if (staff == null)
            {
                return BadRequest(new { message = "Invalid staff data." });
            }

            // Kiểm tra nếu tất cả các trường đều được nhập
            if (string.IsNullOrEmpty(staff.Name) ||
                string.IsNullOrEmpty(staff.AccountFe) ||
                string.IsNullOrEmpty(staff.AccountFpt) ||
                string.IsNullOrEmpty(staff.StaffCode) ||
                staff.Status == null)
            {
                return BadRequest(new { message = "All fields are required." });
            }

            // Kiểm tra mã và email đã tồn tại chưa
            var existingStaff = await _context.Staff
                .Where(s => s.AccountFe == staff.AccountFe || s.AccountFpt == staff.AccountFpt || s.StaffCode == staff.StaffCode)
                .FirstOrDefaultAsync();

            if (existingStaff != null)
            {
                return Conflict(new { message = "Email or Staff Code already exists." });
            }

            // Kiểm tra định dạng email
            if (!staff.AccountFe.EndsWith("@fe.edu.vn"))
            {
                return BadRequest(new { message = "Email FE must end with @fe.edu.vn" });
            }

            if (!staff.AccountFpt.EndsWith("@fpt.edu.vn"))
            {
                return BadRequest(new { message = "Email FPT must end with @fpt.edu.vn" });
            }

            // Kiểm tra độ dài mã nhân viên
            if (staff.StaffCode.Length >= 15)
            {
                return BadRequest(new { message = "Staff Code must be less than 15 characters." });
            }

            // Thiết lập ngày tạo và ngày chỉnh sửa
            staff.CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            staff.LastModifiedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            // Thêm vào cơ sở dữ liệu
            _context.Add(staff);
            await _context.SaveChangesAsync();

            return Ok(staff); // Trả về nhân viên đã tạo
        }

        // GET: Staff/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingStaff = await _context.Staff
                        .Where(s => (s.AccountFe == staff.AccountFe || s.AccountFpt == staff.AccountFpt) && s.Id != id)
                        .FirstOrDefaultAsync();

                    if (existingStaff != null)
                    {
                        ModelState.AddModelError("", "Email already exists.");
                        return View(staff);
                    }

                    if (staff.AccountFe != null && !staff.AccountFe.EndsWith("@fe.edu.vn"))
                    {
                        ModelState.AddModelError(nameof(staff.AccountFe), "Email FE must end with @fe.edu.vn");
                        return View(staff);
                    }

                    if (staff.AccountFpt != null && !staff.AccountFpt.EndsWith("@fpt.edu.vn"))
                    {
                        ModelState.AddModelError(nameof(staff.AccountFpt), "Email FPT must end with @fpt.edu.vn");
                        return View(staff);
                    }

                    if (staff.StaffCode.Length >= 15)
                    {
                        ModelState.AddModelError(nameof(staff.StaffCode), "Staff Code must be less than 15 characters.");
                        return View(staff);
                    }

                    staff.LastModifiedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        private bool StaffExists(Guid id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }
    }
}
