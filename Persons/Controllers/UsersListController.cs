using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persons.Data;
using Users.Models;

namespace Persons.Controllers
{
    public class UsersListController : Controller
    {
        private readonly PersonsContext _context;

        public UsersListController(PersonsContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            List<User> users = await _context.User.ToListAsync();
            List<UserViewModel> userViewModelData = new List<UserViewModel>();
            UserViewModel _userViewModel = new UserViewModel();
            foreach(User user in users)
            {
                _userViewModel = new UserViewModel();
                _userViewModel.IsMarried = user.IsMarried;
                _userViewModel.SpouseId = user.SpouseId;
                _userViewModel.BirthDate = user.BirthDate;
                _userViewModel.Id = user.Id;
                _userViewModel.FirstName = user.FirstName;
                _userViewModel.LastName = user.LastName;
                _userViewModel.Age = Age(user.BirthDate);

                if (user.IsMarried == null)
                    _userViewModel.Spouse = null;
                else
                {
                    User? _spouse = null;

                    if (user.IsMarried.Value)
                    {
                        _spouse = await _context.User
                        .FirstOrDefaultAsync(m => m.Id == user.SpouseId);
                        _userViewModel.Spouse = "Married (" + (_spouse.FirstName ?? "").ToString() + " " + (_spouse.LastName ?? "").ToString() + ")";
                    }
                    else
                        _userViewModel.Spouse = "Single";
                }

                userViewModelData.Add(_userViewModel);
            }

            return View(userViewModelData);

        }

        public int? Age(DateTime? Birthdate)
        {
            if (Birthdate == null)
                return null;
            DateTime today = DateTime.Today;

            int age = today.Year - Birthdate.Value.Year;

            if (Birthdate.Value > today.AddYears(-age)) 
                age--;

            return age;
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string? FirstName, string? LastName, DateTime? BirthDate, List<string>? PhoneNumbers, List<string>? Addresses, string? PrimaryAddress)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.FirstName = FirstName ?? "";
                user.LastName = LastName ?? "";
                user.BirthDate = BirthDate;
                if (PhoneNumbers != null)
                {
                    if (PhoneNumbers.Count() > 0)
                    {
                        foreach (var phoneNumber in PhoneNumbers)
                        {
                            if (phoneNumber != null) 
                            {
                                PhoneNumbers _phoneNumber = new PhoneNumbers();
                                _phoneNumber.PhoneNumber = phoneNumber;

                                if (user.PhoneNumbers == null) 
                                    user.PhoneNumbers = new List<PhoneNumbers>();

                                user.PhoneNumbers.Add(_phoneNumber);
                            }
                        }
                    
                    }
                }

                if (Addresses != null)
                {
                    if (Addresses.Count() > 0)
                    {
                        foreach (var address in Addresses)
                        {
                            if (address != null)
                            {
                                Addresses _address = new Addresses();
                                _address.Address = address;
                                _address.IsPrimary = false;
                                if (PrimaryAddress != null)
                                {
                                    if (PrimaryAddress == address)
                                        _address.IsPrimary = true;
                                }

                                if (user.Addresses == null)
                                    user.Addresses = new List<Addresses>();

                                user.Addresses.Add(_address);
                            }
                            

                        }

                    }
                }




                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SaveSpouse(int? Id, string? Spouse)
        {
            if (Id == null)
                return NotFound();

            List<User> users = await _context.User.ToListAsync();

            User user = users.Where(x => x.Id == Id).FirstOrDefault();

            if (user == null)
                return NotFound();

            String spouse = Spouse ?? "";
            spouse = spouse.Substring(0, spouse.IndexOf("(") - 1);

            int? spouseid = users.Where(x => x.FirstName + " " + x.LastName == spouse).FirstOrDefault().Id;

            if (spouseid == null)
                throw (new Exception("Spouse doesn`t exist in users list!"));

            user.IsMarried = true;
            user.SpouseId = spouseid;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> EditUserIsSingle(int? id)
        {
            if (id == null)
                return NotFound();

            User? user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            user.IsMarried = false;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
