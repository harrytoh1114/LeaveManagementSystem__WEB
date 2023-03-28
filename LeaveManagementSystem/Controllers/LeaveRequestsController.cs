using LeaveManagement.Common.Constants;
using LeaveManagement.Data;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Controllers
{
	[Authorize]
	public class LeaveRequestsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILeaveRequestRepository leaveRequestRepository;
		private readonly ILogger<LeaveRequestsController> logger;

		public LeaveRequestsController(ApplicationDbContext context, ILeaveRequestRepository leaveRequestRepository, ILogger<LeaveRequestsController> logger)
		{
			_context = context;
			this.leaveRequestRepository = leaveRequestRepository;
			this.logger = logger;
		}

		// GET: LeaveRequests
		[Authorize(Roles = Roles.Administrator)]
		public async Task<IActionResult> Index()
		{
			var model = await leaveRequestRepository.GetAdminLeaveRequestList();
			return View(model);
		}

		public async Task<IActionResult> MyLeave()
		{
			var model = await leaveRequestRepository.GetMyLeaveDetails();
			return View(model);
		}

		// GET: LeaveRequests/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			var model = await leaveRequestRepository.GetLeaveRequestAsync(id);

			if (model == null)
			{
				return NotFound();
			}

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApproveRequest(int id, bool approved)
		{
			try
			{
				await leaveRequestRepository.ChangeApprovalStatus(id, approved);

			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error Approving Request");
				throw;
			}

			return Redirect(nameof(Index));
		}

		// GET: LeaveRequests/Create
		public async Task<IActionResult> Create()
		{
			var model = new LeaveRequestCreateVM
			{
				LeaveType = new SelectList(_context.LeaveTypes, "Id", "Name")
			};
			return View(model);
		}

		// POST: LeaveRequests/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(LeaveRequestCreateVM model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var isValid = await leaveRequestRepository.CreateLeaveRequest(model);
					if (isValid)
					{
						return RedirectToAction(nameof(MyLeave));

					}
					else
					{
						ModelState.AddModelError(string.Empty, "Selectd leave has been fully utilised.");
					}
				}

			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error Cancel Request");
				ModelState.AddModelError(string.Empty, "Something went wrong. Please try again!");
			}

			model.LeaveType = new SelectList(_context.LeaveTypes, "Id", "Name", model.LeaveTypeId);
			return View(model);
		}


		// GET: LeaveRequests/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.LeaveRequests == null)
			{
				return NotFound();
			}

			var leaveRequests = await _context.LeaveRequests.FindAsync(id);
			if (leaveRequests == null)
			{
				return NotFound();
			}
			ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequests.LeaveTypeId);
			return View(leaveRequests);
		}

		// POST: LeaveRequests/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,LeaveTypeId,DateRequested,RequestComments,Approved,Cancelled,RequestingEmployeeId,Id,DateCreated,DateUpdated")] LeaveRequest leaveRequests)
		{
			if (id != leaveRequests.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(leaveRequests);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!LeaveRequestsExists(leaveRequests.Id))
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
			ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequests.LeaveTypeId);
			return View(leaveRequests);
		}

		// GET: LeaveRequests/Delete/5
		public async Task<IActionResult> CancelRequest(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			try
			{
				await leaveRequestRepository.CancelLeaveRequest(id);

			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error Cancel Request");
				throw;
			}


			return Redirect(nameof(MyLeave));
		}

		private bool LeaveRequestsExists(int id)
		{
			return (_context.LeaveRequests?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
