using System.Diagnostics;

namespace Interimkantoor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private IJobRepository _jobRepository;
        private IUnitOfWork _unitOfWork;

        private IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Job> modellen = await _unitOfWork.JobRepository.GetAllAsync();
            List<JobDetailsViewModel> viewModellen = _mapper.Map<List<JobDetailsViewModel>>(modellen);

            // Manuele mapping => Niet doen, tijdrovend en foutgevoelig
            //foreach (var job in jobs)
            //{
            //    JobDetailsViewModel vm = new JobDetailsViewModel
            //    {
            //        Id = job.Id,
            //        Omschrijving = job.Omschrijving,
            //        AantalPlaatsen = job.AantalPlaatsen,
            //    }
            //}

            return View(viewModellen);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}