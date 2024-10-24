using System.Threading.Tasks;

namespace Interimkantoor.Controllers
{
    public class JobController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;

        public JobController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Job
        public async Task<IActionResult> Index()
        {
            var models = await _context.JobRepository.GetAllJobsAsync();
            JobIndexViewModel vm = new ();
            vm.Vacatures = models.ToList();

            return View(vm);
        }

        // GET: Job/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.JobRepository.GetJobAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Job/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Omschrijving,StartDatum,EindDatum,Locatie,IsWerkschoenen,IsBadge,IsKleding,AantalPlaatsen")] Job job)
        {
            if (ModelState.IsValid)
            {
                await _context.JobRepository.AddAsync(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Job/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.JobRepository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Omschrijving,StartDatum,EindDatum,Locatie,IsWerkschoenen,IsBadge,IsKleding,AantalPlaatsen")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.JobRepository.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _context.JobRepository.GetByIdAsync(id) != null)
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
            return View(job);
        }

        // GET: Job/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.JobRepository.GetByIdAsync(id);
            JobDeleteViewModel vm = _mapper.Map<JobDeleteViewModel>(job);

            if (job == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.JobRepository.GetByIdAsync(id);
            if (job != null)
            {
                _context.JobRepository.Delete(job);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}