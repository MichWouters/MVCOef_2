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
            IEnumerable<Job> modellen = await _unitOfWork.JobRepository.GetAllJobsAsync();
            List<JobDetailsViewModel> viewModels = new List<JobDetailsViewModel>();

            foreach (Job job in modellen)
            {
                int aantalBezettePlaatsen = job.KlantJobs?.Count ?? 0;
                JobDetailsViewModel vm = _mapper.Map<JobDetailsViewModel>(job);
                vm.VrijePlaatsen = job.AantalPlaatsen - aantalBezettePlaatsen;

                viewModels.Add(vm);
            }

            return View(viewModels);
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