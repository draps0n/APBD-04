using System;

namespace LegacyApp
{
    public class UserService
    {
        private IClientRepository _clientRepository;
        private IUserCreditService _userCreditService;
        private IUserDataAccess _userDataAccess;

        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
            _userDataAccess = new AdapterUserDataAccess();
        }

        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService, IUserDataAccess userDataAccess)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
            _userDataAccess = userDataAccess;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsFirstNameValid(firstName) || !IsLastNameValid(lastName))
            {
                return false;
            }

            if (!IsEmailValid(email))
            {
                return false;
            }

            var age = CalculateAgeUsingBirthDate(dateOfBirth);

            if (age < 21)
            {
                return false;
            }

            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            CalculateCreditLimitForUser(client, user);

            if (IsUsersCreditLimitTooLow(user))
            {
                return false;
            }

            _userDataAccess.AddUser(user);
            return true;
        }

        private static bool IsUsersCreditLimitTooLow(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

        private void CalculateCreditLimitForUser(Client client, User user)
        {
            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                {
                    var creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit *= 2;
                    user.CreditLimit = creditLimit;
                    break;
                }
                default:
                {
                    user.HasCreditLimit = true;
                    var creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                    break;
                }
            }
        }

        private static int CalculateAgeUsingBirthDate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }

        private static bool IsLastNameValid(string lastName)
        {
            return !string.IsNullOrEmpty(lastName);
        }

        private static bool IsFirstNameValid(string firstName)
        {
            return !string.IsNullOrEmpty(firstName);
        }

        private static bool IsEmailValid(string email)
        {
            return email.Contains('@') || email.Contains('.');
        }
    }
}