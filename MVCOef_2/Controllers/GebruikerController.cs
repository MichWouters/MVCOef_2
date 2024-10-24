namespace Interimkantoor.Controllers
{
    [Authorize(Roles = "Beheerder")]
    public class GebruikerController : Controller
    {
        private UserManager<CustomUser> _userManager;
        private SignInManager<CustomUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public GebruikerController(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(GebruikerLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && !user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Emailadres is nog niet bevestigd.");
                return View(model);
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
            {
                // Nooit exacte informatie geven: zeg alleen dat combinatie vekeerd is...
                ModelState.AddModelError("", "Verkeerde logincombinatie!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
            if (result.IsLockedOut)
                ModelState.AddModelError("", "Account geblokkeerd!!");

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Ongeldige loginpoging");
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.IsInRole("Gebruiker")) return RedirectToAction("Index", "Home");

            GebruikerListViewModel viewModel = new GebruikerListViewModel()
            {
                Gebruikers = _userManager.Users.ToList()
            };
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            CustomUser gebruiker = _userManager.Users.Where(k => k.Id == id).FirstOrDefault();

            if (gebruiker != null)
            {
                GebruikerEditViewModel viewModel = new GebruikerEditViewModel()
                {
                    GebruikerId = gebruiker.Id,
                    Naam = gebruiker.Achternaam,
                    Voornaam = gebruiker.Voornaam,
                    Email = gebruiker.Email,
                    Geboortedatum = gebruiker.Geboortedatum,
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GebruikerEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                CustomUser gebruiker = await _userManager.FindByIdAsync(viewModel.GebruikerId);

                gebruiker.Achternaam = viewModel.Naam;
                gebruiker.Voornaam = viewModel.Voornaam;
                gebruiker.Geboortedatum = viewModel.Geboortedatum;
                gebruiker.Email = viewModel.Email;

                IdentityResult result = await _userManager.UpdateAsync(gebruiker);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }

        public IActionResult Create()
        {
            GebruikerCreateViewModel viewModel = new GebruikerCreateViewModel()
            {
                Geboortedatum = new DateTime(1950, 1, 1),
                Rollen = new SelectList(_roleManager.Roles.ToList(), "Id", "Name")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GebruikerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                CustomUser gebruiker = new CustomUser
                {
                    Achternaam = viewModel.Naam,
                    Voornaam = viewModel.Voornaam,
                    Geboortedatum = viewModel.Geboortedatum,
                    UserName = viewModel.GebruikersNaam,
                    Email = viewModel.Email,
                    EmailConfirmed = true
                };

                IdentityResult result = await _userManager.CreateAsync(gebruiker, viewModel.Password);

                if (result.Succeeded)
                {
                    CustomUser user = await _userManager.FindByEmailAsync(viewModel.Email);
                    IdentityRole role = await _roleManager.FindByIdAsync(viewModel.RolId);

                    IdentityResult Resultaat = await _userManager.AddToRoleAsync(user, role.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            CustomUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            GebruikerDeleteViewModel viewModel = new GebruikerDeleteViewModel()
            {
                GebruikerId = id,
                Voornaam = user.Voornaam,
                Naam = user.Achternaam
            };
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            CustomUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", _userManager.Users.ToList());
        }
    }
}