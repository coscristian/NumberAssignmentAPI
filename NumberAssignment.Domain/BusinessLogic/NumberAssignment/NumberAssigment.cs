using BusinessLogic.Interfaces;
using DataAccess.DB;
using DataAccess.Entities;

namespace BusinessLogic.NumberAssignment
{
    public class NumberAssigment : INumberAsignment
    {
        private readonly DataContext _context;
        private const int TOTAL_DIGITS = 5;

        public NumberAssigment(DataContext context) 
        {
            _context = context;
        }

        private string FormatNumber(int number, int totalDigits)
        {
            return number.ToString($"D{totalDigits}");
        }

        private bool IsNumberInUse(int randomNumber, int clientId, int raffleId)
        {
            var assignations = _context.ClientRuffleAsignations
                                .Where(ca => ca.AssignedNumber == randomNumber
                                          && ca.ClientId     == clientId
                                          && ca.RaffleId      == raffleId)
                                .ToList();
            
            return assignations.Any();
        }

        private static int GenerateRandomNumber()
        {
            return new Random().Next(1, 100000);
        }
        private void SaveAssignation(int randomNumber, int clientId, int userId, int raffleId)
        {
            var clientAssignation = new ClientRaffleAsignation
            {
                AssignedNumber = randomNumber,
                ClientId = clientId,
                UserId = userId,
                RaffleId = raffleId                
            };

            _context.ClientRuffleAsignations.Add(clientAssignation);
            _context.SaveChanges();
        }

        private void CreateRaffle(int raffleId)
        {
            var newRaffle = new Raffle { Id = raffleId };
            _context.Raffles.Add(newRaffle);

            _context.SaveChanges();
        }

        private bool ExistsRaffle(int raffleId)
        {
            return _context.Raffles.Where(c => c.Id == raffleId).Any();
        }

        private void CreateUser(int userId)
        {
            var newUser = new User { Id = userId };
            _context.Users.Add(newUser);

            _context.SaveChanges();
        }

        private bool ExistsUser(int userId)
        {
            return _context.Users.Where(c => c.Id == userId).Any();
        }

        private void CreateClient(int clientId)
        {
            var newClient = new Client { Id = clientId };
            _context.Clients.Add(newClient);

            _context.SaveChanges();
        }

        private bool ExistsClient(int clientId)
        {
            return _context.Clients.Where(c => c.Id == clientId).Any();
        }

        private void ValidateEntities(int clientId, int userId, int raffleId)
        {
            if (!ExistsClient(clientId))
                CreateClient(clientId);

            if (!ExistsUser(userId))
                CreateUser(userId);

            if (!ExistsRaffle(raffleId))
                CreateRaffle(raffleId);
        }

        public string GetNumber(int clientId, int userId, int raffleId)
        {
            ValidateEntities(clientId, userId, raffleId);

            int randomNumber;
            int counter = 1;
            const int LIMIT = 10000;

            do
            {
                if (counter == LIMIT)
                    throw new Exception("All numbers were already used");

                counter++;

                randomNumber = GenerateRandomNumber();
            } while (IsNumberInUse(randomNumber, clientId, raffleId));

            SaveAssignation(randomNumber, clientId, userId, raffleId);

            return FormatNumber(randomNumber, TOTAL_DIGITS);
        }
    }
}
