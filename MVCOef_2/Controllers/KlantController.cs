using System.Threading.Tasks;

namespace Interimkantoor.Controllers
{
    public class KlantController : Controller
    {
        private readonly IUnitOfWork _context;

        public KlantController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Klant
        public async Task<IActionResult> Index()
        {
            return View(await _context.KlantRepository.GetAllAsync());
        }

        // GET: Klant/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.KlantRepository.GetByIdAsync(id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // GET: Klant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,Voornaam,Gemeente,Postcode,Straat,Huisnummer,Bankrekeningnummer")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                await _context.KlantRepository.AddAsync(klant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klant);
        }

        // GET: Klant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.KlantRepository.GetByIdAsync(id);
            if (klant == null)
            {
                return NotFound();
            }
            return View(klant);
        }

        // POST: Klant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Naam,Voornaam,Gemeente,Postcode,Straat,Huisnummer,Bankrekeningnummer")] Klant klant)
        {
            if (id != klant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.KlantRepository.Update(klant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _context.KlantRepository.GetByIdAsync(klant.Id) != null)
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
            return View(klant);
        }

        // GET: Klant/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.KlantRepository.GetByIdAsync(id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // POST: Klant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var klant = await _context.KlantRepository.GetByIdAsync(id);
            if (klant != null)
            {
                _context.KlantRepository.Delete(klant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}